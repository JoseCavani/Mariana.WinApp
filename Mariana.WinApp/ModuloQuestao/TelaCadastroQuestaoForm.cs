using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
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

namespace Mariana.WinApp.ModuloQuestao
{
    public partial class TelaCadastroQuestaoForm : Form
    {
        private Questao questao;
        public TelaCadastroQuestaoForm(List<Materia> materias)
        {
            InitializeComponent();
            comboBoxMateria.Items.AddRange(materias.ToArray());
            comboBoxMateria.SelectedIndex = 0;
        }

        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        public Questao Questao
        {
            get
            {
                return questao;
            }
            set
            {
                questao = value;
                txtNumero.Text = Questao.Numero.ToString();
                txtQuestao.Text = Questao.questao;
                if (Questao.Bimestre == 1)
                    comboBoxBimestre.SelectedIndex = 0;
                else if(Questao.Bimestre == 2)
                    comboBoxBimestre.SelectedIndex = 1;
                else if (Questao.Bimestre == 3)
                    comboBoxBimestre.SelectedIndex = 2;
                else if (Questao.Bimestre == 4)
                    comboBoxBimestre.SelectedIndex = 3;

                comboBoxMateria.SelectedItem = Questao.materia;

                int contador = 0;
                foreach (var item in Questao.opcoes)
                {
                    checkedListBoxAlternativas.Items.Add(item.Key);
                    if(item.Value == true)
                    {
                        checkedListBoxAlternativas.SetItemChecked(contador, true);
                    }

                }



            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            Questao.questao = txtQuestao.Text;

            if (comboBoxBimestre.SelectedIndex == 0)
                Questao.Bimestre = 1;
            else if (comboBoxBimestre.SelectedIndex == 1)
                Questao.Bimestre = 2;
            else if (comboBoxBimestre.SelectedIndex == 2)
                Questao.Bimestre = 3;
            else if (comboBoxBimestre.SelectedIndex == 3)
                Questao.Bimestre = 4;

            Questao.materia = (Materia)comboBoxMateria.SelectedItem;

               Dictionary<string, bool> dic = new();


            foreach (var item in checkedListBoxAlternativas.CheckedItems)
            {
                dic.Add(item.ToString(),true);
            }

            foreach (object item in checkedListBoxAlternativas.Items)
            {
                if (!checkedListBoxAlternativas.CheckedItems.Contains(item))
                {
                    dic.Add(item.ToString(), false);
                }
            }

            questao.opcoes = dic;



            var resultadoValidacao = GravarRegistro(Questao);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void TelaCadastroQuestaoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void TelaCadastroQuestaoForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void buttonGravarAlternativas_Click(object sender, EventArgs e)
        {
            checkedListBoxAlternativas.Items.Add(textBoxAlternativas.Text);
        }
    }
}
