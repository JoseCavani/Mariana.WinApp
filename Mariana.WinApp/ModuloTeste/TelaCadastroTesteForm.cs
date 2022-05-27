using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
using Mariana.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp.ModuloTeste
{
    public partial class TelaCadastroTesteForm : Form
    {
        private Teste teste;
        List<Disciplina> disciplinas;
        public TelaCadastroTesteForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            comboBoxDisciplina.Items.AddRange(disciplinas.ToArray());
            this.disciplinas = disciplinas;
          
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        public Teste Teste
        {
            get
            {
                return teste;
            }
            set
            {
                teste = value;
                txtNumero.Text = teste.Numero.ToString();

                txtTitulo.Text = teste.Titulo;

                if (teste.Disciplina != null)
                comboBoxDisciplina.SelectedItem = disciplinas.Where(x => x.Numero == teste.Disciplina.Numero).Single();


                if(teste.Materia != null)
                comboBoxMateria.SelectedItem = teste.Materia;


                if (teste.Materia == null)
                    comboBoxMateria.SelectedIndex = 0;

                if (teste.Disciplina == null)
                    comboBoxDisciplina.SelectedIndex = 0;


                numericUpDownQuantidade.Value = teste.NumeroQuestoes;


            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                teste.Disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;
            }
            catch (Exception)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("selecionar uma disciplina");
                return;
            }


            try
            {
                if (comboBoxMateria.SelectedIndex == 0)
                    teste.Materia = default;
                else
                    teste.Materia = (Materia)comboBoxMateria.SelectedItem;
            }
            catch (Exception)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("selecionar uma materia");
                return;
            }

            Random rnd = new Random();

            List<Questao> q = new List<Questao>();

            foreach (var item in teste.Disciplina.questoes)
            {
                q.Add(item);
            }

            if (comboBoxMateria.SelectedItem.ToString() == "Todos")
            {
                teste.Questoes = q
                 .OrderBy(x => rnd.Next())
                .Take((int)numericUpDownQuantidade.Value)
                .ToList();
            }

            else
            {
                teste.Questoes = q.Where(x => x.materia == teste.Materia)
                    .ToList()
                    .OrderBy(x => rnd.Next())
                    .Take((int)numericUpDownQuantidade.Value)
                    .ToList();


            }

            teste.Titulo = txtTitulo.Text.TrimEnd().TrimStart();




            if (teste.Questoes.Count != (int)numericUpDownQuantidade.Value)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("não ha questoes suficientes com esses criterios");
                DialogResult = DialogResult.None;
                return;
            }
          
            var resultadoValidacao = GravarRegistro(Teste);


          


            if (resultadoValidacao.IsValid == false)
            {


                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void TelaCadastroTesteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void TelaCadastroTesteForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void comboBoxDisciplina_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMateria.Items.Clear();
            comboBoxMateria.Items.Add("Todos");
            comboBoxMateria.SelectedIndex = 0;
            Disciplina disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;
          
                foreach (var questao in disciplina.questoes)
                {
                if (comboBoxMateria.Items.Contains(questao.materia))
                    continue;
                    comboBoxMateria.Items.Add(questao.materia);
                }
            
        }


        ToolTip buttonToolTip = new ToolTip();
        private void comboBoxMateria_MouseHover(object sender, EventArgs e)
        {
            buttonToolTip.UseFading = true;
            buttonToolTip.IsBalloon = true;
            buttonToolTip.ShowAlways = true;
            buttonToolTip.AutoPopDelay = 3000;
            buttonToolTip.InitialDelay = 0;
            buttonToolTip.ReshowDelay = 0;

            buttonToolTip.SetToolTip(comboBoxMateria, "So aparece as materias que contem questoes");
        }
    }
}
