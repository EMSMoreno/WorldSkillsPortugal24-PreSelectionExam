﻿namespace WSN24_EduardoMoreno_M3
{
    partial class FormEditarSessao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditarSessao));
            this.lblID = new System.Windows.Forms.Label();
            this.txtIDSessao = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdateMovie = new System.Windows.Forms.Button();
            this.dgViewSessions = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Close = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.cinemasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.listarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.salasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.listarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.sessõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.listarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.filmesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registarToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.listarToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.tiposDeFilmeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registarToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.editarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apagarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbFilme = new System.Windows.Forms.ComboBox();
            this.lblIDFilme = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.cbCinema = new System.Windows.Forms.ComboBox();
            this.dtpData = new System.Windows.Forms.DateTimePicker();
            this.txtHour = new System.Windows.Forms.TextBox();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSala = new System.Windows.Forms.ComboBox();
            this.lblCodigoSala = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewSessions)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(185, 210);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(59, 13);
            this.lblID.TabIndex = 89;
            this.lblID.Text = "ID Sessão:";
            // 
            // txtIDSessao
            // 
            this.txtIDSessao.Enabled = false;
            this.txtIDSessao.Location = new System.Drawing.Point(244, 207);
            this.txtIDSessao.Name = "txtIDSessao";
            this.txtIDSessao.Size = new System.Drawing.Size(41, 20);
            this.txtIDSessao.TabIndex = 88;
            // 
            // btnCancel
            // 
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCancel.Location = new System.Drawing.Point(704, 249);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(192, 30);
            this.btnCancel.TabIndex = 87;
            this.btnCancel.Text = "Cancelar Operação";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdateMovie
            // 
            this.btnUpdateMovie.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnUpdateMovie.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.btnUpdateMovie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnUpdateMovie.Location = new System.Drawing.Point(704, 212);
            this.btnUpdateMovie.Name = "btnUpdateMovie";
            this.btnUpdateMovie.Size = new System.Drawing.Size(192, 30);
            this.btnUpdateMovie.TabIndex = 84;
            this.btnUpdateMovie.Text = "Editar Sessão";
            this.btnUpdateMovie.UseVisualStyleBackColor = false;
            this.btnUpdateMovie.Click += new System.EventHandler(this.btnUpdateSessao_Click);
            // 
            // dgViewSessions
            // 
            this.dgViewSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewSessions.Location = new System.Drawing.Point(168, 289);
            this.dgViewSessions.Name = "dgViewSessions";
            this.dgViewSessions.Size = new System.Drawing.Size(728, 201);
            this.dgViewSessions.TabIndex = 83;
            this.dgViewSessions.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgViewSessions_CellContentClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(179, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 24);
            this.label6.TabIndex = 82;
            this.label6.Text = "Editar Sessão";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(178, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 29);
            this.label5.TabIndex = 81;
            this.label5.Text = "Sessões";
            // 
            // Close
            // 
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(819, 12);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 47);
            this.Close.TabIndex = 80;
            this.Close.Text = "Fechar Janela";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.SteelBlue;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem10,
            this.toolStripMenuItem5,
            this.cinemasToolStripMenuItem,
            this.toolStripMenuItem6,
            this.salasToolStripMenuItem,
            this.toolStripMenuItem7,
            this.sessõesToolStripMenuItem,
            this.toolStripMenuItem8,
            this.filmesToolStripMenuItem,
            this.toolStripMenuItem9,
            this.tiposDeFilmeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(156, 537);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 79;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem2.Text = "                       ";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem3.Text = "                        ";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem4.Text = "                 ";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13});
            this.toolStripMenuItem10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(152, 25);
            this.toolStripMenuItem10.Text = "Locais";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItem11.Text = "Registar";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItem12.Text = "Editar";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(180, 26);
            this.toolStripMenuItem13.Text = "Apagar";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem5.Text = "                  ";
            // 
            // cinemasToolStripMenuItem
            // 
            this.cinemasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registarToolStripMenuItem,
            this.editarToolStripMenuItem1,
            this.listarToolStripMenuItem});
            this.cinemasToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cinemasToolStripMenuItem.Name = "cinemasToolStripMenuItem";
            this.cinemasToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.cinemasToolStripMenuItem.Text = "Cinemas";
            // 
            // registarToolStripMenuItem
            // 
            this.registarToolStripMenuItem.Name = "registarToolStripMenuItem";
            this.registarToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.registarToolStripMenuItem.Text = "Registar";
            // 
            // editarToolStripMenuItem1
            // 
            this.editarToolStripMenuItem1.Name = "editarToolStripMenuItem1";
            this.editarToolStripMenuItem1.Size = new System.Drawing.Size(142, 26);
            this.editarToolStripMenuItem1.Text = "Editar";
            // 
            // listarToolStripMenuItem
            // 
            this.listarToolStripMenuItem.Name = "listarToolStripMenuItem";
            this.listarToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.listarToolStripMenuItem.Text = "Listar";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem6.Text = "                ";
            // 
            // salasToolStripMenuItem
            // 
            this.salasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registarToolStripMenuItem1,
            this.editarToolStripMenuItem2,
            this.listarToolStripMenuItem1});
            this.salasToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.salasToolStripMenuItem.Name = "salasToolStripMenuItem";
            this.salasToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.salasToolStripMenuItem.Text = "Salas";
            // 
            // registarToolStripMenuItem1
            // 
            this.registarToolStripMenuItem1.Name = "registarToolStripMenuItem1";
            this.registarToolStripMenuItem1.Size = new System.Drawing.Size(142, 26);
            this.registarToolStripMenuItem1.Text = "Registar";
            // 
            // editarToolStripMenuItem2
            // 
            this.editarToolStripMenuItem2.Name = "editarToolStripMenuItem2";
            this.editarToolStripMenuItem2.Size = new System.Drawing.Size(142, 26);
            this.editarToolStripMenuItem2.Text = "Editar";
            // 
            // listarToolStripMenuItem1
            // 
            this.listarToolStripMenuItem1.Name = "listarToolStripMenuItem1";
            this.listarToolStripMenuItem1.Size = new System.Drawing.Size(142, 26);
            this.listarToolStripMenuItem1.Text = "Listar";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem7.Text = "              ";
            // 
            // sessõesToolStripMenuItem
            // 
            this.sessõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registarToolStripMenuItem2,
            this.editarToolStripMenuItem3,
            this.listarToolStripMenuItem2});
            this.sessõesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessõesToolStripMenuItem.Name = "sessõesToolStripMenuItem";
            this.sessõesToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.sessõesToolStripMenuItem.Text = "Sessões";
            // 
            // registarToolStripMenuItem2
            // 
            this.registarToolStripMenuItem2.Name = "registarToolStripMenuItem2";
            this.registarToolStripMenuItem2.Size = new System.Drawing.Size(142, 26);
            this.registarToolStripMenuItem2.Text = "Registar";
            // 
            // editarToolStripMenuItem3
            // 
            this.editarToolStripMenuItem3.Name = "editarToolStripMenuItem3";
            this.editarToolStripMenuItem3.Size = new System.Drawing.Size(142, 26);
            this.editarToolStripMenuItem3.Text = "Editar";
            // 
            // listarToolStripMenuItem2
            // 
            this.listarToolStripMenuItem2.Name = "listarToolStripMenuItem2";
            this.listarToolStripMenuItem2.Size = new System.Drawing.Size(142, 26);
            this.listarToolStripMenuItem2.Text = "Listar";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem8.Text = "                ";
            // 
            // filmesToolStripMenuItem
            // 
            this.filmesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registarToolStripMenuItem3,
            this.editarToolStripMenuItem4,
            this.listarToolStripMenuItem3});
            this.filmesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filmesToolStripMenuItem.Name = "filmesToolStripMenuItem";
            this.filmesToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.filmesToolStripMenuItem.Text = "Filmes";
            // 
            // registarToolStripMenuItem3
            // 
            this.registarToolStripMenuItem3.Name = "registarToolStripMenuItem3";
            this.registarToolStripMenuItem3.Size = new System.Drawing.Size(142, 26);
            this.registarToolStripMenuItem3.Text = "Registar";
            // 
            // editarToolStripMenuItem4
            // 
            this.editarToolStripMenuItem4.Name = "editarToolStripMenuItem4";
            this.editarToolStripMenuItem4.Size = new System.Drawing.Size(142, 26);
            this.editarToolStripMenuItem4.Text = "Editar";
            // 
            // listarToolStripMenuItem3
            // 
            this.listarToolStripMenuItem3.Name = "listarToolStripMenuItem3";
            this.listarToolStripMenuItem3.Size = new System.Drawing.Size(142, 26);
            this.listarToolStripMenuItem3.Text = "Apagar";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(152, 19);
            this.toolStripMenuItem9.Text = "                ";
            // 
            // tiposDeFilmeToolStripMenuItem
            // 
            this.tiposDeFilmeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.registarToolStripMenuItem4,
            this.editarToolStripMenuItem,
            this.apagarToolStripMenuItem});
            this.tiposDeFilmeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.tiposDeFilmeToolStripMenuItem.Name = "tiposDeFilmeToolStripMenuItem";
            this.tiposDeFilmeToolStripMenuItem.Size = new System.Drawing.Size(152, 25);
            this.tiposDeFilmeToolStripMenuItem.Text = "Tipos de Filme";
            // 
            // registarToolStripMenuItem4
            // 
            this.registarToolStripMenuItem4.Name = "registarToolStripMenuItem4";
            this.registarToolStripMenuItem4.Size = new System.Drawing.Size(142, 26);
            this.registarToolStripMenuItem4.Text = "Registar";
            // 
            // editarToolStripMenuItem
            // 
            this.editarToolStripMenuItem.Name = "editarToolStripMenuItem";
            this.editarToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.editarToolStripMenuItem.Text = "Editar";
            // 
            // apagarToolStripMenuItem
            // 
            this.apagarToolStripMenuItem.Name = "apagarToolStripMenuItem";
            this.apagarToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.apagarToolStripMenuItem.Text = "Apagar";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SteelBlue;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(155, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 72);
            this.panel1.TabIndex = 78;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Registar Filme";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filmes";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Cinemas Skillianos";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // cbFilme
            // 
            this.cbFilme.Enabled = false;
            this.cbFilme.FormattingEnabled = true;
            this.cbFilme.Location = new System.Drawing.Point(244, 233);
            this.cbFilme.Name = "cbFilme";
            this.cbFilme.Size = new System.Drawing.Size(205, 21);
            this.cbFilme.TabIndex = 98;
            // 
            // lblIDFilme
            // 
            this.lblIDFilme.AutoSize = true;
            this.lblIDFilme.Location = new System.Drawing.Point(191, 236);
            this.lblIDFilme.Name = "lblIDFilme";
            this.lblIDFilme.Size = new System.Drawing.Size(48, 13);
            this.lblIDFilme.TabIndex = 97;
            this.lblIDFilme.Text = "ID Filme:";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.BackColor = System.Drawing.SystemColors.ControlLight;
            this.chkActive.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkActive.Location = new System.Drawing.Point(570, 238);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(104, 17);
            this.chkActive.TabIndex = 96;
            this.chkActive.Text = "Sessão É Ativa?";
            this.chkActive.UseVisualStyleBackColor = false;
            // 
            // cbCinema
            // 
            this.cbCinema.Enabled = false;
            this.cbCinema.FormattingEnabled = true;
            this.cbCinema.Location = new System.Drawing.Point(244, 262);
            this.cbCinema.Name = "cbCinema";
            this.cbCinema.Size = new System.Drawing.Size(205, 21);
            this.cbCinema.TabIndex = 95;
            // 
            // dtpData
            // 
            this.dtpData.Location = new System.Drawing.Point(498, 206);
            this.dtpData.Name = "dtpData";
            this.dtpData.Size = new System.Drawing.Size(158, 20);
            this.dtpData.TabIndex = 94;
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(498, 236);
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(66, 20);
            this.txtHour.TabIndex = 93;
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Location = new System.Drawing.Point(459, 239);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(33, 13);
            this.lblHora.TabIndex = 92;
            this.lblHora.Text = "Hora:";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(459, 212);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(33, 13);
            this.lblData.TabIndex = 91;
            this.lblData.Text = "Data:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(179, 265);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 90;
            this.label4.Text = "ID Cinema:";
            // 
            // cbSala
            // 
            this.cbSala.Enabled = false;
            this.cbSala.FormattingEnabled = true;
            this.cbSala.Location = new System.Drawing.Point(383, 207);
            this.cbSala.Name = "cbSala";
            this.cbSala.Size = new System.Drawing.Size(66, 21);
            this.cbSala.TabIndex = 100;
            // 
            // lblCodigoSala
            // 
            this.lblCodigoSala.AutoSize = true;
            this.lblCodigoSala.Location = new System.Drawing.Point(296, 210);
            this.lblCodigoSala.Name = "lblCodigoSala";
            this.lblCodigoSala.Size = new System.Drawing.Size(81, 13);
            this.lblCodigoSala.TabIndex = 99;
            this.lblCodigoSala.Text = "ID Código Sala:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Location = new System.Drawing.Point(155, 503);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(741, 34);
            this.panel2.TabIndex = 101;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(117, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(458, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "@2024 WorldSkills Portugal Pre-Selection Exam -  Portugal, May 2024";
            // 
            // FormEditarSessao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 537);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbSala);
            this.Controls.Add(this.lblCodigoSala);
            this.Controls.Add(this.cbFilme);
            this.Controls.Add(this.lblIDFilme);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.cbCinema);
            this.Controls.Add(this.dtpData);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.lblHora);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.txtIDSessao);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnUpdateMovie);
            this.Controls.Add(this.dgViewSessions);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEditarSessao";
            this.Text = "FormEditarSessao";
            ((System.ComponentModel.ISupportInitialize)(this.dgViewSessions)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtIDSessao;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdateMovie;
        private System.Windows.Forms.DataGridView dgViewSessions;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem cinemasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem listarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem salasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem listarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem sessõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registarToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem listarToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem filmesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registarToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem listarToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem tiposDeFilmeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registarToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem editarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apagarToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbFilme;
        private System.Windows.Forms.Label lblIDFilme;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.ComboBox cbCinema;
        private System.Windows.Forms.DateTimePicker dtpData;
        private System.Windows.Forms.TextBox txtHour;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSala;
        private System.Windows.Forms.Label lblCodigoSala;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
    }
}