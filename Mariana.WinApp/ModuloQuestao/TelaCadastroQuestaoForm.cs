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
using Mariana.WinApp.Compartilhado;

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
                txtQuestao.Text = Questao.Titulo;

                 if(Questao.bimestre == 2)
                    comboBoxBimestre.SelectedIndex = 1;
                else if (Questao.bimestre == 3)
                    comboBoxBimestre.SelectedIndex = 2;
                else if (Questao.bimestre == 4)
                    comboBoxBimestre.SelectedIndex = 3;
                else
                    comboBoxBimestre.SelectedIndex = 0;

                comboBoxMateria.SelectedItem = Questao.materia;

                if (Questao.materia == null)
                    comboBoxMateria.SelectedIndex = 0;

                int contador = 0;
                foreach (var item in Questao.opcoes)
                {
                    checkedListBoxAlternativas.Items.Add(item.Key);
                    if(item.Value == true)
                    {
                        checkedListBoxAlternativas.SetItemChecked(contador, true);
                        
                    }
                    contador++;

                }



            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {

    

            if (checkedListBoxAlternativas.CheckedItems.Count == 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Selecionar um item correto");
                DialogResult = DialogResult.None;
                return;
            }

            if (checkedListBoxAlternativas.Items.Count == 0)
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Adicione uma alternativa");
                DialogResult = DialogResult.None;
                return;
            }


            Questao.Titulo = txtQuestao.Text.TrimEnd().TrimStart();

            if (comboBoxBimestre.SelectedIndex == 0)
                Questao.bimestre = 1;
            else if (comboBoxBimestre.SelectedIndex == 1)
                Questao.bimestre = 2;
            else if (comboBoxBimestre.SelectedIndex == 2)
                Questao.bimestre = 3;
            else if (comboBoxBimestre.SelectedIndex == 3)
                Questao.bimestre = 4;

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

            questao.opcoes = dic.Shuffle();

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
            if (string.IsNullOrWhiteSpace(textBoxAlternativas.Text) || checkedListBoxAlternativas.Items.Contains(textBoxAlternativas.Text))
            {
                TelaPrincipalForm.Instancia.AtualizarRodape("Campo vazio ou ja existente");
                return;
            }
            checkedListBoxAlternativas.Items.Add(textBoxAlternativas.Text);
        }

        private void checkedListBoxAlternativas_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBoxAlternativas.Items.Count; ++ix)
                if (ix != e.Index) checkedListBoxAlternativas.SetItemChecked(ix, false);
        }
    }
}
