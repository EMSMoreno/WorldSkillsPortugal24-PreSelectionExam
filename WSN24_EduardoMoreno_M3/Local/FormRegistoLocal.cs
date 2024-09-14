using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoLocal : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoLocal()
        {
            InitializeComponent();
            ShowDataOnGridView();
            GenerateNewLocalID();
        }

        #region Métodos

        private void FormRegistoLocal_Load(object sender, EventArgs e)
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
            GenerateNewLocalID();
        }

        private void HideEditingControls()
        {
            txtIDLocal.Visible = false;
            txtDescription.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            txtSearchLocal.Enabled = true;
            btnSearchLocal.Enabled = true;
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
                Text = "Não tens permissões para veres os IDs dos Locais.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(198, 235)
            };
            this.Controls.Add(lblPermission1);

            Label lblPermission2 = new Label
            {
                Text = "Não tens permissões para veres as Descrições dos Locais.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(198, 294)
            };
            this.Controls.Add(lblPermission2);

        }

        private void ShowAllControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }

            txtIDLocal.Visible = true;
            txtDescription.Visible = true;
            btnSave.Visible = true;
            btnCancel.Visible = true;
            txtSearchLocal.Enabled = true;
            btnSearchLocal.Enabled = true;
        }

        private void DisableEditingControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox && control != txtSearchLocal)
                {
                    control.Enabled = false;
                }
                else if (control is ComboBox || control == btnClose)
                {
                    control.Enabled = true;
                }
                else if (control is Button && control != btnSearchLocal)
                {
                    control.Enabled = false;
                }
            }
        }

        private void ShowViewOnlyControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control == txtSearchLocal || control == btnSearchLocal || control is DataGridView)
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

        private void GenerateNewLocalID()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(id_local), 0) + 1 FROM Local";
                    cmd = new SqlCommand(query, con);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        txtIDLocal.Text = result.ToString();
                    }
                    else
                    {
                        txtIDLocal.Text = "1"; // Se não houver registros, começa em 1
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar novo ID para o Local: " + ex.Message);
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
                    SELECT id_local AS 'ID Local',
                           descricao AS 'Descrição'
                    FROM Local";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewLocals.DataSource = dt;
                    dgViewLocals.AutoGenerateColumns = true;
                    dgViewLocals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgViewLocals.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private bool LocalExists(string descricao)
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM Local WHERE descricao = @descricao";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@descricao", descricao);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar existência do local: " + ex.Message);
                return false;
            }
        }

        private bool IsValidDescription(string description)
        {
            foreach (char c in description)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                {
                    return false;
                }
            }
            return true;
        }

        private void ClearAllData()
        {
            txtDescription.Clear();
            GenerateNewLocalID();
        }

        private void SearchLocais(string searchTerm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT id_local AS 'ID Local', descricao AS 'Descrição'
                        FROM Local
                        WHERE descricao LIKE @searchTerm", con);

                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewLocal.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Nenhum Local encontrado, com base naquilo que pesquisaste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar Locais: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Preencha a descrição antes de salvar.");
                return;
            }

            if (!IsValidDescription(txtDescription.Text))
            {
                MessageBox.Show("A descrição contém caracteres inválidos. Use apenas letras e números.");
                return;
            }

            if (LocalExists(txtDescription.Text))
            {
                MessageBox.Show("Esse local já existe.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Local (id_local, descricao) VALUES (@id_local, @descricao)", con);
                    cmd.Parameters.AddWithValue("@id_local", txtIDLocal.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Local registrado com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar o local: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnSearchLocal_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchLocal.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve o Local que procuras.");
                txtSearchLocal.Text = "";
                return;
            }

            SearchLocais(searchTerm);
            txtSearchLocal.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        
    }
}