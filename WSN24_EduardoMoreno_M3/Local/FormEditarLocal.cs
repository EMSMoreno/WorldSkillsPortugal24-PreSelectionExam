using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormEditarLocal : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormEditarLocal()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        #region Métodos

        private void FormEditarLocal_Load(object sender, EventArgs e)
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
                    SELECT id_local AS 'ID Local',
                           descricao AS 'Descrição'
                    FROM Local";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dgViewLocals.DataSource = dt;
                    dgViewLocals.AutoGenerateColumns = true;
                    dgViewLocals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgViewLocals.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void ClearAllData()
        {
            txtIDLocal.Clear();
            txtDescription.Clear();
        }

        #endregion

        #region UI

        private void dgViewLocais_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgViewLocals.Rows.Count > 0)
            {
                try
                {
                    DataGridViewRow row = dgViewLocals.Rows[e.RowIndex];

                    txtIDLocal.Text = row.Cells["ID Local"].Value.ToString();
                    txtDescription.Text = row.Cells["Descrição"].Value.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao selecionar local: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text) || string.IsNullOrWhiteSpace(txtIDLocal.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de atualizar.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("UPDATE Local SET descricao = @descricao WHERE id_local = @id_local", con);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);
                    cmd.Parameters.AddWithValue("@id_local", txtIDLocal.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Local atualizado com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o local: " + ex.Message);
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