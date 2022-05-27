using Mariana.Dominio.ModuloQuestao;
using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp.ModuloQuestao
{
    public partial class TabelaQuestaoControl : UserControl
    {
        public TabelaQuestaoControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Questão", HeaderText = "Questão"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Bimestre", HeaderText = "Bimestre"},
            };

            return colunas;
        }

        public int ObtemNumeroQuestaoSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Questao> questeos)
        {
            grid.Rows.Clear();

            foreach (Questao questao in questeos)
            {
                grid.Rows.Add(questao.Numero, questao.Titulo, questao.bimestre+"ª");
            }
        }

    }
}

