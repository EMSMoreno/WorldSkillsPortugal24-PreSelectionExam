using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormEditarFilme : Form
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

        public FormEditarFilme()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        public void ClearAllData()
        {
            txtName.Text = "";
            txtDescription.Text = "";
            txtYear.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = this.dgViewMovies.CurrentRow.Cells["nome"].Value.ToString();
            txtDescription.Text = this.dgViewMovies.CurrentRow.Cells["descricao_filme"].Value.ToString();
            txtYear.Text = this.dgViewMovies.CurrentRow.Cells["ano"].Value.ToString();

            lblMID.Text = this.dgViewMovies.CurrentRow.Cells["id_filme"].Value.ToString();
        }


        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(cs))
                {
                con.Open();
                cmd = new SqlCommand("Update filme set nome=@nome, descricao_filme=@descricao, ano=@ano where id_filme=@id_filme", con);
                cmd.Parameters.AddWithValue("@nome", txtName.Text);
                cmd.Parameters.AddWithValue("@descricao", txtDescription.Text);
                cmd.Parameters.AddWithValue("@ano", txtYear.Text);
                cmd.Parameters.AddWithValue("@id_filme", lblMID.Text);

                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Filme editado com sucesso!");
                ShowDataOnGridView();
                ClearAllData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }
    }
}
