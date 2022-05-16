using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp.ModuloQuestao
{

    public class ControladorQuestao : ControladorBase
    {
        private IRepositorioQuestao repositorioQuestao;
        private TabelaQuestaoControl tabelaQuestaos;
        private List<Materia> materias;
        private List<Questao> questaos = new();

        public ControladorQuestao(IRepositorioQuestao repositorioQuestao, List<Materia> materias)
        {
            this.repositorioQuestao = repositorioQuestao;
            this.materias = materias;
        }

        private Questao ObtemQuestaoSelecionada()
        {
            var numero = tabelaQuestaos.ObtemNumeroQuestaoSelecionado();

            return repositorioQuestao.SelecionarPorNumero(numero);
        }

        public override void Editar()
        {
            Questao QuestaoSelecionada = ObtemQuestaoSelecionada();

            if (QuestaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma Questao primeiro",
                "Edição de Questaos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm(materias);

            tela.Questao = (Questao)QuestaoSelecionada.Clone();

            tela.GravarRegistro = repositorioQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                questaos.Remove(QuestaoSelecionada);
                questaos.Add(tela.Questao);

                if (TelaPrincipalForm.Instancia.testeAtivo)
                {
                    TelaPrincipalForm.Instancia.testeAtual.Questoes.Remove(QuestaoSelecionada);
                    TelaPrincipalForm.Instancia.testeAtual.Questoes.Add(tela.Questao);
                    TelaPrincipalForm.Instancia.testeAtivo = false;
                }

                CarregarQuestaos();
            }
        }

        public override void Excluir()
        {
            Questao QuestaoSelecionada = ObtemQuestaoSelecionada();

            if (QuestaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma Questao primeiro",
                "Exclusão de Questaos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Questao?",
                "Exclusão de Questao", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                questaos.Remove(QuestaoSelecionada);

                if (TelaPrincipalForm.Instancia.testeAtivo)
                {
                    TelaPrincipalForm.Instancia.testeAtual.Questoes.Remove(QuestaoSelecionada);
                    TelaPrincipalForm.Instancia.testeAtual.NumeroQuestoes -= 1;
                }
                else
                    repositorioQuestao.Excluir(QuestaoSelecionada);
                CarregarQuestaos();
            }
        }

        public override void Inserir()
        {
            TelaCadastroQuestaoForm tela = new TelaCadastroQuestaoForm(materias);
            tela.Questao = new Questao();

            tela.GravarRegistro = repositorioQuestao.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                questaos.Add(tela.Questao);
                if (TelaPrincipalForm.Instancia.testeAtivo) 
                {
                    TelaPrincipalForm.Instancia.testeAtual.Questoes.Add(tela.Questao);
                    TelaPrincipalForm.Instancia.testeAtual.NumeroQuestoes += 1;
                }
                 CarregarQuestaos();
            }
        }

        private void CarregarQuestaos()
        {
         

            tabelaQuestaos.AtualizarRegistros(questaos);
            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {questaos.Count} Questaos(s)");
        }


        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxQuestao();
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaQuestaos == null)
                tabelaQuestaos = new TabelaQuestaoControl();


            questaos = TelaPrincipalForm.Instancia.disciplinaSelecionada.questoes;

            TelaPrincipalForm.Instancia.disciplinaSelecionada.questoes = questaos;

            CarregarQuestaos();

            return tabelaQuestaos;
        }
      
    }
}


