using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3.Sala
{
    public partial class FormEditarSala : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormEditarSala()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        #region Métodos

        private void FormEditarSala_Load(object sender, EventArgs e)
        {
            ShowDataOnGridView();
        }

        private void ShowDataOnGridView()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    string query = @"
                    SELECT codigo_sala AS 'Código Sala', 
                           descricao AS 'Descrição'
                    FROM Sala";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewSalas.DataSource = dt;
                    dgViewSalas.AutoGenerateColumns = true;
                    dgViewSalas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgViewSalas.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados das salas: " + ex.Message);
            }
        }

        private void dgViewSalas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgViewSalas.Rows[e.RowIndex];

                txtIDSala.Text = row.Cells["Código Sala"].Value.ToString();
                txtDescription.Text = row.Cells["Descrição"].Value.ToString();
            }
        }

        private void ClearAllData()
        {
            txtIDSala.Clear();
            txtDescription.Clear();
        }

        #endregion

        #region UI

        private void btnUpdateSala_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtIDSala.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de atualizar a sala.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand(@"
                        UPDATE Sala 
                        SET descricao = @descricao
                        WHERE codigo_sala = @codigo_sala", con);

                    cmd.Parameters.AddWithValue("@codigo_sala", txtIDSala.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sala atualizada com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar a sala: " + ex.Message);
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