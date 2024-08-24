using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.Cinema
{
    public partial class FormEditarCinema : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormEditarCinema()
        {
            InitializeComponent();
            LoadCinemas();
        }

        private void FormEditarCinema_Load(object sender, EventArgs e)
        {
            LoadCinemas();
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
                    dgViewCinemas.AutoGenerateColumns = true;
                    dgViewCinemas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cinemas: " + ex.Message);
            }
        }

        private void dgViewCinemas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = dgViewCinemas.Rows[e.RowIndex];

                    txtIDCinema.Text = row.Cells["id_cinema"].Value.ToString();
                    txtName.Text = row.Cells["nome"].Value.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar cinema: " + ex.Message);
                }
            }
        }

        private void btnUpdateCinema_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDCinema.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de atualizar.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE Cinema SET nome = @nome WHERE id_cinema = @id_cinema", con);

                    cmd.Parameters.AddWithValue("@id_cinema", txtIDCinema.Text);
                    cmd.Parameters.AddWithValue("@nome", txtName.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cinema atualizado com sucesso!");

                    LoadCinemas();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o cinema: " + ex.Message);
            }
        }

        private void ClearAllData()
        {
            txtIDCinema.Clear();
            txtName.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}