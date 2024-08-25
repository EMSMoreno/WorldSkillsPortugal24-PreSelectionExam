using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.TipoFilme
{
    public partial class FormRegistoTipoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormRegistoTipoFilme()
        {
            InitializeComponent();
            LoadTiposFilme();
        }

        #region Métodos

        private void FormRegistoTipoFilme_Load(object sender, EventArgs e)
        {
            GenerateNewID();
            LoadTiposFilme();
            cbTiposFilme.SelectedIndexChanged += new EventHandler(cbTiposFilme_SelectedIndexChanged);
        }

        private void cbTiposFilme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTiposFilme.SelectedIndex >= 0)
            {
                string selectedTipoFilme = cbTiposFilme.Text;
                int selectedTipoFilmeID = (int)cbTiposFilme.SelectedValue;

                MessageBox.Show($"Tipo de Filme selecionado: {selectedTipoFilme}\nID: {selectedTipoFilmeID}");
            }
        }

        private void GenerateNewID()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(id_tipo), 0) + 1 FROM TipoFilme", con);
                int newID = (int)cmd.ExecuteScalar();
                txtID_tipofilme.Text = newID.ToString();
            }
        }

        private void LoadTiposFilme()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_tipo, descricao FROM TipoFilme", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count > 0)
                    {
                        cbTiposFilme.DisplayMember = "descricao";
                        cbTiposFilme.ValueMember = "id_tipo";
                        cbTiposFilme.DataSource = dt;
                    }
                    else
                    {
                        cbTiposFilme.Items.Clear();
                        cbTiposFilme.Items.Add("Nenhum Tipo de Filme encontrado");
                        cbTiposFilme.SelectedIndex = 0;
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Tipos de Filme: " + ex.Message);
            }
        }

        private bool TipoFilmeExists(string descricao)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM TipoFilme WHERE descricao = @descricao";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@descricao", descricao);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar Tipo de Filme: " + ex.Message);
                return false;
            }
        }

        #endregion

        #region UI

        private void btnSaveType_Click(object sender, EventArgs e)
        {
            string typeName = txtTypeName.Text;

            if (string.IsNullOrWhiteSpace(typeName))
            {
                MessageBox.Show("O nome do Tipo de Filme não pode estar vazio.");
                return;
            }

            if (TipoFilmeExists(typeName))
            {
                MessageBox.Show("Esse Tipo de Filme já existe.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO TipoFilme (descricao) VALUES (@descricao); SELECT SCOPE_IDENTITY();", con);
                    cmd.Parameters.AddWithValue("@descricao", typeName);

                    int newID = Convert.ToInt32(cmd.ExecuteScalar());

                    MessageBox.Show("Tipo de Filme registado com sucesso!");

                    txtID_tipofilme.Text = newID.ToString();
                    txtTypeName.Clear();
                    LoadTiposFilme();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registar Tipo de Filme: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
