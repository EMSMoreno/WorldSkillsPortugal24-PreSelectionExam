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
                    SELECT f.codigo_filme, 
                           f.nome, 
                           f.descricao, 
                           FORMAT(f.ano, 'yyyy') AS Ano,  -- Formata a data para mostrar apenas o ano
                           f.id_tipo, 
                           t.descricao AS TipoFilme
                    FROM filme f
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
                    txtDescription.Text = row.Cells["Descricao"].Value.ToString();
                    txtYear.Text = row.Cells["Ano"].Value.ToString();
                    txtID_filme.Text = row.Cells["Codigo_Filme"].Value.ToString();

                    if (dt.Columns.Contains("id_tipo"))
                    {
                        cbTipoFilme.SelectedValue = row.Cells["id_tipo"].Value;
                    }
                    else
                    {
                        MessageBox.Show("A coluna 'id_tipo' não foi encontrada.");
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

        private void ClearAllData()
        {
            txtID_filme.Clear();
            txtName.Clear();
            txtDescription.Clear();
            txtYear.Clear();
            cbTipoFilme.SelectedIndex = -1;
        }

        #endregion

        #region UI

        private void btnCreateMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtYear.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || cbTipoFilme.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos antes de criar um filme.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO filme (nome, descricao, ano, id_tipo) VALUES (@nome, @descricao, @ano, @id_tipo)", con);

                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@ano", new DateTime(int.Parse(txtYear.Text), 1, 1));
                    cmd.Parameters.AddWithValue("@id_tipo", cbTipoFilme.SelectedValue);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Filme registado com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro durante o registo do Tipo de Filme: " + ex.Message);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        #endregion
    }
}
