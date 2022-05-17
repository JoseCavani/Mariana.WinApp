using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloQuestao;
using Mariana.Dominio.ModuloTeste;
using Mariana.WinApp.Compartilhado;
using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.IO;
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

        public ControladorTeste(IRepositorioTeste repositorioTeste)
        {
            this.repositorioTeste = repositorioTeste;
        }

        private Teste ObtemTesteSelecionada()
        {
            var numero = tabelaTestes.ObtemNumeroTesteSelecionado();

            return repositorioTeste.SelecionarPorNumero(numero);
        }



        public override void PDF()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DocumentCore dc = new DocumentCore();

            dc.Content.End.Insert(TesteSelecionada.Titulo + "\n");
            dc.Content.End.Insert("-------------------------------------------------\n");
            foreach (var questao in TesteSelecionada.Questoes)
            {
                dc.Content.End.Insert($"{questao.Titulo}\n");
                foreach (var opcao in questao.opcoes)
                {
                  dc.Content.End.Insert($"{opcao.Key}\n");
                }
                dc.Content.End.Insert("-------------------------------------------------\n");
            }

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Teste"
                + TesteSelecionada.Numero.ToString() + ".pdf";

            dc.Save(path, new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A1a,
                PreserveFormFields = true
            });


            MessageBox.Show("salvo nos documentos");

        }


        public override void AtualizarQuestoes()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Questoes dos Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaPrincipalForm.Instancia.testeAtual = TesteSelecionada;
            TelaPrincipalForm.Instancia.testeAtivo = true;
            TelaPrincipalForm.Instancia.ConfigurarTelaPrincipal("Teste");
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

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(repositorioTeste.ObterDiscplinas());

            tela.Teste = (Teste)TesteSelecionada.Clone();

            tela.GravarRegistro = repositorioTeste.Editar;

            tela.numericUpDownQuantidade.Enabled = false;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Gabarito()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Exclusão de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DocumentCore dc = new DocumentCore();

            dc.Content.End.Insert(TesteSelecionada.Titulo + "\n");
            dc.Content.End.Insert("-------------------------------------------------\n");
            foreach (var questao in TesteSelecionada.Questoes)
            {
                dc.Content.End.Insert($"{questao.Titulo}\n");
                foreach (var opcao in questao.opcoes)
                {
                    if (opcao.Value == true)
                        dc.Content.End.Insert($"{opcao.Key} (CORRETA)\n");
                    else
                    dc.Content.End.Insert($"{opcao.Key}\n");
                }
                dc.Content.End.Insert("-------------------------------------------------\n");
            }



            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Gabarito do Teste"
              + TesteSelecionada.Numero.ToString() + ".pdf";

            dc.Save(path, new PdfSaveOptions()
            {
                Compliance = PdfCompliance.PDF_A1a,
                PreserveFormFields = true
            });


            MessageBox.Show("salvo nos documentos");
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

            Stream str = Properties.Resources.silvio_santos_esta_certo_disso1;

            System.Media.SoundPlayer snd = new System.Media.SoundPlayer(str);
            snd.Play();

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a Teste?",
                "Exclusão de Teste", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTeste.Excluir(TesteSelecionada);
                CarregarTestes();
            }
        }

        public override void Duplicar()
        {
            Teste TesteSelecionada = ObtemTesteSelecionada();

            if (TesteSelecionada == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Edição de Testes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(repositorioTeste.ObterDiscplinas());

            tela.Teste = (Teste)TesteSelecionada.Clone();

            tela.txtTitulo.Enabled = false;

            tela.comboBoxDisciplina.Enabled = false;

            tela.comboBoxMateria.Enabled = false;

            tela.GravarRegistro = ColocaNumeroNoTituloEInserir(tela);



            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        private Func<Teste,ValidationResult> ColocaNumeroNoTituloEInserir(TelaCadastroTesteForm tela)
        {

            var result = string.Concat(tela.Teste.Titulo.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse());
            bool nomeEncontrado;
            string tituloTemporary;


            if (int.TryParse(result.ToString(), out int num))
            {
                do
                {
                    num++;
                    tituloTemporary = tela.Teste.Titulo.Replace(result, num.ToString());


                    nomeEncontrado = repositorioTeste.ObterRegistros()
         .Select(x => x.Titulo)
         .Contains(tituloTemporary);
                } while (nomeEncontrado);

                tela.Teste.Titulo = tituloTemporary;
            }
            else
            {
              


                nomeEncontrado = false;

                int contador = 2;

                do
                {
                     tituloTemporary = tela.Teste.Titulo;

                    tituloTemporary += $" {contador.ToString()}";


                    nomeEncontrado = repositorioTeste.ObterRegistros()
             .Select(x => x.Titulo)
             .Contains(tituloTemporary);

                    contador++;

                } while (nomeEncontrado);

                tela.Teste.Titulo = tituloTemporary;

            }

            tela.txtTitulo.Text = tela.Teste.Titulo;

           return repositorioTeste.Inserir;
        }

        public override void Inserir()
        {

            TelaCadastroTesteForm tela = new TelaCadastroTesteForm(repositorioTeste.ObterDiscplinas());
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
