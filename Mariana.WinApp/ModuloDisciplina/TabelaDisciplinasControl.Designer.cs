namespace Mariana.WinApp.ModuloDisciplina
{
    partial class TabelaDisciplinasControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridDisciplinas = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridDisciplinas)).BeginInit();
            this.SuspendLayout();
            // 
            // gridDisciplinas
            // 
            this.gridDisciplinas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDisciplinas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDisciplinas.Location = new System.Drawing.Point(0, 0);
            this.gridDisciplinas.Name = "gridDisciplinas";
            this.gridDisciplinas.RowTemplate.Height = 25;
            this.gridDisciplinas.Size = new System.Drawing.Size(311, 307);
            this.gridDisciplinas.TabIndex = 1;
            // 
            // TabelaDisciplinasControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridDisciplinas);
            this.Name = "TabelaDisciplinasControl";
            this.Size = new System.Drawing.Size(311, 307);
            ((System.ComponentModel.ISupportInitialize)(this.gridDisciplinas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridDisciplinas;
    }
}
