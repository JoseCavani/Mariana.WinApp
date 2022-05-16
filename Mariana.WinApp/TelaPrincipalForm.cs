using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloTeste;
using Mariana.Infra.Arquivos.ModuloDisciplina;
using Mariana.Infra.Arquivos.ModuloMateria;
using Mariana.Infra.Arquivos.ModuloQuestao;
using Mariana.Infra.Arquivos.ModuloTeste;
using Mariana.WinApp.Compartilhado;
using Mariana.WinApp.ModuloDisciplina;
using Mariana.WinApp.ModuloMateria;
using Mariana.WinApp.ModuloQuestao;
using Mariana.WinApp.ModuloTeste;
using Marina.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;
        public  Disciplina disciplinaSelecionada = new();

        public Teste testeAtual = new();

        public bool testeAtivo = false;

        public TelaPrincipalForm(DataContext contextoDados)
        {
            InitializeComponent();
            Instancia = this;

            labelRodape.Text = string.Empty;
            labelTipoCadastro.Text = string.Empty;

            this.contextoDados = contextoDados;

            InicializarControladores();
        }

        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }

        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }

        private void TesteMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void materiaMenuItem_Click(object sender, EventArgs e)
        {
            testeAtivo = false;
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }
        private void disciplinaMenuItem_Click(object sender, EventArgs e)
        {
            testeAtivo = false;
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }



        private void toolStripButtonDuplicar_Click(object sender, EventArgs e)
        {
            controlador.Duplicar();
        }


        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }

        private void btnAtualizarQuestoes_Click(object sender, EventArgs e)
        {
            controlador.AtualizarQuestoes();
        }

        private void toolStripButtonGabarito_Click(object sender, EventArgs e)
        {
            controlador.Gabarito();
            AtualizarRodape("PDF gerado nos documentos");
        }
        private void toolStripButtonPDF_Click(object sender, EventArgs e)
        {
            controlador.PDF();
            AtualizarRodape("PDF gerado nos documentos");
        }


        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnAtualizarQuestoes.Enabled = configuracao.AtualizarQuestoesHabilitado;
            toolStripButtonGabarito.Enabled = configuracao.GabaritoHabilitado;
            toolStripButtonPDF.Enabled = configuracao.PDFHabilitado;
            toolStripButtonDuplicar.Enabled = configuracao.DuplicarHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnAtualizarQuestoes.ToolTipText = configuracao.TooltipAtualizarQuestoes;
            toolStripButtonGabarito.ToolTipText = configuracao.TooltipGabarito;
            toolStripButtonPDF.ToolTipText = configuracao.TooltipPDF;
            toolStripButtonDuplicar.ToolTipText = configuracao.TooltipDuplicar;
        }

        /// <summary>
        /// para questoes
        /// </summary>
        /// <param></param>
        public void ConfigurarTelaPrincipal()
        {
            controlador = controladores["Disciplina"];



            if (disciplinaSelecionada == default)
            {
                AtualizarRodape("Selecione uma disciplina");
                return;
            }

            List<Materia> materias = new List<Materia>();
            foreach (var item in contextoDados.Materias)
            {
                if (item.Disciplina == disciplinaSelecionada)
                {
                    materias.Add(item);
                }
            }
            if (materias.Count == 0)
            {
                AtualizarRodape("registre uma materia com essa disciplina primeiro");
                return;
            }


            IniciaControladorQuestao(materias);

            controlador = controladores["Questao"];



            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            if (tipo != "Disciplina" && contextoDados.Disciplinas.Count == 0)
            {
                AtualizarRodape("cadastre uma disciplina primeiro");
                return;
            }

            if (tipo == "Teste" && contextoDados.Materias.Count == 0 && contextoDados.Teste.Count == 0)
            {
                AtualizarRodape("cadastre uma Materia primeiro");
                return;
            }



            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolboxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolbox.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            Thread.Sleep(100);
            panelRegistros.Controls.Add(listagemControl);
        }


        private void IniciaControladorQuestao(List<Materia> materias)
        {

            var repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);
            if (!controladores.ContainsKey("Questao"))
            {

                controladores.Add("Questao", new ControladorQuestao(repositorioQuestao, materias));
            }
            controladores["Questao"] = new ControladorQuestao(repositorioQuestao, materias);
        }

        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contextoDados);

            var repositorioMateria = new RepositorioMaterEmArquivo(contextoDados);

            var repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);



            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplina", new ControladorDisciplina(repositorioDisciplina));

            controladores.Add("Materia", new ControladorMateria(repositorioMateria));

            controladores.Add("Teste", new ControladorTeste(repositorioTeste));
        }

      
    }
}

