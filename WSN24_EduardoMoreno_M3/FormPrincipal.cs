using System;
using System.Windows.Forms;
using WSN24_EduardoMoreno_M3.Cinema;
using WSN24_EduardoMoreno_M3.Local;
using WSN24_EduardoMoreno_M3.TipoFilme;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

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
            using (var formRegistoFilme = new FormRegistoSala())
            { formRegistoFilme.ShowDialog(); }
        }

        #endregion

        // neste momento estou a trabalhar aqui

        #region MenuStrip - Sessões

        private void registarToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            using (var formRegistoSessao = new FormRegistoSessao())
            { formRegistoSessao.ShowDialog(); }
        }

        #endregion



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
