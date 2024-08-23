using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormEditarFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormEditarFilme()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        private void ShowDataOnGridView()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    string query = @"
                SELECT f.codigo_filme AS 'Código Filme', 
                       f.nome AS 'Nome', 
                       f.descricao AS 'Descrição', 
                       f.ano AS 'Ano', 
                       t.descricao AS 'Tipo de Filme'
                FROM filme f
                JOIN TipoFilme t ON f.id_tipo = t.id_tipo";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewMovies.DataSource = dt;

                    // Ensure column names match the ones in DataTable
                    if (dgViewMovies.Columns.Contains("Código Filme"))
                        dgViewMovies.Columns["Código Filme"].HeaderText = "Código Filme";
                    if (dgViewMovies.Columns.Contains("Nome"))
                        dgViewMovies.Columns["Nome"].HeaderText = "Nome";
                    if (dgViewMovies.Columns.Contains("Descrição"))
                        dgViewMovies.Columns["Descrição"].HeaderText = "Descrição";
                    if (dgViewMovies.Columns.Contains("Ano"))
                        dgViewMovies.Columns["Ano"].HeaderText = "Ano";
                    if (dgViewMovies.Columns.Contains("Tipo de Filme"))
                        dgViewMovies.Columns["Tipo de Filme"].HeaderText = "Tipo de Filme";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgViewMovies.Rows.Count > 0)
            {
                try
                {
                    txtName.Text = dgViewMovies.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                    txtDescription.Text = dgViewMovies.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                    txtYear.Text = dgViewMovies.Rows[e.RowIndex].Cells["ano"].Value.ToString();
                    txtID.Text = dgViewMovies.Rows[e.RowIndex].Cells["codigo_filme"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar célula: " + ex.Message);
                }
            }
        }

        private void ClearAllData()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtYear.Text = "";
            lblID.Text = "";
        }

        #region UI

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtYear.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(lblID.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de atualizar o filme.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE filme SET nome = @nome, descricao = @descricao, ano = @ano WHERE codigo_filme = @codigo_filme", con);
                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ano", txtYear.Text);
                    cmd.Parameters.AddWithValue("@codigo_filme", lblID.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Filme editado com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o filme: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        #endregion
    }
}
