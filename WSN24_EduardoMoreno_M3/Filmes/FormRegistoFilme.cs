using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoFilme()
        {
            InitializeComponent();
        }

        public void ShowDataOnGridView()
        {
            using (con = new SqlConnection(cs))
            {
                adapter = new SqlDataAdapter("SELECT * FROM filme", con);
                dt = new DataTable();
                adapter.Fill(dt);
                dgViewMovies.DataSource = dt;
            }
        }

        private void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Garantir que uma linha válida foi clicada
            {
                txtName.Text = dgViewMovies.Rows[e.RowIndex].Cells["nome"].Value.ToString();
                txtDescription.Text = dgViewMovies.Rows[e.RowIndex].Cells["descricao"].Value.ToString();
                txtYear.Text = dgViewMovies.Rows[e.RowIndex].Cells["ano"].Value.ToString();
                lblMID.Text = dgViewMovies.Rows[e.RowIndex].Cells["id_filme"].Value.ToString();
            }
        }

        private void FormRegistoFilme_Load(object sender, EventArgs e)
        {
            ShowDataOnGridView();
        }

        #region Methods

        public void ClearAllData()
        {
            txtName.Clear();
            txtDescription.Clear();
            txtYear.Clear();
            lblMID.Text = "";
        } //método para limpar

        #endregion

        #region UI

        private void btnCreateMovie_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("INSERT INTO filme (nome, descricao, ano) VALUES (@nome, @descricao, @ano)", con);

                cmd.Parameters.AddWithValue("@nome", txtName.Text);
                cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);
                cmd.Parameters.AddWithValue("@ano", txtYear.Text);

                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Filme registado com sucesso!");

                // Refresh DataGridView after Insert
                ShowDataOnGridView();
                ClearAllData();
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) //Chamar o método para limpar
        {
            ClearAllData();
        }

        #endregion
    }
}
