﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.Local
{
    public partial class FormApagarLocal : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormApagarLocal()
        {
            InitializeComponent();
        }

        #region Métodos

        private void FormApagarLocal_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Não tens permissão para aceder ao Form de Apagar o Local.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                LoadLocals();
            }
        }

        private void LoadLocals()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_local, descricao FROM Local", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbLocals.DisplayMember = "descricao";
                    cbLocals.ValueMember = "id_local";
                    cbLocals.DataSource = dt;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Locais: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnApagarLocal_Click(object sender, EventArgs e)
        {
            if (cbLocals.SelectedIndex == -1 || cbLocals.SelectedValue == null)
            {
                MessageBox.Show("Seleciona um Local para apagar.");
                return;
            }

            int selectedID = Convert.ToInt32(cbLocals.SelectedValue);

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Local WHERE id_local = @id_local", con);
                    cmd.Parameters.AddWithValue("@id_local", selectedID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Local apagado com sucesso!");
                        LoadLocals();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao apagar o Local. Parece que já não existe!");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao tentar apagar o Local: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbLocals.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
