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
                string query = "SELECT Pass, Salt FROM Users WHERE Nome = @Nome";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Nome", username);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["Pass"].ToString();
                            string storedSalt = reader["Salt"].ToString();

                            // Verifica a senha com o salt armazenado
                            string hashedPassword = PasswordHasher.HashPasswordWithSalt(password, storedSalt);

                            return hashedPassword == storedHash;
                        }
                        else
                        {
                            return false; // Usuário não encontrado
                        }
                    }
                }
            }
        }

        public static class PasswordHasher
        {
            public static string HashPassword(string password, out string salt)
            {
                // Gerar o salt
                byte[] saltBytes = new byte[16];
                using (var rng = new RNGCryptoServiceProvider())
                {
                    rng.GetBytes(saltBytes);
                }
                salt = Convert.ToBase64String(saltBytes);

                // Combina a senha com o salt e gera o hash
                return HashPasswordWithSalt(password, salt);
            }

            public static string HashPasswordWithSalt(string password, string salt)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Combina a senha com o salt e gera o hash
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

            // Gerar o salt e o hash da senha
            string salt;
            string hashedPassword = PasswordHasher.HashPassword(password, out salt);

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "INSERT INTO Users (Nome, Pass, Salt) VALUES (@Nome, @Pass, @Salt)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Nome", username);
                        cmd.Parameters.AddWithValue("@Pass", hashedPassword);
                        cmd.Parameters.AddWithValue("@Salt", salt); // Salvando o salt corretamente

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
    }
        #endregion   
}