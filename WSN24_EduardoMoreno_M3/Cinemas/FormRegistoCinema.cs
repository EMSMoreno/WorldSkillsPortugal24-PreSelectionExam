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
            GenerateNewID();
            LoadCinemas();
        }

        #region Métodos

        private void FormRegistoCinema_Load(object sender, EventArgs e)
        {
            LoadLocals();
            GenerateNewID();
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

        private void GenerateNewID()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(id_cinema), 0) + 1 FROM Cinema", con);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtIDCinema.Text = result.ToString();
                    }
                    else
                    {
                        txtIDCinema.Text = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar novo ID para o Cinema: " + ex.Message);
            }
        }

        private void LoadCinemas()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_cinema, nome, id_local FROM Cinema", con);
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

        private void ClearAllData()
        {
            txtName.Clear();
            cbLocal.SelectedIndex = -1;
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDCinema.Text) || string.IsNullOrWhiteSpace(txtName.Text) || cbLocal.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Cinema (id_cinema, nome, id_local) VALUES (@id_cinema, @nome, @id_local)", con);

                    cmd.Parameters.AddWithValue("@id_cinema", txtIDCinema.Text);
                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@id_local", cbLocal.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cinema registado com sucesso!");

                    ClearAllData();
                    GenerateNewID(); // Opcional: Gerar um novo ID após salvar
                    LoadCinemas(); // Atualiza o DataGridView com os novos dados
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registar o Cinema: " + ex.Message);
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