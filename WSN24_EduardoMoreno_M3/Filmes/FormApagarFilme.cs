using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormApagarFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormApagarFilme()
        {
            InitializeComponent();
        }

        #region Métodos

        private void FormApagarFilme_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Não tens permissão para aceder ao Form de Apagar o Filme.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ShowDataOnGridView();
            }
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
                try
                {
                    DataGridViewRow row = dgViewMovies.Rows[e.RowIndex];

                    txtName.Text = row.Cells["Nome"].Value.ToString();
                    txtDescription.Text = row.Cells["Descrição"].Value.ToString();
                    txtYear.Text = row.Cells["Ano"].Value.ToString();
                    txtID.Text = row.Cells["Código Filme"].Value.ToString();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar célula: " + ex.Message);
                }
            }
        }

        private void DeleteMovie(string codigoFilme)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM filme WHERE codigo_filme = @codigo_filme", con);
                    cmd.Parameters.AddWithValue("@codigo_filme", codigoFilme);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Filme apagado com sucesso!");
                        ShowDataOnGridView();
                        ClearAllData();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum filme encontrado com o código fornecido.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar o filme: " + ex.Message);
            }
        }

        private void ClearAllData()
        {
            txtName.Clear();
            txtDescription.Clear();
            txtYear.Clear();
            txtID.Clear();
        }

        #endregion

        #region UI

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Seleciona um filme para apagar.");
                return;
            }

            DialogResult result = MessageBox.Show($"Tens a certeza que queres apagar o filme {txtName.Text}?", "Confirmar Eliminação", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                DeleteMovie(txtID.Text);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtYear.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}