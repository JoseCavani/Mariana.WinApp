using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloTeste;
using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp.ModuloTeste
{
    class ControladorTeste : ControladorBase
    {
        private IRepositorioTeste repositorioTeste;
        private TabelaTesteControl tabelaTestes;
        private List<Disciplina> disciplinas;

        public ControladorTeste(IRepositorioTeste repositorioTeste, List<Disciplina> disciplinas)
        {
            this.repositorioTeste = repositorioTeste;
            this.disciplinas = disciplinas;
        }

        private Teste ObtemTesteSelecionada()
        {
            var numero = tabelaTestes.ObtemNumeroTesteSelecionado();

            return repositorioTeste.SelecionarPorNumero(numero);
        }


        public override void AtualizarQuestoes()
        {

            foreach (var item in repositorioTeste.SelecionarQuestoes())
            {
                klasjhdjkashdskaj
            }
            TelaPrincipalForm.Instancia.disciplinaSelecionada.questoes = repositorioTeste.SelecionarQuestoes();
            TelaPrincipalForm.Instancia.ConfigurarTelaPrincipal();
        }

        public override void Editar()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Edição de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(disciplinas);

            tela.Teste = TesteSelecionada;

            tela.GravarRegistro = repositorioTeste.Editar;

            tela.numericUpDownQuantidade.Enabled = false;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Excluir()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Teste?",
                "Exclusão de Teste", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTeste.Excluir(TesteSelecionada);
                CarregarTestes();
            }
        }

        public override void Inserir()
        {
            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(disciplinas);
            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            tela.numericUpDownQuantidade.Enabled = true;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        private void CarregarTestes()
        {
            List<Teste> Testes = repositorioTeste.SelecionarTodos();



            tabelaTestes.AtualizarRegistros(Testes);
            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {Testes.Count} Testes(s)");
        }


        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxTeste();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaTestes == null)
                tabelaTestes = new TabelaTesteControl();

            CarregarTestes();

            return tabelaTestes;
        }
    }
}
