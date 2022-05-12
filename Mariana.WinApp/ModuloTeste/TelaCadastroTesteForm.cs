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
        public TelaCadastroTesteForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            comboBoxDisciplina.Items.AddRange(disciplinas.ToArray());
          
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

                comboBoxMateria.SelectedItem = teste.Materia;

                comboBoxDisciplina.SelectedItem = teste.Disciplina;
                

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

            teste.NumeroQuestoes = (int)numericUpDownQuantidade.Value;


            if (comboBoxMateria.SelectedItem.ToString() == "Todos")
            {
                teste.Questoes = teste.Disciplina.questoes
                 .OrderBy(x => rnd.Next())
                .Take(teste.NumeroQuestoes)
                .ToList();
            }

            else
            {
                teste.Questoes = teste.Disciplina.questoes.Where(x => x.materia == teste.Materia)
                    .ToList()
                    .OrderBy(x => rnd.Next())
                    .Take(teste.NumeroQuestoes)
                    .ToList();
            }

          
            teste.Titulo = txtTitulo.Text;


          
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
            Disciplina disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;
          
                foreach (var questao in disciplina.questoes)
                {
                if (comboBoxMateria.Items.Contains(questao.materia))
                    continue;
                    comboBoxMateria.Items.Add(questao.materia);
                }
            
        }
    }
}
