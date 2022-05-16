using Mariana.Dominio.ModuloTeste;
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

namespace Mariana.WinApp.ModuloTeste
{
    public partial class TabelaTesteControl : UserControl
    {
        public TabelaTesteControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Teste", HeaderText = "Teste"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Quantidade de questoes", HeaderText = "Quantidade de questoes"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina ", HeaderText = "Disciplina "},

                new DataGridViewTextBoxColumn { DataPropertyName = "Data ", HeaderText = "Data "},
            };

            return colunas;
        }

        public int ObtemNumeroTesteSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Teste> Testes)
        {
            grid.Rows.Clear();

            foreach (Teste Teste in Testes)
            {
                grid.Rows.Add(Teste.Numero, Teste.Titulo,Teste.NumeroQuestoes,Teste.Disciplina.Titulo,Teste.Data);
            }
            if (grid.RowCount > 0)
            {
                grid.Rows[0].Selected = true;
            }
        }
    }
}
