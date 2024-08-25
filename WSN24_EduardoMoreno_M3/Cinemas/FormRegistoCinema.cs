using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.Cinema
{
    public partial class FormRegistoCinema : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoCinema()
        {
            InitializeComponent();
            LoadLocals();
            LoadCinemas();
        }

        #region Métodos

        private void FormRegistoCinema_Load(object sender, EventArgs e)
        {
            LoadLocals();
            LoadCinemas();
        }

        private void LoadLocals()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_local, descricao FROM Local", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbLocal.DisplayMember = "descricao";
                    cbLocal.ValueMember = "id_local";
                    cbLocal.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Locais: " + ex.Message);
            }
        }

        private void LoadCinemas()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_cinema, nome, l.descricao AS Local FROM Cinema c JOIN Local l ON c.id_local = l.id_local", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewCinemas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Cinemas: " + ex.Message);
            }
        }

        private bool CinemaExists(string nome, int idLocal)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Cinema WHERE nome = @nome AND id_local = @id_local";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@id_local", idLocal);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar existência do cinema: " + ex.Message);
                return false;
            }
        }

        private void ClearAllData()
        {
            txtName.Clear();
            cbLocal.SelectedIndex = -1;
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || cbLocal.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.");
                return;
            }

            if (CinemaExists(txtName.Text, (int)cbLocal.SelectedValue))
            {
                MessageBox.Show("Esse cinema já está registrado no local selecionado.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Cinema (nome, id_local) VALUES (@nome, @id_local)", con);

                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@id_local", cbLocal.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cinema registrado com sucesso!");

                    ClearAllData();
                    LoadCinemas(); // Atualiza o DataGridView com os novos dados
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar o Cinema: " + ex.Message);
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

        #endregion
    }
}