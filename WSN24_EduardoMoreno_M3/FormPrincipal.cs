using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using WSN24_EduardoMoreno_M3.Cinema;
using WSN24_EduardoMoreno_M3.Local;
using WSN24_EduardoMoreno_M3.Sala;
using WSN24_EduardoMoreno_M3.TipoFilme;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormPrincipal : Form
    {
        private string lastSelectedSala;
        private string lastSelectedCinema;
        private string lastSelectedSessao;
        private string lastSelectedFilme;
        private string lastSelectedTipoFilme;
        private string lastSelectedLocal;

        public FormPrincipal()
        {
            InitializeComponent();
            InitializeUI("UIMode"); // Dark Mode
        }

        #region Dark Mode

        private void InitializeUI(string key)
        {
            try
            {
                var uiMode = ConfigurationManager.AppSettings[key];
                if (uiMode == "light")
                {
                    ApplyLightMode();
                }
                else
                {
                    ApplyDarkMode();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao inicializar o modo de UI: " + ex.Message);
            }
        }

        private void ApplyLightMode()
        {
            btnDarkMode.Text = "Enable Dark Mode";
            this.ForeColor = Color.FromArgb(3, 0, 10);
            this.BackColor = Color.FromArgb(245, 247, 246);
            UpdatePanelSkillsTextColor();
        }

        private void ApplyDarkMode()
        {
            btnDarkMode.Text = "Enable Light Mode";
            this.ForeColor = Color.FromArgb(245, 247, 246);
            this.BackColor = Color.FromArgb(3, 0, 10);
            UpdatePanelSkillsTextColor();
        }

        private void UpdatePanelSkillsTextColor()
        { 
            if (panelSkills.BackColor == Color.FromArgb(3, 0, 10))
            {
                lblSkills.ForeColor = Color.White;
            }
            else
            {
                lblSkills.ForeColor = Color.Black;
            }
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            var key = "UIMode";
            var uiMode = ConfigurationManager.AppSettings[key];

            if (uiMode == "light")
            {
                ConfigurationManager.AppSettings[key] = "dark";
                SaveConfiguration(key, "dark");
                ApplyDarkMode();
            }
            else
            {
                ConfigurationManager.AppSettings[key] = "light";
                SaveConfiguration(key, "light");
                ApplyLightMode();
            }
        }

        #endregion

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Log Out & Guardar Dados do Utilizador na Sessão

        private void SaveConfiguration(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void SaveUserPreferences()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            AppSettingsSection appSettings = (AppSettingsSection)config.GetSection("appSettings");

            // Atualizar as configurações
            appSettings.Settings["LastSelectedSala"].Value = lastSelectedSala ?? string.Empty;
            appSettings.Settings["LastSelectedCinema"].Value = lastSelectedCinema ?? string.Empty;
            appSettings.Settings["LastSelectedSessao"].Value = lastSelectedSessao ?? string.Empty;
            appSettings.Settings["LastSelectedFilme"].Value = lastSelectedFilme ?? string.Empty;
            appSettings.Settings["LastSelectedTipoFilme"].Value = lastSelectedTipoFilme ?? string.Empty;
            appSettings.Settings["LastSelectedLocal"].Value = lastSelectedLocal ?? string.Empty;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void LoadUserPreferences()
        {
            lastSelectedSala = ConfigurationManager.AppSettings["LastSelectedSala"];
            lastSelectedCinema = ConfigurationManager.AppSettings["LastSelectedCinema"];
            lastSelectedSessao = ConfigurationManager.AppSettings["LastSelectedSessao"];
            lastSelectedFilme = ConfigurationManager.AppSettings["LastSelectedFilme"];
            lastSelectedTipoFilme = ConfigurationManager.AppSettings["LastSelectedTipoFilme"];
            lastSelectedLocal = ConfigurationManager.AppSettings["LastSelectedLocal"];
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Tens a certeza que queres sair da tua sessão dos Cinemas Skillianos?",
                "Confirmar Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            // Se "Sim"
            if (result == DialogResult.Yes)
            {
                UserSession.Logout();

                Form loginForm = new FormLogin();
                loginForm.Show();

                this.Close();
            }
        }

        #endregion


        #region MenuStrip - Local

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            using (var formRegistoLocal = new FormRegistoLocal())
            { formRegistoLocal.ShowDialog(); }
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            using (var formEditarLocal = new FormEditarLocal())
            { formEditarLocal.ShowDialog(); }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            using (var formApagarLocal = new FormApagarLocal())
            { formApagarLocal.ShowDialog(); }
        }

        #endregion

        // ✅

        #region MenuStrip - Cinemas

        private void registarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formRegistoCinema = new FormRegistoCinema())
            { formRegistoCinema.ShowDialog(); }
        }

        private void editarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var formEditarCinema = new FormEditarCinema())
            { formEditarCinema.ShowDialog(); }
        }

        private void listarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formApagarCinema = new FormApagarCinema())
            { formApagarCinema.ShowDialog(); }
        }

        #endregion

        // ✅

        #region MenuStrip - Salas

        private void registarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var formRegistoSala = new FormRegistoSala())
            { formRegistoSala.ShowDialog(); }
        }

        private void editarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var formEditarSala = new FormEditarSala())
            { formEditarSala.ShowDialog(); }
        }

        private void listarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (var formApagarSala = new FormApagarSala())
            { formApagarSala.ShowDialog(); }
        }

        #endregion

        // ✅

        #region MenuStrip - Sessões

        private void registarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var formRegistoSessao = new FormRegistoSessao())
            { formRegistoSessao.ShowDialog(); }
        }

        private void editarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var formEditarSessao = new FormEditarSessao())
            { formEditarSessao.ShowDialog(); }
        }

        private void listarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var formApagarSessao = new FormApagarSessao())
            { formApagarSessao.ShowDialog(); }
        }

        #endregion

        // ✅

        #region MenuStrip - Filmes

        private void registarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var formEditarFilme = new FormRegistoFilme())
            { formEditarFilme.ShowDialog(); }
        }

        private void editarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            using (var formEditarFilme = new FormEditarFilme())
            { formEditarFilme.ShowDialog(); }
        }

        private void listarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var formListarFilme = new FormApagarFilme())
            { formListarFilme.ShowDialog(); }
        }

        #endregion 

        // ✅

        #region MenuStrip - Tipos de Filme

        private void registarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            using (var formListarTipoFilme = new FormRegistoTipoFilme())
            { formListarTipoFilme.ShowDialog(); }
        }

        private void apagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formApagarTipoFilme = new FormApagarTipoFilme())
            { formApagarTipoFilme.ShowDialog(); }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var formEditarTipoFilme = new FormEditarTipoFilme())
            { formEditarTipoFilme.ShowDialog(); }
        }

        #endregion

        // ✅
    }
}
