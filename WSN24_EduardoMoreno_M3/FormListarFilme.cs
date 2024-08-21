using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormListarFilme : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WSN24_EduardoMoreno_M3_ConnectionString"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public DataTable GetDataTable()
        {
            return dt;
        }

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

        public FormListarFilme()
        {
            InitializeComponent();
            ShowDataOnGridView();
        }

        public void dgViewMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = this.dgViewMovies.CurrentRow.Cells["Nome"].Value.ToString();
            txtDescription.Text = this.dgViewMovies.CurrentRow.Cells["Descricao"].Value.ToString();
            txtYear.Text = this.dgViewMovies.CurrentRow.Cells["Ano"].Value.ToString();

            lblSID.Text = this.dgViewMovies.CurrentRow.Cells["id_filme"].Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDeleteMovie_Click(object sender, EventArgs e)
        {
            using (con = new SqlConnection(cs))
            {
                con.Open();
                cmd = new SqlCommand("Delete From filme Where id_filme=@id_filme", con);
                cmd.Parameters.AddWithValue("@id_filme", lblSID.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Filme que selecionaste foi apagado com sucesso!");
                ShowDataOnGridView();
            }
        }

        
    }
}
