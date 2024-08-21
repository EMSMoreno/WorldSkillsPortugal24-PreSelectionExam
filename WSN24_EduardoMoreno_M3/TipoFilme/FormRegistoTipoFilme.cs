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

        private void FormRegistoTipoFilme_Load(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(ID), 0) + 1 FROM TipoFilme", con);
                int newID = (int)cmd.ExecuteScalar();
                txtID_tipofilme.Text = newID.ToString();
            }

            LoadTiposFilme();

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

                    cbTiposFilme.DisplayMember = "descricao";
                    cbTiposFilme.ValueMember = "id_tipo";
                    cbTiposFilme.DataSource = dt;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Tipos de Filme: " + ex.Message);
            }
        }

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

                    // Query Execution and Generate ID
                    int newID = Convert.ToInt32(cmd.ExecuteScalar());

                    MessageBox.Show("Tipo de Filme registado com sucesso!");

                    // Show Generated ID
                    txtID_tipofilme.Text = newID.ToString();
                    txtTypeName.Clear();
                    LoadTiposFilme();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante o registo do Tipo de Filme: " + ex.Message);
            }
        }
    }
}