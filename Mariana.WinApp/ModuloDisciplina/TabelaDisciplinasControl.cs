using Mariana.Dominio.ModuloDisciplina;
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

namespace Mariana.WinApp.ModuloDisciplina
{
    public partial class TabelaDisciplinasControl : UserControl
    {
        public TabelaDisciplinasControl()
        {
            InitializeComponent(); 
            gridDisciplinas.ConfigurarGridZebrado();
            gridDisciplinas.ConfigurarGridSomenteLeitura();
            gridDisciplinas.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Número"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina", HeaderText = "Disciplina"},
            };

            return colunas;
        }

        public int ObtemNumeroDisciplinaSelecionado()
        {
            return gridDisciplinas.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Disciplina> disciplinas)
        {
            gridDisciplinas.Rows.Clear();

            foreach (Disciplina disciplina in disciplinas)
            {
                gridDisciplinas.Rows.Add(disciplina.Numero, disciplina.Titulo);
            }
        }


    }
}
    