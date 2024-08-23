using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.TipoFilme
{
    public partial class FormEditarTipoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;

        public FormEditarTipoFilme()
        {
            InitializeComponent();
            LoadTiposFilme();
        }

        #region Methods

        private void FormEditarTipoFilme_Load(object sender, EventArgs e)
        {
            LoadTiposFilme();
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
                MessageBox.Show("Por favor, selecione um Tipo de Filme para editar.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEditarTipoFilme.Text))
            {
                MessageBox.Show("O nome do Tipo de Filme não pode estar vazio.");
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
                MessageBox.Show("Ocorreu um erro ao tentar atualizar o Tipo de Filme: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
