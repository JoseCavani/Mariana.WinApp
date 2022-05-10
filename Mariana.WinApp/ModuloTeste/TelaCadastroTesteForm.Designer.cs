
namespace Mariana.WinApp.ModuloTeste
{
    partial class TelaCadastroTesteForm
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
            this.comboBoxDisciplina = new System.Windows.Forms.ComboBox();
            this.labelDisciplina = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.labelMateria = new System.Windows.Forms.Label();
            this.labelQuantidadeQuestoes = new System.Windows.Forms.Label();
            this.numericUpDownQuantidade = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantidade)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxDisciplina
            // 
            this.comboBoxDisciplina.FormattingEnabled = true;
            this.comboBoxDisciplina.Location = new System.Drawing.Point(83, 68);
            this.comboBoxDisciplina.Name = "comboBoxDisciplina";
            this.comboBoxDisciplina.Size = new System.Drawing.Size(121, 23);
            this.comboBoxDisciplina.TabIndex = 31;
            this.comboBoxDisciplina.SelectedIndexChanged += new System.EventHandler(this.comboBoxDisciplina_SelectedIndexChanged);
            // 
            // labelDisciplina
            // 
            this.labelDisciplina.AutoSize = true;
            this.labelDisciplina.Location = new System.Drawing.Point(5, 68);
            this.labelDisciplina.Name = "labelDisciplina";
            this.labelDisciplina.Size = new System.Drawing.Size(61, 15);
            this.labelDisciplina.TabIndex = 30;
            this.labelDisciplina.Text = "Disciplina:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(414, 170);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 39);
            this.btnCancelar.TabIndex = 27;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(336, 170);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(72, 39);
            this.btnGravar.TabIndex = 26;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // txtTitulo
            // 
            this.txtTitulo.Location = new System.Drawing.Point(83, 39);
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(333, 23);
            this.txtTitulo.TabIndex = 25;
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(83, 13);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(64, 23);
            this.txtNumero.TabIndex = 24;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Location = new System.Drawing.Point(5, 42);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(40, 15);
            this.labelTitulo.TabIndex = 23;
            this.labelTitulo.Text = "Titulo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Número:";
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Items.AddRange(new object[] {
            "Todos"});
            this.comboBoxMateria.Location = new System.Drawing.Point(83, 97);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(121, 23);
            this.comboBoxMateria.TabIndex = 33;
            // 
            // labelMateria
            // 
            this.labelMateria.AutoSize = true;
            this.labelMateria.Location = new System.Drawing.Point(5, 97);
            this.labelMateria.Name = "labelMateria";
            this.labelMateria.Size = new System.Drawing.Size(50, 15);
            this.labelMateria.TabIndex = 32;
            this.labelMateria.Text = "Materia:";
            // 
            // labelQuantidadeQuestoes
            // 
            this.labelQuantidadeQuestoes.AutoSize = true;
            this.labelQuantidadeQuestoes.Location = new System.Drawing.Point(5, 128);
            this.labelQuantidadeQuestoes.Name = "labelQuantidadeQuestoes";
            this.labelQuantidadeQuestoes.Size = new System.Drawing.Size(73, 30);
            this.labelQuantidadeQuestoes.TabIndex = 35;
            this.labelQuantidadeQuestoes.Text = "Quantidade \r\nde questoes:";
            // 
            // numericUpDownQuantidade
            // 
            this.numericUpDownQuantidade.Location = new System.Drawing.Point(83, 128);
            this.numericUpDownQuantidade.Name = "numericUpDownQuantidade";
            this.numericUpDownQuantidade.Size = new System.Drawing.Size(120, 23);
            this.numericUpDownQuantidade.TabIndex = 36;
            // 
            // TelaCadastroTesteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 234);
            this.Controls.Add(this.numericUpDownQuantidade);
            this.Controls.Add(this.labelQuantidadeQuestoes);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.labelMateria);
            this.Controls.Add(this.comboBoxDisciplina);
            this.Controls.Add(this.labelDisciplina);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.txtTitulo);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.label1);
            this.Name = "TelaCadastroTesteForm";
            this.Text = "TelaCadastroTesteForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TelaCadastroTesteForm_FormClosing);
            this.Load += new System.EventHandler(this.TelaCadastroTesteForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownQuantidade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDisciplina;
        private System.Windows.Forms.Label labelDisciplina;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxMateria;
        private System.Windows.Forms.Label labelMateria;
        private System.Windows.Forms.Label labelQuantidadeQuestoes;
        public System.Windows.Forms.NumericUpDown numericUpDownQuantidade;
    }
}