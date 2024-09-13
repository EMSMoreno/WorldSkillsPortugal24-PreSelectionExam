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
        }

        #region Métodos

        private void FormEditarFilme_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Não tens permissão para aceder ao Form de Editar o Filme.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                foreach (Form form in Application.OpenForms)
                {
                    if (form is FormPrincipal)
                    {
                        form.BringToFront();
                        return;
                    }
                }

                FormPrincipal formPrincipal = new FormPrincipal();
                formPrincipal.Show();

                return;
            }
            else
            {
                LoadTypesMovies();
                ShowDataOnGridView();
            }
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
                           FORMAT(f.ano, 'yyyy') AS 'Ano',
                           f.id_tipo AS 'ID Tipo', 
                           t.descricao AS 'Tipo de Filme'
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
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgViewMovies.Rows.Count > 0)
            {
                    DataGridViewRow row = dgViewMovies.Rows[e.RowIndex];

                    txtName.Text = row.Cells["Nome"].Value.ToString();
                    txtDescription.Text = row.Cells["Descrição"].Value.ToString();
                    txtYear.Text = row.Cells["Ano"].Value.ToString();
                    txtID.Text = row.Cells["Código Filme"].Value.ToString();
            }
        }

        private void LoadTypesMovies()
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
        }

        #endregion

        #region UI

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtYear.Text) || string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de atualizar o filme.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "UPDATE filme SET nome = @nome, descricao = @descricao, ano = @ano WHERE codigo_filme = @codigo_filme";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);

                    if (int.TryParse(txtYear.Text, out int ano))
                    {
                        DateTime dataAno = new DateTime(ano, 1, 1);
                        cmd.Parameters.AddWithValue("@ano", dataAno);
                    }
                    else
                    {
                        MessageBox.Show("Ano inválido. Certifique-se de que o campo 'Ano' contém um número válido.");
                        return;
                    }

                    cmd.Parameters.AddWithValue("@codigo_filme", txtID.Text);

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