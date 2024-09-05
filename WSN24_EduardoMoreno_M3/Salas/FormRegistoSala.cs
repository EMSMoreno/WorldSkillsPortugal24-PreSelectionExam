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
            GenerateNewID();
            LoadSalas();
        }

        #region Métodos

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
                    string query = "SELECT codigo_sala, descricao FROM Sala";
                    cmd = new SqlCommand(query, con);
                    adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbSalas.DisplayMember = "descricao";
                    cbSalas.ValueMember = "codigo_sala";
                    cbSalas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar salas: " + ex.Message);
            }
        }

        private void FormRegistoSessoes_Load(object sender, EventArgs e)
        {
            LoadSalas();
        }

        private void cbSalas_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalas();
        }

        private void ClearAllData()
        {
            txtDescricao.Clear();
        }

        private void SearchSalas(string searchTerm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT codigo_sala AS 'Código Sala', descricao AS 'Descrição'
                        FROM Sala
                        WHERE descricao LIKE @searchTerm", con);

                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewSala.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma Sala encontrada, com base naquilo que pesquisaste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar Salas: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Preenche a descrição antes de guardar!");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Sala (codigo_sala, descricao, id_cinema) VALUES (@codigo_sala, @descricao, @id_cinema)", con);
                    cmd.Parameters.AddWithValue("@codigo_sala", txtIDSala.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);

                    int defaultCinemaId = 1;
                    cmd.Parameters.AddWithValue("@id_cinema", defaultCinemaId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sala registrada com sucesso!");                   
                    LoadSalas();
                    ClearAllData();
                    GenerateNewID();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar a sala: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnSearchSala_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchSala.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve a Sala que procuras.");
                return;
            }

            SearchSalas(searchTerm);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        #endregion

    }
}