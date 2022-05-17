namespace Mariana.WinApp.ModuloQuestao
{
    partial class TelaCadastroQuestaoForm
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
            this.comboBoxMateria = new System.Windows.Forms.ComboBox();
            this.labelMateria = new System.Windows.Forms.Label();
            this.comboBoxBimestre = new System.Windows.Forms.ComboBox();
            this.labelBimestre = new System.Windows.Forms.Label();
            this.txtQuestao = new System.Windows.Forms.TextBox();
            this.txtNumero = new System.Windows.Forms.TextBox();
            this.labelQuestao = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravar = new System.Windows.Forms.Button();
            this.checkedListBoxAlternativas = new System.Windows.Forms.CheckedListBox();
            this.labelAlternativas = new System.Windows.Forms.Label();
            this.textBoxAlternativas = new System.Windows.Forms.TextBox();
            this.buttonGravarAlternativas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxMateria
            // 
            this.comboBoxMateria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMateria.FormattingEnabled = true;
            this.comboBoxMateria.Location = new System.Drawing.Point(83, 96);
            this.comboBoxMateria.Name = "comboBoxMateria";
            this.comboBoxMateria.Size = new System.Drawing.Size(121, 23);
            this.comboBoxMateria.TabIndex = 29;
            // 
            // labelMateria
            // 
            this.labelMateria.AutoSize = true;
            this.labelMateria.Location = new System.Drawing.Point(27, 100);
            this.labelMateria.Name = "labelMateria";
            this.labelMateria.Size = new System.Drawing.Size(50, 15);
            this.labelMateria.TabIndex = 28;
            this.labelMateria.Text = "Materia:";
            // 
            // comboBoxBimestre
            // 
            this.comboBoxBimestre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBimestre.FormattingEnabled = true;
            this.comboBoxBimestre.Items.AddRange(new object[] {
            "1ª",
            "2ª",
            "3ª",
            "4ª"});
            this.comboBoxBimestre.Location = new System.Drawing.Point(83, 67);
            this.comboBoxBimestre.Name = "comboBoxBimestre";
            this.comboBoxBimestre.Size = new System.Drawing.Size(121, 23);
            this.comboBoxBimestre.TabIndex = 27;
            // 
            // labelBimestre
            // 
            this.labelBimestre.AutoSize = true;
            this.labelBimestre.Location = new System.Drawing.Point(21, 71);
            this.labelBimestre.Name = "labelBimestre";
            this.labelBimestre.Size = new System.Drawing.Size(56, 15);
            this.labelBimestre.TabIndex = 26;
            this.labelBimestre.Text = "Bimestre:";
            // 
            // txtQuestao
            // 
            this.txtQuestao.Location = new System.Drawing.Point(83, 39);
            this.txtQuestao.Name = "txtQuestao";
            this.txtQuestao.Size = new System.Drawing.Size(333, 23);
            this.txtQuestao.TabIndex = 25;
            // 
            // txtNumero
            // 
            this.txtNumero.Enabled = false;
            this.txtNumero.Location = new System.Drawing.Point(83, 13);
            this.txtNumero.Name = "txtNumero";
            this.txtNumero.Size = new System.Drawing.Size(64, 23);
            this.txtNumero.TabIndex = 24;
            // 
            // labelQuestao
            // 
            this.labelQuestao.AutoSize = true;
            this.labelQuestao.Location = new System.Drawing.Point(23, 42);
            this.labelQuestao.Name = "labelQuestao";
            this.labelQuestao.Size = new System.Drawing.Size(54, 15);
            this.labelQuestao.TabIndex = 23;
            this.labelQuestao.Text = "Questao:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Número:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(344, 241);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 39);
            this.btnCancelar.TabIndex = 31;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnGravar
            // 
            this.btnGravar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnGravar.Location = new System.Drawing.Point(266, 241);
            this.btnGravar.Name = "btnGravar";
            this.btnGravar.Size = new System.Drawing.Size(72, 39);
            this.btnGravar.TabIndex = 30;
            this.btnGravar.Text = "Gravar";
            this.btnGravar.UseVisualStyleBackColor = true;
            this.btnGravar.Click += new System.EventHandler(this.btnGravar_Click);
            // 
            // checkedListBoxAlternativas
            // 
            this.checkedListBoxAlternativas.CheckOnClick = true;
            this.checkedListBoxAlternativas.FormattingEnabled = true;
            this.checkedListBoxAlternativas.Location = new System.Drawing.Point(83, 156);
            this.checkedListBoxAlternativas.Name = "checkedListBoxAlternativas";
            this.checkedListBoxAlternativas.Size = new System.Drawing.Size(144, 94);
            this.checkedListBoxAlternativas.TabIndex = 32;
            this.checkedListBoxAlternativas.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxAlternativas_ItemCheck);
            // 
            // labelAlternativas
            // 
            this.labelAlternativas.AutoSize = true;
            this.labelAlternativas.Location = new System.Drawing.Point(5, 125);
            this.labelAlternativas.Name = "labelAlternativas";
            this.labelAlternativas.Size = new System.Drawing.Size(72, 15);
            this.labelAlternativas.TabIndex = 33;
            this.labelAlternativas.Text = "Alternativas:";
            // 
            // textBoxAlternativas
            // 
            this.textBoxAlternativas.Location = new System.Drawing.Point(83, 125);
            this.textBoxAlternativas.Name = "textBoxAlternativas";
            this.textBoxAlternativas.Size = new System.Drawing.Size(144, 23);
            this.textBoxAlternativas.TabIndex = 34;
            // 
            // buttonGravarAlternativas
            // 
            this.buttonGravarAlternativas.Location = new System.Drawing.Point(233, 125);
            this.buttonGravarAlternativas.Name = "buttonGravarAlternativas";
            this.buttonGravarAlternativas.Size = new System.Drawing.Size(54, 23);
            this.buttonGravarAlternativas.TabIndex = 35;
            this.buttonGravarAlternativas.Text = "Gravar";
            this.buttonGravarAlternativas.UseVisualStyleBackColor = true;
            this.buttonGravarAlternativas.Click += new System.EventHandler(this.buttonGravarAlternativas_Click);
            // 
            // TelaCadastroQuestaoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 292);
            this.Controls.Add(this.buttonGravarAlternativas);
            this.Controls.Add(this.textBoxAlternativas);
            this.Controls.Add(this.labelAlternativas);
            this.Controls.Add(this.checkedListBoxAlternativas);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGravar);
            this.Controls.Add(this.comboBoxMateria);
            this.Controls.Add(this.labelMateria);
            this.Controls.Add(this.comboBoxBimestre);
            this.Controls.Add(this.labelBimestre);
            this.Controls.Add(this.txtQuestao);
            this.Controls.Add(this.txtNumero);
            this.Controls.Add(this.labelQuestao);
            this.Controls.Add(this.label1);
            this.Name = "TelaCadastroQuestaoForm";
            this.Text = "TelaCadastroQuestaoForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TelaCadastroQuestaoForm_FormClosing);
            this.Load += new System.EventHandler(this.TelaCadastroQuestaoForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxMateria;
        private System.Windows.Forms.Label labelMateria;
        private System.Windows.Forms.ComboBox comboBoxBimestre;
        private System.Windows.Forms.Label labelBimestre;
        private System.Windows.Forms.TextBox txtQuestao;
        private System.Windows.Forms.TextBox txtNumero;
        private System.Windows.Forms.Label labelQuestao;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravar;
        private System.Windows.Forms.CheckedListBox checkedListBoxAlternativas;
        private System.Windows.Forms.Label labelAlternativas;
        private System.Windows.Forms.TextBox textBoxAlternativas;
        private System.Windows.Forms.Button buttonGravarAlternativas;
    }
}