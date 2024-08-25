using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormApagarSessao : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormApagarSessao()
        {
            InitializeComponent();
            LoadSessions();
        }

        #region Métodos

        private void LoadSessions()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT id_sessao, CONCAT('Sessão ', id_sessao) AS descricao FROM Sessao";
                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    cbSession.DisplayMember = "descricao";
                    cbSession.ValueMember = "id_sessao";
                    cbSession.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar sessões: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (cbSession.SelectedValue == null)
            {
                MessageBox.Show("Por favor, selecione uma sessão.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("DELETE FROM Sessao WHERE id_sessao = @id_sessao", con);
                    cmd.Parameters.AddWithValue("@id_sessao", cbSession.SelectedValue);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sessão apagada com sucesso!");
                        LoadSessions(); // Recarregar as sessões após exclusão
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma sessão encontrada com o ID fornecido.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao apagar a sessão: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}