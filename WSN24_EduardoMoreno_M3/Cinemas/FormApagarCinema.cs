﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.Cinema
{
    public partial class FormApagarCinema : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormApagarCinema()
        {
            InitializeComponent();
        }

        #region Métodos

        private void FormApagarCinema_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Não tens permissão para aceder ao Form de Apagar o Cinema.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                LoadCinemas();
            }
        }

        private void LoadCinemas()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_cinema, nome FROM Cinema", con);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    cbCinemas.DisplayMember = "nome";
                    cbCinemas.ValueMember = "id_cinema";
                    cbCinemas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Cinemas: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnDeleteCinema_Click(object sender, EventArgs e)
        {
            if (cbCinemas.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um Cinema para apagar.");
                return;
            }

            int selectedID = (int)cbCinemas.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Cinema WHERE id_cinema = @id_cinema", con);
                    cmd.Parameters.AddWithValue("@id_cinema", selectedID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cinema apagado com sucesso!");
                        LoadCinemas();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao apagar o Cinema. Parece que já não existe!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar apagar o Cinema: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbCinemas.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}