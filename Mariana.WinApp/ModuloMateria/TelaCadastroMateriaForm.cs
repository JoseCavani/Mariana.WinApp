using Mariana.Dominio.ModuloMateria;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationResult = FluentValidation.Results.ValidationResult;
using Mariana.Dominio.ModuloDisciplina;

namespace Mariana.WinApp.ModuloMateria
{
    public partial class TelaCadastroMateriaForm : Form
    {
        private Materia materia;
        public TelaCadastroMateriaForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            comboBoxDisciplina.Items.AddRange(disciplinas.ToArray());
            //comboBoxDisciplina.SelectedIndex = 0;
        }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        public Materia Materia
        {
            get
            {
                return materia;
            }
            set
            {
                materia = value;
                txtNumero.Text = materia.Numero.ToString();
                txtTitulo.Text = materia.Titulo;
                if (materia.Serie == 1)
                    comboBoxSeries.SelectedIndex = 0;
                else
                    comboBoxSeries.SelectedIndex = 1;

                comboBoxDisciplina.SelectedItem = materia.Disciplina;

                if (materia.Disciplina == null)
                    comboBoxDisciplina.SelectedIndex = 0;
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            materia.Titulo = txtTitulo.Text.TrimEnd().TrimStart();

            if (comboBoxSeries.SelectedIndex == 0)
                materia.Serie = 1;
            else
                materia.Serie = 2;

            materia.Disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;

          var resultadoValidacao = GravarRegistro(materia);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void TelaCadastroMateriaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void TelaCadastroMateriaForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }
    }
}
