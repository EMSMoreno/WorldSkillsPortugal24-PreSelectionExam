using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoSala : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoSala()
        {
            InitializeComponent();
            ShowDataOnComboBox();
        }

        #region Methods

        private void GenerateNewID()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(codigo_sala), 0) + 1 FROM Sala", con);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        txtIDSala.Text = result.ToString();
                    }
                    else
                    {
                        txtIDSala.Text = "1";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar ID: " + ex.Message);
            }
        }

        private void ShowDataOnComboBox()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    string query = @"
                    SELECT codigo_sala, descricao
                    FROM Sala";

                    adapter = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    adapter.Fill(dt);

                    dt.Columns.Add("SalaFormatada", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        row["SalaFormatada"] = $"Sala {row["id_sala"]}";
                    }

                    cbSalas.DisplayMember = "SalaFormatada";
                    cbSalas.ValueMember = "codigo_sala";
                    cbSalas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void FormSala_Load(object sender, EventArgs e)
        {
            GenerateNewID();
            LoadSessao();
            LoadCinemas();
            ShowDataOnComboBox();
        }

        private void LoadSessao()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_sessao FROM Sessao", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbSessao.DisplayMember = "id_sessao";
                    cbSessao.ValueMember = "id_sessao";
                    cbSessao.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Sessões: " + ex.Message);
            }
        }

        private void LoadCinemas()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT id_cinema, nome FROM Cinema", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbSalas.DisplayMember = "nome";
                    cbSalas.ValueMember = "id_cinema";
                    cbSalas.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Cinemas: " + ex.Message);
            }
        }

        private void ClearAllData()
        {
            txtIDSala.Clear();
            txtIDCinema.Clear();
            txtDescricao.Clear();
            cbSessao.SelectedIndex = -1;
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDSala.Text) || string.IsNullOrWhiteSpace(txtIDCinema.Text) || string.IsNullOrWhiteSpace(txtDescricao.Text) || cbSessao.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.");
                return;
            }

            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    cmd = new SqlCommand("INSERT INTO Sala (id_sala, id_cinema, descricao, id_sessao) VALUES (@id_sala, @id_cinema, @descricao, @id_sessao)", con);

                    cmd.Parameters.AddWithValue("@id_sala", txtIDSala.Text);
                    cmd.Parameters.AddWithValue("@id_cinema", txtIDCinema.Text);
                    cmd.Parameters.AddWithValue("@descricao", txtDescricao.Text);
                    cmd.Parameters.AddWithValue("@id_sessao", cbSessao.SelectedValue);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sala registada com sucesso!");

                    ShowDataOnComboBox();
                    ClearAllData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registar a sala: " + ex.Message);
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
