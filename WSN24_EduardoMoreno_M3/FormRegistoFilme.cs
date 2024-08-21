using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WSN24_EduardoMoreno_M3_ConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public void ShowDataOnGridView()
        {
            using (con = new SqlConnection(cs))
            {
                adapter = new SqlDataAdapter("Select * from filme", con);
                dt = new DataTable();
                adapter.Fill(dt);
                dgViewMovies.DataSource = dt;
            }
        }

        public FormRegistoFilme()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ClearAllData()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtYear.Text = "";
        }

        private void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = this.dgViewMovies.CurrentRow.Cells["nome"].Value.ToString();
            txtDescription.Text = this.dgViewMovies.CurrentRow.Cells["descricao_filme"].Value.ToString();
            txtYear.Text = this.dgViewMovies.CurrentRow.Cells["ano"].Value.ToString();

            lblMID.Text = this.dgViewMovies.CurrentRow.Cells["id_filme"].Value.ToString();
        }

        private void FormRegistoFilme_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnCreateMovie_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Insert Into filme (nome, descricao_filme, ano) Values (@nome, @descricao_filme, @ano)", con);


                cmd.Parameters.AddWithValue("@nome", txtName.Text);
                cmd.Parameters.AddWithValue("@descricao_filme", txtDescription.Text);
                cmd.Parameters.AddWithValue("@ano", txtYear.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Filme registado com sucesso!");
                ShowDataOnGridView();
                ClearAllData();
            }
        }
    }
}
