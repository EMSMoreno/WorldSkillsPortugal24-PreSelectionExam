using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormRegistoSessao : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["WorldSkillsPreSelection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public FormRegistoSessao()
        {
            InitializeComponent();
            InitializeForm();
        }

        #region Métodos

        private void InitializeForm()
        {
            LoadSalas();
            LoadFilmes();
            LoadCinemas();
            ShowDataOnGridView();
            GenerateNewID();
        }

        private void GenerateNewID()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT ISNULL(MAX(id_sessao), 0) + 1 FROM Sessao", con);
                    object result = cmd.ExecuteScalar();
                    txtIDSessao.Text = result != null ? result.ToString() : "1";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar ID: " + ex.Message);
            }
        }

        private void LoadSalas()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT codigo_sala, descricao FROM Sala", con);
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    cbSala.DisplayMember = "descricao";
                    cbSala.ValueMember = "codigo_sala";
                    cbSala.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Salas: " + ex.Message);
            }
        }

        private void LoadFilmes()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT codigo_filme, nome FROM Filme", con);

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    cbFilme.DisplayMember = "nome";
                    cbFilme.ValueMember = "codigo_filme";
                    cbFilme.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Filmes: " + ex.Message);
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

                    DataTable dt = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);

                    cbCinema.DisplayMember = "nome";
                    cbCinema.ValueMember = "id_cinema";
                    cbCinema.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar Cinemas: " + ex.Message);
            }
        }

        private void ShowDataOnGridView()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = @"
                SELECT s.id_sessao AS 'ID Sessão',
                       sa.descricao AS 'Sala',
                       f.nome AS 'Filme',
                       c.nome AS 'Cinema',  -- Inclui o nome do cinema
                       s.data AS 'Data',
                       s.hora AS 'Hora',
                       s.ativa AS 'Ativa'
                FROM Sessao s
                JOIN Sala sa ON s.codigo_sala = sa.codigo_sala
                JOIN Filme f ON s.codigo_filme = f.codigo_filme
                JOIN Cinema c ON s.id_cinema = c.id_cinema";  // Adiciona o join para Cinema

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
                MessageBox.Show("Erro ao carregar sessões: " + ex.Message);
            }
        }

        private bool IsSessionDuplicate()
        {
            try
            {
                using (con = new SqlConnection(cs))
                {
                    con.Open();
                    string query = @"
                        SELECT COUNT(*) 
                        FROM Sessao 
                        WHERE codigo_sala = @codigo_sala 
                          AND codigo_filme = @codigo_filme 
                          AND data = @data 
                          AND hora = @hora";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@codigo_sala", cbSala.SelectedValue);
                    cmd.Parameters.AddWithValue("@codigo_filme", cbFilme.SelectedValue);
                    cmd.Parameters.AddWithValue("@data", dtpData.Value.Date);
                    cmd.Parameters.AddWithValue("@hora", txtHour.Text);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar duplicatas: " + ex.Message);
                return false;
            }
        }

        private void ClearAllData()
        {
            txtIDSessao.Clear();
            cbSala.SelectedIndex = -1;
            cbFilme.SelectedIndex = -1;
            dtpData.Value = DateTime.Now;
            txtHour.Clear();
            chkActive.Checked = false;
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDSessao.Text) ||
                cbSala.SelectedValue == null ||
                cbFilme.SelectedValue == null ||
                cbCinema.SelectedValue == null ||  // Certifique-se de que o id_cinema não é nulo
                string.IsNullOrWhiteSpace(txtHour.Text))
            {
                MessageBox.Show("Preencha todos os campos antes de salvar.");
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
                INSERT INTO Sessao (id_sessao, codigo_sala, codigo_filme, id_cinema, data, hora, ativa) 
                VALUES (@id_sessao, @codigo_sala, @codigo_filme, @id_cinema, @data, @hora, @ativa)", con);

                    cmd.Parameters.AddWithValue("@id_sessao", txtIDSessao.Text);
                    cmd.Parameters.AddWithValue("@codigo_sala", cbSala.SelectedValue);
                    cmd.Parameters.AddWithValue("@codigo_filme", cbFilme.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_cinema", cbCinema.SelectedValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@data", dtpData.Value.Date);
                    cmd.Parameters.AddWithValue("@hora", txtHour.Text);
                    cmd.Parameters.AddWithValue("@ativa", chkActive.Checked);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sessão registrada com sucesso!");

                    LoadSalas();
                    LoadFilmes();
                    LoadCinemas();
                    ShowDataOnGridView();
                    ClearAllData();
                    GenerateNewID();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar a sessão: " + ex.Message);
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