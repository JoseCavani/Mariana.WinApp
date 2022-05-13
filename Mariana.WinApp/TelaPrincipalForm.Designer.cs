namespace Mariana.WinApp
{
    partial class TelaPrincipalForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TelaPrincipalForm));
            this.menu = new System.Windows.Forms.MenuStrip();
            this.cadastrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TesteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disciplinaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materiaMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbox = new System.Windows.Forms.ToolStrip();
            this.btnInserir = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonDuplicar = new System.Windows.Forms.ToolStripButton();
            this.btnAtualizarQuestoes = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonGabarito = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPDF = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.labelTipoCadastro = new System.Windows.Forms.ToolStripLabel();
            this.panelRegistros = new System.Windows.Forms.Panel();
            this.statusStripRodape = new System.Windows.Forms.StatusStrip();
            this.labelRodape = new System.Windows.Forms.ToolStripStatusLabel();
            this.menu.SuspendLayout();
            this.toolbox.SuspendLayout();
            this.statusStripRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cadastrosToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(800, 24);
            this.menu.TabIndex = 1;
            this.menu.Text = "menuStrip1";
            // 
            // cadastrosToolStripMenuItem
            // 
            this.cadastrosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TesteMenuItem,
            this.disciplinaMenuItem,
            this.materiaMenuItem});
            this.cadastrosToolStripMenuItem.Name = "cadastrosToolStripMenuItem";
            this.cadastrosToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.cadastrosToolStripMenuItem.Text = "Cadastros";
            // 
            // TesteMenuItem
            // 
            this.TesteMenuItem.Name = "TesteMenuItem";
            this.TesteMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.TesteMenuItem.Size = new System.Drawing.Size(144, 22);
            this.TesteMenuItem.Text = "Teste";
            this.TesteMenuItem.Click += new System.EventHandler(this.TesteMenuItem_Click);
            // 
            // disciplinaMenuItem
            // 
            this.disciplinaMenuItem.Name = "disciplinaMenuItem";
            this.disciplinaMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.disciplinaMenuItem.Size = new System.Drawing.Size(144, 22);
            this.disciplinaMenuItem.Text = "Disciplina";
            this.disciplinaMenuItem.Click += new System.EventHandler(this.disciplinaMenuItem_Click);
            // 
            // materiaMenuItem
            // 
            this.materiaMenuItem.Name = "materiaMenuItem";
            this.materiaMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.materiaMenuItem.Size = new System.Drawing.Size(144, 22);
            this.materiaMenuItem.Text = "Materia";
            this.materiaMenuItem.Click += new System.EventHandler(this.materiaMenuItem_Click);
            // 
            // toolbox
            // 
            this.toolbox.Enabled = false;
            this.toolbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInserir,
            this.btnEditar,
            this.btnExcluir,
            this.toolStripSeparator1,
            this.toolStripButtonDuplicar,
            this.btnAtualizarQuestoes,
            this.toolStripButtonGabarito,
            this.toolStripButtonPDF,
            this.toolStripSeparator2,
            this.labelTipoCadastro});
            this.toolbox.Location = new System.Drawing.Point(0, 24);
            this.toolbox.Name = "toolbox";
            this.toolbox.Size = new System.Drawing.Size(800, 41);
            this.toolbox.TabIndex = 2;
            this.toolbox.Text = "toolStrip1";
            // 
            // btnInserir
            // 
            this.btnInserir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnInserir.Image = global::Mariana.WinApp.Properties.Resources.outline_add_circle_outline_black_24dp1;
            this.btnInserir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInserir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Padding = new System.Windows.Forms.Padding(5);
            this.btnInserir.Size = new System.Drawing.Size(38, 38);
            this.btnInserir.Text = "Inserir";
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::Mariana.WinApp.Properties.Resources.outline_mode_edit_black_24dp1;
            this.btnEditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Padding = new System.Windows.Forms.Padding(5);
            this.btnEditar.Size = new System.Drawing.Size(38, 38);
            this.btnEditar.Text = "Editar";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExcluir.Image = global::Mariana.WinApp.Properties.Resources.outline_delete_black_24dp1;
            this.btnExcluir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Padding = new System.Windows.Forms.Padding(5);
            this.btnExcluir.Size = new System.Drawing.Size(38, 38);
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // toolStripButtonDuplicar
            // 
            this.toolStripButtonDuplicar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDuplicar.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDuplicar.Image")));
            this.toolStripButtonDuplicar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDuplicar.Name = "toolStripButtonDuplicar";
            this.toolStripButtonDuplicar.Size = new System.Drawing.Size(23, 38);
            this.toolStripButtonDuplicar.Text = "toolStripButton1";
            this.toolStripButtonDuplicar.Click += new System.EventHandler(this.toolStripButtonDuplicar_Click);
            // 
            // btnAtualizarQuestoes
            // 
            this.btnAtualizarQuestoes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAtualizarQuestoes.Image = global::Mariana.WinApp.Properties.Resources.outline_list_alt_black_24dp1;
            this.btnAtualizarQuestoes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAtualizarQuestoes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAtualizarQuestoes.Name = "btnAtualizarQuestoes";
            this.btnAtualizarQuestoes.Padding = new System.Windows.Forms.Padding(5);
            this.btnAtualizarQuestoes.Size = new System.Drawing.Size(38, 38);
            this.btnAtualizarQuestoes.Click += new System.EventHandler(this.btnAtualizarQuestoes_Click);
            // 
            // toolStripButtonGabarito
            // 
            this.toolStripButtonGabarito.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGabarito.Image = global::Mariana.WinApp.Properties.Resources.check_box_FILL0_wght400_GRAD0_opsz243;
            this.toolStripButtonGabarito.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButtonGabarito.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGabarito.Name = "toolStripButtonGabarito";
            this.toolStripButtonGabarito.Size = new System.Drawing.Size(28, 38);
            this.toolStripButtonGabarito.Text = "toolStripButton1";
            this.toolStripButtonGabarito.Click += new System.EventHandler(this.toolStripButtonGabarito_Click);
            // 
            // toolStripButtonPDF
            // 
            this.toolStripButtonPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPDF.Image = global::Mariana.WinApp.Properties.Resources.file_download_FILL0_wght400_GRAD0_opsz481;
            this.toolStripButtonPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPDF.Name = "toolStripButtonPDF";
            this.toolStripButtonPDF.Size = new System.Drawing.Size(23, 38);
            this.toolStripButtonPDF.Text = "toolStripButtonPDF";
            this.toolStripButtonPDF.Click += new System.EventHandler(this.toolStripButtonPDF_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // labelTipoCadastro
            // 
            this.labelTipoCadastro.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.labelTipoCadastro.Name = "labelTipoCadastro";
            this.labelTipoCadastro.Size = new System.Drawing.Size(90, 38);
            this.labelTipoCadastro.Text = "[tipoCadastro]";
            // 
            // panelRegistros
            // 
            this.panelRegistros.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRegistros.Location = new System.Drawing.Point(0, 65);
            this.panelRegistros.Name = "panelRegistros";
            this.panelRegistros.Size = new System.Drawing.Size(800, 363);
            this.panelRegistros.TabIndex = 5;
            // 
            // statusStripRodape
            // 
            this.statusStripRodape.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelRodape});
            this.statusStripRodape.Location = new System.Drawing.Point(0, 428);
            this.statusStripRodape.Name = "statusStripRodape";
            this.statusStripRodape.Size = new System.Drawing.Size(800, 22);
            this.statusStripRodape.TabIndex = 4;
            this.statusStripRodape.Text = "Rodape";
            // 
            // labelRodape
            // 
            this.labelRodape.Name = "labelRodape";
            this.labelRodape.Size = new System.Drawing.Size(52, 17);
            this.labelRodape.Text = "[rodapé]";
            // 
            // TelaPrincipalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelRegistros);
            this.Controls.Add(this.statusStripRodape);
            this.Controls.Add(this.toolbox);
            this.Controls.Add(this.menu);
            this.Name = "TelaPrincipalForm";
            this.Text = "Gerador de testes";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.toolbox.ResumeLayout(false);
            this.toolbox.PerformLayout();
            this.statusStripRodape.ResumeLayout(false);
            this.statusStripRodape.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem cadastrosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TesteMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disciplinaMenuItem;
        private System.Windows.Forms.ToolStrip toolbox;
        private System.Windows.Forms.ToolStripButton btnInserir;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnExcluir;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel labelTipoCadastro;
        private System.Windows.Forms.Panel panelRegistros;
        private System.Windows.Forms.StatusStrip statusStripRodape;
        private System.Windows.Forms.ToolStripStatusLabel labelRodape;
        private System.Windows.Forms.ToolStripMenuItem materiaMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnAtualizarQuestoes;
        private System.Windows.Forms.ToolStripButton toolStripButtonGabarito;
        private System.Windows.Forms.ToolStripButton toolStripButtonPDF;
        private System.Windows.Forms.ToolStripButton toolStripButtonDuplicar;
    }
}
