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

        #region Methods

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

        #endregion

        #region UI

        private void btnSaveType_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTypeName.Text))
            {
                MessageBox.Show("O nome do Tipo de Filme não pode estar vazio.");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO TipoFilme (descricao) VALUES (@descricao); SELECT SCOPE_IDENTITY();", con);
                    cmd.Parameters.AddWithValue("@descricao", txtTypeName.Text);

                    // Executa a query e obtém o ID gerado
                    int newID = Convert.ToInt32(cmd.ExecuteScalar());

                    MessageBox.Show("Tipo de Filme registado com sucesso!");

                    // Atualiza o ID e limpa o campo de nome
                    txtID_tipofilme.Text = newID.ToString();
                    txtTypeName.Clear();

                    // Recarrega os tipos de filmes na ComboBox
                    LoadTiposFilme();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante o registo do Tipo de Filme: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
