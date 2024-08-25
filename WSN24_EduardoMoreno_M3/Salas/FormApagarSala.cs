using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.Sala
{
    public partial class FormApagarSala : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        public FormApagarSala()
        {
            InitializeComponent();
            LoadSalas();
        }

        #region Métodos

        private void LoadSalas()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT codigo_sala, descricao FROM Sala", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbSalas.DisplayMember = "descricao";
                    cbSalas.ValueMember = "codigo_sala";
                    cbSalas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar salas: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbSalas.SelectedValue == null)
            {
                MessageBox.Show("Seleciona uma sala para apagar.");
                return;
            }

            var result = MessageBox.Show("Tens a certeza que queres apagar esta sala?", "Confirmação", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (con = new SqlConnection(cs))
                    {
                        con.Open();
                        cmd = new SqlCommand("DELETE FROM Sala WHERE codigo_sala = @codigo_sala", con);
                        cmd.Parameters.AddWithValue("@codigo_sala", cbSalas.SelectedValue);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Sala apagada com sucesso!");

                        LoadSalas();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir a sala: " + ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}