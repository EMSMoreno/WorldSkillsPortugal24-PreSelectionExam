using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoLocal : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoLocal()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        #region Methods

        private void FormRegistoLocal_Load(object sender, EventArgs e)
        {
            ShowDataOnGridView();
            GenerateNewLocalID();
        }

        private void GenerateNewLocalID()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = "SELECT ISNULL(MAX(id_local), 0) + 1 FROM Local";
                    cmd = new SqlCommand(query, con);
                    int newId = (int)cmd.ExecuteScalar();
                    txtIDLocal.Text = newId.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar novo ID de Local: " + ex.Message);
            }
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
            txtDescription.Clear();
            GenerateNewLocalID();
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Preencha a descrição antes de salvar.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Local (id_local, descricao) VALUES (@id_local, @descricao)", con);
                    cmd.Parameters.AddWithValue("@id_local", txtIDLocal.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Local registrado com sucesso!");

                    ShowDataOnGridView();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar o local: " + ex.Message);
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