using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp.ModuloMateria
{
    public class ControladorMateria : ControladorBase
    {
        private IRepositorioMateria repositorioMateria;
        private TabelaMateriaControl tabelaMaterias;
        private List<Disciplina> disciplinas;

        public ControladorMateria(IRepositorioMateria repositorioMateria)
        {
            this.repositorioMateria = repositorioMateria;
            this.disciplinas = repositorioMateria.ObterDisciplinas();
        }

        private Materia ObtemMateriaSelecionada()
        {
            var numero = tabelaMaterias.ObtemNumeroMateriaSelecionado();

            return repositorioMateria.SelecionarPorNumero(numero);
        }

        public override void Editar()
        {
            Materia MateriaSelecionada = ObtemMateriaSelecionada();

            if (MateriaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Materia primeiro",
                "Edição de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroMateriaForm tela = new TelaCadastroMateriaForm(disciplinas);

            tela.Materia = (Materia)MateriaSelecionada.Clone();

            tela.GravarRegistro = repositorioMateria.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
        }

        public override void Excluir()
        {
            Materia MateriaSelecionada = ObtemMateriaSelecionada();

            if (MateriaSelecionada == null)
            {
                MessageBox.Show("Selecione uma Materia primeiro",
                "Exclusão de Materias", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

       


            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Materia?",
                "Exclusão de Materia", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);


            if (resultado == DialogResult.OK)
            {
                var validation = repositorioMateria.Excluir(MateriaSelecionada);

                if (validation.Errors.Count > 0)
                    MessageBox.Show(validation.Errors[0].ToString());
                CarregarMaterias();
            }
        }

        public override void Inserir()
        {
            TelaCadastroMateriaForm tela = new TelaCadastroMateriaForm(disciplinas);
            tela.Materia = new Materia();

            tela.GravarRegistro = repositorioMateria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarMaterias();
            }
        }

        private void CarregarMaterias()
        {
            List<Materia> Materias = repositorioMateria.SelecionarTodos();



            tabelaMaterias.AtualizarRegistros(Materias);
            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {Materias.Count} Materias(s)");
        }


        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxMateria();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaMaterias == null)
                tabelaMaterias = new TabelaMateriaControl();

            CarregarMaterias();

            return tabelaMaterias;
        }
    }
}
