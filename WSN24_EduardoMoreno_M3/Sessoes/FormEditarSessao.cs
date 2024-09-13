using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormEditarSessao : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormEditarSessao()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        #region Métodos

        private void FormEditarSessao_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

                if (string.IsNullOrEmpty(role))
                {
                    MessageBox.Show("Não tens permissão para aceder ao Form de Editar a Sessão.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    ShowDataOnGridView();
                }
        }

        private void ShowDataOnGridView()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    string query = @"
                SELECT s.id_sessao AS 'ID Sessão',
                       sa.codigo_sala AS 'Código Sala',
                       sa.descricao AS 'Sala',
                       f.codigo_filme AS 'Código Filme',
                       f.nome AS 'Filme',
                       s.data AS 'Data',
                       s.hora AS 'Hora',
                       s.ativa AS 'Ativa'
                FROM Sessao s
                JOIN Sala sa ON s.codigo_sala = sa.codigo_sala
                JOIN Filme f ON s.codigo_filme = f.codigo_filme";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewSessions.DataSource = dt;
                    dgViewSessions.AutoGenerateColumns = true;
                    dgViewSessions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgViewSessions.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados das sessões: " + ex.Message);
            }
        }

        private void dgViewSessions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgViewSessions.Rows[e.RowIndex];

                txtIDSessao.Text = row.Cells["ID Sessão"].Value.ToString();
                dtpData.Value = Convert.ToDateTime(row.Cells["Data"].Value);
                txtHour.Text = row.Cells["Hora"].Value.ToString();
                chkActive.Checked = Convert.ToBoolean(row.Cells["Ativa"].Value);
            }
        }

        private void ClearAllData()
        {
            txtIDSessao.Clear();
            dtpData.Value = DateTime.Now;
            txtHour.Clear();
            chkActive.Checked = false;
        }

        #endregion

        #region UI

        private void btnUpdateSessao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHour.Text) || string.IsNullOrWhiteSpace(txtIDSessao.Text))
            {
                MessageBox.Show("Preencha todos os campos obrigatórios antes de atualizar a sessão.");
                return;
            }

            TimeSpan hora;
            if (!TimeSpan.TryParse(txtHour.Text, out hora))
            {
                MessageBox.Show("O formato da hora deve ser hh:mm.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand(@"
                UPDATE Sessao 
                SET data = @data,
                    hora = @hora,
                    ativa = @ativa
                WHERE id_sessao = @id_sessao", con);

                    cmd.Parameters.AddWithValue("@id_sessao", txtIDSessao.Text);
                    cmd.Parameters.AddWithValue("@data", dtpData.Value.Date);
                    cmd.Parameters.AddWithValue("@hora", hora);
                    cmd.Parameters.AddWithValue("@ativa", chkActive.Checked);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sessão atualizada com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a sessão: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
