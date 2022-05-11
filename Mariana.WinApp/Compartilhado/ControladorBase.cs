using Mariana.Dominio.ModuloDisciplina;
using Mariana.WinApp.Compartilhado;
using Marina.Dominio.Compartilhado;
using System;
using System.Windows.Forms;

namespace Mariana.WinApp.Compartilhado
{
    public abstract class ControladorBase
    {
        public abstract void Inserir();
        public abstract void Editar();
        public abstract void Excluir();

        public virtual Disciplina ObtemSelecionada() { return new(); }

        public virtual void AtualizarQuestoes() { }

        public abstract UserControl ObtemListagem();

        public abstract ConfiguracaoToolboxBase ObtemConfiguracaoToolbox();

        public virtual void Gabarito() { }

        public virtual void PDF() { }
    }
}
