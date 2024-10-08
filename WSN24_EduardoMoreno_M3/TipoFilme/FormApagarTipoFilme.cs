﻿using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.TipoFilme
{
    public partial class FormApagarTipoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormApagarTipoFilme()
        {
            InitializeComponent();
        }

        #region Métodos

        private void FormApagarTipoFilme_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Não tens permissão para aceder ao Form de Apagar o Tipo de Filme.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                CarregarTiposFilme();
            }
        }

        private void CarregarTiposFilme()
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

        #endregion

        #region UI

        private void btnApagarTipoFilme_Click(object sender, EventArgs e)
        {
            if (cbTiposFilme.SelectedIndex == -1)
            {
                MessageBox.Show("Seleciona um Tipo de Filme a apagar.");
                return;
            }

            int selectedID = (int)cbTiposFilme.SelectedValue;

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM TipoFilme WHERE id_tipo = @id_tipo", con);
                    cmd.Parameters.AddWithValue("@id_tipo", selectedID);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Tipo de Filme apagado com sucesso!");
                        CarregarTiposFilme();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao apagar o Tipo de Filme. Parece que já não existe!");
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Existe um erro ao tentar apagar o Tipo de Filme: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cbTiposFilme.Text = string.Empty;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
