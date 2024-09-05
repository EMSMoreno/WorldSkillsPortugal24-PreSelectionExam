using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoFilme()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        #region Métodos

        private void FormRegistoFilme_Load(object sender, EventArgs e)
        {
            ShowDataOnGridView();
            LoadTiposFilme();
        }

        private void ShowDataOnGridView()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = @"
                    SELECT f.codigo_filme AS 'Código Filme', 
                           f.nome AS 'Nome', 
                           f.descricao AS 'Descrição', 
                           FORMAT(f.ano, 'yyyy') AS 'Ano',
                           f.id_tipo AS 'ID Tipo', 
                           t.descricao AS 'TipoFilme'
                    FROM Filme f
                    JOIN TipoFilme t ON f.id_tipo = t.id_tipo";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewMovies.DataSource = dt;
                    dgViewMovies.AutoGenerateColumns = true;
                    dgViewMovies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgViewMovies.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados dos filmes: " + ex.Message);
            }
        }

        private void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgViewMovies.Rows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dgViewMovies.Rows[e.RowIndex];

                    txtName.Text = row.Cells["Nome"].Value.ToString();
                    txtDescription.Text = row.Cells["Descrição"].Value.ToString();
                    txtYear.Text = row.Cells["Ano"].Value.ToString();
                    txtID_filme.Text = row.Cells["Código Filme"].Value.ToString();

                    if (dt.Columns.Contains("ID Tipo"))
                    {
                        cbTipoFilme.SelectedValue = row.Cells["ID Tipo"].Value;
                    }
                    else
                    {
                        MessageBox.Show("A coluna 'ID Tipo' não foi encontrada.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar célula: " + ex.Message);
                }
            }
        }

        private void LoadTiposFilme()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_tipo, descricao FROM TipoFilme", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbTipoFilme.DisplayMember = "descricao";
                    cbTipoFilme.ValueMember = "id_tipo";
                    cbTipoFilme.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Tipos de Filme: " + ex.Message);
            }
        }

        private bool FilmeExists(string nome, int idTipo)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Filme WHERE nome = @nome AND id_tipo = @id_tipo";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@id_tipo", idTipo);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar existência do filme: " + ex.Message);
                return false;
            }
        }

        private void ClearAllData()
        {
            txtID_filme.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtYear.Clear();
            cbTipoFilme.SelectedIndex = -1;
        }

        private void SearchFilmes(string searchTerm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT codigo_filme AS 'Código Filme', nome AS 'Nome'
                        FROM Filme
                        WHERE nome LIKE @searchTerm", con);

                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewFilme.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Nenhum Filme encontrado, com base naquilo que pesquisaste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar Filmes: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnCreateMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                cbTipoFilme.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos antes de criar um filme.");
                return;
            }

            int idTipo = (int)cbTipoFilme.SelectedValue;

            if (FilmeExists(txtName.Text, idTipo))
            {
                MessageBox.Show("Esse Filme já existe.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Filme (nome, descricao, ano, id_tipo) VALUES (@nome, @descricao, @ano, @id_tipo)", con);

                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ano", new DateTime(int.Parse(txtYear.Text), 1, 1));
                    cmd.Parameters.AddWithValue("@id_tipo", idTipo);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Filme registado com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante o registo do Filme: " + ex.Message);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchLocal_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchFilme.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve o Filme que procuras.");
                txtSearchFilme.Text = "";
                return;
            }

            SearchFilmes(searchTerm);
            txtSearchFilme.Text = "";
        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        #endregion

    }
}