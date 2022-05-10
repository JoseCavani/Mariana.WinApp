using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Infra.Arquivos.ModuloDisciplina;
using Mariana.Infra.Arquivos.ModuloMateria;
using Mariana.Infra.Arquivos.ModuloQuestao;
using Mariana.WinApp.Compartilhado;
using Mariana.WinApp.ModuloDisciplina;
using Mariana.WinApp.ModuloMateria;
using Mariana.WinApp.ModuloQuestao;
using Marina.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mariana.WinApp
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;
        private Disciplina disciplinaSelecionada;
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
      
        private void materiaMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }
        private void disciplinaMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
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

       

        private void ConfigurarBotoes(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnAtualizarQuestoes.Enabled = configuracao.AtualizarQuestoesHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolboxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnAtualizarQuestoes.ToolTipText = configuracao.TooltipAtualizarQuestoes;
        }

        /// <summary>
        /// para questoes
        /// </summary>
        /// <param name="v"></param>
        public void ConfigurarTelaPrincipal(string questao)
        {
            controlador = controladores["Disciplina"];

           disciplinaSelecionada =  controlador.ObtemDisciplinaSelecionada();

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

            controlador = controladores[questao];



            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            if (tipo == "Materia" && contextoDados.Disciplinas.Count == 0)
            {
                AtualizarRodape("cadastre uma disciplina primeiro");
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

            panelRegistros.Controls.Add(listagemControl);
        }


        private void IniciaControladorQuestao(List<Materia> materias)
        {
            var repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);

            controladores.Add("Questao", new ControladorQuestao(repositorioQuestao, materias));
        }

        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contextoDados);

            var repositorioMateria = new RepositorioMaterEmArquivo(contextoDados);



            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplina", new ControladorDisciplina(repositorioDisciplina));

            controladores.Add("Materia", new ControladorMateria(repositorioMateria, contextoDados.Disciplinas));
        }
    }
}

