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
        }

        #region Métodos

        private void FormRegistoCinema_Load(object sender, EventArgs e)
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

            LoadLocals();
            LoadCinemas();
        }

        private void HideEditingControls()
        {
            txtIDCinema.Visible = false;
            txtNameMovie.Visible = false;
            cbLocal.Visible = false;
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
                Text = "Não tens permissões para veres os IDs dos Cinemas.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(180, 249)
            };
            this.Controls.Add(lblPermission1);

            Label lblPermission2 = new Label
            {
                Text = "Não tens permissões para veres os Nomes dos Cinemas.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(180, 297)
            };
            this.Controls.Add(lblPermission2);

            Label lblPermission3 = new Label
            {
                Text = "Não tens permissões para veres os Locais associados aos Cinemas.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(180, 348)
            };
            this.Controls.Add(lblPermission3);

        }

        private void ShowAllControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }

            txtIDCinema.Visible = true;
            txtNameMovie.Visible = true;
            cbLocal.Visible = true;
            btnCreateMovie.Visible = true;
            btnCancel.Visible = true;
            btnClose.Visible = true;
            dgViewCinemas.Visible = true;
            dgSearchCinema.Visible = true;
            txtSearchCinema.Visible = true;
            btnSearchCinema.Visible = true;
        }

        private void DisableEditingControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox && control != txtSearchCinema)
                {
                    control.Enabled = false;
                }
                else if (control is ComboBox || control == btnClose)
                {
                    control.Enabled = true;
                }
                else if (control is Button && control != btnSearchCinema)
                {
                    control.Enabled = false;
                }
            }
        }

        private void ShowViewOnlyControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control == txtSearchCinema || control == btnSearchCinema || control is DataGridView)
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
            txtNameMovie.Clear();
            cbLocal.SelectedIndex = -1;
        }

        private void SearchCinemas(string searchTerm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT id_cinema AS 'ID Cinema', nome AS 'Nome'
                        FROM Cinema
                        WHERE nome LIKE @searchTerm", con);

                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dgSearchCinema.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Nenhum Cinema encontrado, com base naquilo que pesquisaste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar Cinemas: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNameMovie.Text) || cbLocal.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.");
                return;
            }

            if (CinemaExists(txtNameMovie.Text, (int)cbLocal.SelectedValue))
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

                    cmd.Parameters.AddWithValue("@nome", txtNameMovie.Text);
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

        private void btnSearchLocal_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchCinema.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve o Cinema que procuras.");
                txtSearchCinema.Text = "";
                return;
            }

            SearchCinemas(searchTerm);
            txtSearchCinema.Text = "";
        }

        #endregion
    }
}