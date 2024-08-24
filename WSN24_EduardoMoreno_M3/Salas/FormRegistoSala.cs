using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoSala : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoSala()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            GenerateNewID();
            LoadSalas();
        }

        private void GenerateNewID()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(codigo_sala), 0) + 1 FROM Sala", con);
                    object result = cmd.ExecuteScalar();
                    txtIDSala.Text = result != null ? result.ToString() : "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar ID: " + ex.Message);
            }
        }

        private void LoadSalas()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT codigo_sala, descricao FROM Sala", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbSalas.DisplayMember = "descricao";
                    cbSalas.ValueMember = "codigo_sala";
                    cbSalas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Salas: " + ex.Message);
            }
        }

        private void ClearAllData()
        {
            txtDescricao.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDSala.Text) || string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand(@"
                        INSERT INTO Sala (codigo_sala, descricao) 
                        VALUES (@codigo_sala, @descricao)", con);

                    cmd.Parameters.AddWithValue("@codigo_sala", txtIDSala.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sala registada com sucesso!");

                    LoadSalas();
                    ClearAllData();
                    GenerateNewID();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registar a Sala: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}