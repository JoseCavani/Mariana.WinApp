using Mariana.WinApp.Compartilhado;
using System.Windows.Forms;

namespace Mariana.WinApp.Compartilhado
{
    public abstract class ControladorBase
    {
        public abstract void Inserir();
        public abstract void Editar();
        public abstract void Excluir();

        public virtual void AdicionarQuestoes() { }

        public virtual void AtualizarQuestoes() { }

        public abstract UserControl ObtemListagem();

        public abstract ConfiguracaoToolboxBase ObtemConfiguracaoToolbox();
    }
}
