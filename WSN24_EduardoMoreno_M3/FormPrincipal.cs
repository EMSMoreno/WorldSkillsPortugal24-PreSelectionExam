using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WSN24_EduardoMoreno_M3.TipoFilme;

namespace WSN24_EduardoMoreno_M3
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        } // Fechar

        #region MenuStrip - Cinemas

        #endregion


        #region MenuStrip - Salas

        #endregion


        #region MenuStrip - Sessões

        #endregion


        #region MenuStrip - Filmes

        private void registarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var formRegistoFilme = new FormRegistoFilme())
            { formRegistoFilme.ShowDialog(); }
        }

        private void editarToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            using (var formEditarFilme = new FormEditarFilme())
            { formEditarFilme.ShowDialog(); }
        }

        private void listarToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            using (var formListarFilme = new FormListarFilme())
            { formListarFilme.ShowDialog(); }
        }

        #endregion

        // neste momento estou a trabalhar aqui

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
    }
}
