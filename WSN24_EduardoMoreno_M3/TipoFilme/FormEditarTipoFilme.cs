using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.TipoFilme
{
    public partial class FormEditarTipoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormEditarTipoFilme()
        {
            InitializeComponent();
        }

        #region Métodos

        private void FormEditarTipoFilme_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role)) // Se o utilizador não tiver role (NULL ou "")
            {
                MessageBox.Show("Não tens permissão para aceder ao Form de Editar o Tipo de Filme.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Fecha o formulário atual
                this.Close();

                // Verifica se o FormPrincipal já está aberto; se não, o abre
                foreach (Form form in Application.OpenForms)
                {
                    if (form is FormPrincipal)
                    {
                        form.BringToFront();
                        return;
                    }
                }

                // Se o FormPrincipal não estiver aberto, cria uma nova instância e a mostra
                FormPrincipal formPrincipal = new FormPrincipal();
                formPrincipal.Show();

                return; // Garante que o resto do código não será executado
            }
            else
            {
                LoadTiposFilme(); // Carrega os tipos de filme caso o role seja válido
            }
        }

        private void LoadTiposFilme()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_tipo, descricao FROM TipoFilme", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbTiposFilme.DisplayMember = "descricao";
                    cbTiposFilme.ValueMember = "id_tipo";
                    cbTiposFilme.DataSource = dt;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Tipos de Filme: " + ex.Message);
            }
        }

        private void cbTiposFilme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTiposFilme.SelectedIndex != -1)
            {
                txtEditarTipoFilme.Text = cbTiposFilme.Text;
            }
        }

        #endregion

        #region UI

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (cbTiposFilme.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um Tipo de Filme para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditarTipoFilme.Text))
            {
                MessageBox.Show("O nome do Tipo de Filme não pode estar vazio, cuidado!");
                return;
            }

            int selectedID = (int)cbTiposFilme.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE TipoFilme SET descricao = @descricao WHERE id_tipo = @id_tipo", con);
                    cmd.Parameters.AddWithValue("@descricao", txtEditarTipoFilme.Text);
                    cmd.Parameters.AddWithValue("@id_tipo", selectedID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Tipo de Filme atualizado com sucesso!");
                        LoadTiposFilme();
                        txtEditarTipoFilme.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("Falha ao atualizar o Tipo de Filme. Vê se ainda existe.");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Existe um erro ao tentar atualizar o Tipo de Filme: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbTiposFilme.Text = string.Empty;
            txtEditarTipoFilme.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
