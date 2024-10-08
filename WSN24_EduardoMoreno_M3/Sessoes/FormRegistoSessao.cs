﻿using System;
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
        }

        #region Métodos

        private void FormRegistoSessão_Load(object sender, EventArgs e)
        {
            string role = UserSession.Role;

            if (string.IsNullOrEmpty(role))
            {
                HideEditingControls(); 
                ShowPermissionLabel();
            }
            else if (role.ToLower() == "admin")
            {
                ShowAllControls();
            }
            else if (role.ToLower() == "coordenador")
            {
                ShowAllControls();
                DisableEditingControls();
            }
            else
            {
                ShowViewOnlyControls();
            }

            LoadSalas();
            LoadFilmes();
            LoadCinemas();
            ShowDataOnGridView();
            GenerateNewID();
        }

        private void HideEditingControls()
        {
            cbCinema.Visible = false;
            cbCinema.Enabled = false;
            cbFilme.Visible = false;
            cbFilme.Enabled = false;
            cbSala.Visible = false;
            cbSala.Enabled = false;
            dtpData.Visible = false;
            dtpData.Enabled = false;
            txtIDSessao.Visible = false;
            txtIDSessao.Enabled = false;
            btnRegistoTipoFilme.Visible = false;
            btnCancel.Visible = false;

            //Extra
            txtHour.Visible = false;
            txtHour.Enabled = false;
            chkActive.Visible = false;
            chkActive.Enabled = false;
        }

        private void ShowPermissionLabel()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.ForeColor == System.Drawing.Color.Red)
                {
                    this.Controls.Remove(control);
                }
            }

            Label lblPermission1 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres os IDs dos Cinemas associados às Sessões.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(187, 216)
            };
            this.Controls.Add(lblPermission1);

            Label lblPermission2 = new Label
            {
                Text = "Não tens permissões para \n " +
                "veres os IDs das Sessões.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(187, 272)
            };
            this.Controls.Add(lblPermission2);

            Label lblPermission3 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres os IDs dos Filmes.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(354, 272)
            };
            this.Controls.Add(lblPermission3);

            Label lblPermission4 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres as Datas das Sessões.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(354, 320)
            };
            this.Controls.Add(lblPermission4);

            Label lblPermission5 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres os IDs das Salas.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(186, 318)
            };
            this.Controls.Add(lblPermission5);

            Label lblPermission6 = new Label
            {
                Text = "Não tens permissões para \n" +
                "veres as Horas das Sessões.",
                AutoSize = true,
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(187, 365)
            };
            this.Controls.Add(lblPermission6);
        }

        private void ShowAllControls()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = true;
                control.Enabled = true;
            }

            cbCinema.Enabled = true;
            cbFilme.Enabled = true;
            cbSala.Enabled = true;
            dtpData.Enabled = true;
            txtIDSessao.Enabled = true;
            btnClose.Enabled = true;
        }

        private void DisableEditingControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox && control != txtSearchSessões)
                {
                    control.Enabled = false;
                }
                else if (control is ComboBox || control == btnClose)
                {
                    control.Enabled = true;
                }
                else if (control is Button && control != btnSearchSessões)
                {
                    control.Enabled = false;
                }
            }
        }

        private void ShowViewOnlyControls()
        {
            foreach (Control control in this.Controls)
            {
                if (control == txtSearchSessões || control == btnSearchSessões || control is DataGridView)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else if (control is ComboBox)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else if (control == btnClose)
                {
                    control.Visible = true;
                    control.Enabled = true;
                }
                else if (control is TextBox || control is Button)
                {
                    control.Visible = false;
                    control.Enabled = false;
                }
            }
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
                       c.nome AS 'Cinema',
                       s.data AS 'Data',
                       s.hora AS 'Hora',
                       s.ativa AS 'Ativa'
                FROM Sessao s
                JOIN Sala sa ON s.codigo_sala = sa.codigo_sala
                JOIN Filme f ON s.codigo_filme = f.codigo_filme
                JOIN Cinema c ON s.id_cinema = c.id_cinema";

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
                  AND id_cinema = @id_cinema 
                  AND data = @data 
                  AND hora = @hora";

                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@codigo_sala", cbSala.SelectedValue);
                    cmd.Parameters.AddWithValue("@codigo_filme", cbFilme.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_cinema", cbCinema.SelectedValue);
                    cmd.Parameters.AddWithValue("@data", dtpData.Value.Date);
                    cmd.Parameters.AddWithValue("@hora", txtHour.Text);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao verificar duplicação de sessões com os mesmos dados: " + ex.Message);
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

        private void SearchSessoes(string searchTerm)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();
                    
                    SqlCommand cmd = new SqlCommand(@"
                    SELECT s.id_sessao AS 'ID Sessão',
                           sa.descricao AS 'Sala',
                           f.nome AS 'Filme',
                           c.nome AS 'Cinema',
                           CONVERT(VARCHAR, s.data, 105) AS 'Data',
                           s.hora AS 'Hora',
                           s.ativa AS 'Ativa'
                    FROM Sessao s
                    JOIN Sala sa ON s.codigo_sala = sa.codigo_sala
                    JOIN Filme f ON s.codigo_filme = f.codigo_filme
                    JOIN Cinema c ON s.id_cinema = c.id_cinema
                    WHERE sa.descricao LIKE @searchTerm 
                       OR f.nome LIKE @searchTerm 
                       OR c.nome LIKE @searchTerm 
                       OR CONVERT(VARCHAR, s.data, 105) LIKE @searchTerm 
                       OR s.hora LIKE @searchTerm", con);

                    cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewSearchSessões.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma Sessão encontrada, com base naquilo que pesquisaste.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao pesquisar Sessões: " + ex.Message);
            }
        }

        #endregion

        #region UI

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIDSessao.Text) ||
                cbSala.SelectedValue == null ||
                cbFilme.SelectedValue == null ||
                cbCinema.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtHour.Text))
            {
                MessageBox.Show("Preenche todos os campos antes de adicionar uma nova sessão!");
                return;
            }

            TimeSpan hora;
            if (!TimeSpan.TryParse(txtHour.Text, out hora))
            {
                MessageBox.Show("O formato da hora deve ser hh:mm.");
                return;
            }

            // Vai ver se sessão é duplicada
            if (IsSessionDuplicate())
            {
                MessageBox.Show("Já existe uma sessão com esse filme na mesma sala, cinema, data ou hora, cuidado com isso!");
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
                    MessageBox.Show("Sessão registada com sucesso!");

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
                MessageBox.Show("Existe um erro ao registar a tua sessão: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAllData();
        }

        private void btnSearchSessões_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearchSessões.Text.Trim();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                MessageBox.Show("Escreve a Sessão que procuras.");
                txtSearchSessões.Text = "";
                return;
            }

            SearchSessoes(searchTerm);
            txtSearchSessões.Text = "";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        
    }
}