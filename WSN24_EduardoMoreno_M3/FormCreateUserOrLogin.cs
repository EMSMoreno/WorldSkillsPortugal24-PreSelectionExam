using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormCreateUserOrLogin : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormCreateUserOrLogin()
        {
            InitializeComponent();
        }

        #region Métodos

        private bool LoginUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string query = "SELECT Pass, Role, Salt FROM Users WHERE Nome = @Nome";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["Pass"].ToString();
                            string role = reader["Role"].ToString();
                            string salt = reader["Salt"].ToString();

                            if (IsPasswordHashed(storedHash))
                            {
                                string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, salt);
                                if (hashedPassword == storedHash)
                                {
                                    // Armazena o privilégio do utilizador na sessão
                                    UserSession.Role = role;
                                    return true;
                                }
                            }
                            else
                            {
                                if (storedHash == password)
                                {
                                    // Armazena o privilégio do utilizador na sessão
                                    UserSession.Role = role;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        public static class PasswordHasher
        {
            public static string HashPassword(string password, out string salt)
            {
                byte[] saltBytes = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }
                salt = Convert.ToBase64String(saltBytes);

                return HashPasswordWithSalt(password, salt);
            }

            public static string HashPasswordWithSalt(string password, string salt)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }

        private bool IsPasswordHashed(string password)
        {
            return password.Length >= 64;
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role)) // Se o utilizador não tiver role (NULL ou "")
            {
                ShowViewOnlyControls(); // Mostra só os DataGridViews
            }
            else if (role.ToLower() == "admin")
            {
                ShowAllControls(); // Admin pode ver e editar tudo
            }
            else if (role.ToLower() == "coordenador")
            {
                ShowAllControls(); // Coordenador pode ver tudo
                DisableEditingControls(); // Mas não pode editar
            }
            else
            {
                ShowViewOnlyControls(); // Qualquer outro role não reconhecido só pode visualizar
            }
        }

        private void ShowAllControls()
        {
            // Mostra todos os controles
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }
        }

        private void DisableEditingControls()
        {
            // Desabilita controles de edição, mas mantém visíveis
            foreach (Control control in this.Controls)
            {
                if (control is TextBox || control is ComboBox || control is Button)
                {
                    control.Enabled = false;
                }
            }
        }

        private void ShowViewOnlyControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is DataGridView)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else
                {
                    control.Visible = false; // Esconde qualquer outra coisa que não seja DataGridView
                    control.Enabled = false;
                }
            }
        }

        #endregion

        #region UI

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text;
            string pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Preenche todos os campos.");
                return;
            }

            if (LoginUser(user, pass))
            {
                MessageBox.Show("Login bem-sucedido! Bem-vindo aos Cinemas Skillianos!");
                this.Hide();
                txtUsername.Text = "";
                txtPassword.Text = "";
                new FormPrincipal().Show();
            }
            else
            {
                MessageBox.Show("Nome de utilizador ou password inválidos.");
            }
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            string username = txtUsernameNew.Text;
            string password = txtPasswordNew.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("O Username e a Password são de preenchimento obrigatório!");
                return;
            }

            string salt;
            string hashedPassword = PasswordHasher.HashPassword(password, out salt);

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "INSERT INTO Users (Nome, Pass, Salt, Role) VALUES (@Nome, @Pass, @Salt, @Role)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", username);
                        cmd.Parameters.AddWithValue("@Pass", hashedPassword);
                        cmd.Parameters.AddWithValue("@Salt", salt);
                        cmd.Parameters.AddWithValue("@Role", "espetador"); // privilégio default

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Utilizador criado com sucesso!");
                            txtUsernameNew.Text = "";
                            txtPasswordNew.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Erro ao criar Utilizador! Tenta novamente!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}