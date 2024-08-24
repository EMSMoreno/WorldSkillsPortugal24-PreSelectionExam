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

        #region Methods

        private void FormEditarFilme_Load(object sender, EventArgs e)
        {
            LoadTiposFilme();
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
                   f.id_tipo AS 'ID Tipo',  -- Inclua a FK no SELECT
                   t.descricao AS 'Tipo de Filme'
            FROM filme f
            JOIN TipoFilme t ON f.id_tipo = t.id_tipo";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewMovies.DataSource = dt;

                    // Configuração opcional dos cabeçalhos das colunas
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

                    // Opcional: ocultar a coluna da FK se não for necessária na interface
                    if (dgViewMovies.Columns.Contains("ID Tipo"))
                        dgViewMovies.Columns["ID Tipo"].Visible = false;
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
                    DataGridViewRow row = dgViewMovies.Rows[e.RowIndex];

                    txtName.Text = row.Cells["Nome"].Value.ToString();
                    txtDescription.Text = row.Cells["Descrição"].Value.ToString();
                    txtYear.Text = row.Cells["Ano"].Value.ToString();
                    txtID.Text = row.Cells["Código Filme"].Value.ToString();

                    // Captura o valor da FK e seleciona no ComboBox
                    if (row.Cells["ID Tipo"].Value != DBNull.Value)
                    {
                        cbTipoFilme.SelectedValue = row.Cells["ID Tipo"].Value;
                    }
                    else
                    {
                        MessageBox.Show("O valor da coluna 'ID Tipo' é nulo.");
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
            txtName.Clear();
            txtDescription.Clear();
            txtYear.Clear();
            cbTipoFilme.SelectedIndex = -1;
        }

        #endregion

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

        private void btnCancelar_Click(object sender, EventArgs e)
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
