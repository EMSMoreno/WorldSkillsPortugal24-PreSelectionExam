using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using System.Xml.Linq;

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
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                HideEditingControls();
                ShowPermissionLabel();
            }
            else if (role.ToLower() == "admin")
            {
                ShowAllControls();
            }
            else if (role.ToLower() == "coordenador")
            {
                ShowAllControls();
                DisableEditingControls();
            }
            else
            {
                ShowViewOnlyControls();
            }

            ShowDataOnGridView();
            LoadTiposFilme();
            ShowDataOnGridView();
        }

        private void HideEditingControls()
        {
            txtID_filme.Visible = false;
            txtName.Visible = false;
            txtDescription.Visible = false;
            txtYear.Visible = false;
            cbTipoFilme.Visible = false;
            btnCreateMovie.Visible = false;
            btnCancel.Visible = false;
            btnClose.Visible = true;
        }

        private void ShowPermissionLabel()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.ForeColor == System.Drawing.Color.Red)
                {
                    this.Controls.Remove(control);
                }
            }

            Label lblPermission1 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres os IDs dos Filmes.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(198, 233)
            };
            this.Controls.Add(lblPermission1);

            Label lblPermission2 = new Label
            {
                Text = "Não tens permissões para \n " +
                "veres os Nomes dos Filmes.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(338, 233)
            };
            this.Controls.Add(lblPermission2);

            Label lblPermission3 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres a Descrição dos Filmes.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(273, 299)
            };
            this.Controls.Add(lblPermission3);

            Label lblPermission4 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres os Anos dos Filmes",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(198, 382)
            };
            this.Controls.Add(lblPermission4);

            Label lblPermission5 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres os Tipos de Filme.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(348, 381)
            };
            this.Controls.Add(lblPermission5);
        }

        private void ShowAllControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }

            txtID_filme.Visible = true;
            txtName.Visible = true;
            txtDescription.Visible = true;
            txtYear.Visible = true;
            cbTipoFilme.Visible = true;
            btnCreateMovie.Visible = true;
            btnCancel.Visible = true;
            dgViewMovies.Visible = true;
            dgSearchMovie.Visible = true;
            txtSearchMovie.Visible = true;
            btnSearchMovie.Visible = true;
            btnClose.Visible = true;
        }

        private void DisableEditingControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox && control != txtSearchMovie)
                {
                    control.Enabled = false;
                }
                else if (control is ComboBox || control == btnClose)
                {
                    control.Enabled = true;
                }
                else if (control is Button && control != btnSearchMovie)
                {
                    control.Enabled = false;
                }
            }
        }

        private void ShowViewOnlyControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control == txtSearchMovie || control == btnSearchMovie || control is DataGridView)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else if (control is ComboBox)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else if (control == btnClose)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else if (control is TextBox || control is Button)
                {
                    control.Visible = false;
                    control.Enabled = false;
                }
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
                        dgSearchMovie.DataSource = dt;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearchLocal_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchMovie.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve o Filme que procuras.");
                txtSearchMovie.Text = "";
                return;
            }

            SearchFilmes(searchTerm);
            txtSearchMovie.Text = "";
        }

        private void btnCancelarOperacao_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        #endregion

    }
}