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
        }

        #region Métodos

        private void FormRegistoTipoFilme_Load(object sender, EventArgs e)
        {
            GenerateNewID();
            LoadTiposFilme();
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

        private void SearchTiposFilme(string searchTerm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_tipo, descricao FROM TipoFilme WHERE descricao LIKE @searchTerm", con);
                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewTipoFilme.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Nenhum Tipo de Filme encontrado, com base naquilo que procuras.");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar Tipos de Filme: " + ex.Message);
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
                MessageBox.Show("Esse Tipo de Filme já existe!");
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO TipoFilme (descricao) VALUES (@descricao)", con);
                    cmd.Parameters.AddWithValue("@descricao", typeName);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Tipo de Filme registado com sucesso!");

                    GenerateNewID();
                    txtTypeName.Clear();
                    LoadTiposFilme();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registar Tipo de Filme: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtID_tipofilme.Text = string.Empty;
            txtTypeName.Text = string.Empty;
        }

        private void btnSearchTipoFilme_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchTipoFilme.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve o Tipo de Filme que procuras.");
                return;
            }

            SearchTiposFilme(searchTerm);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

      
    }
}
