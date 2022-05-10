namespace Mariana.WinApp.Compartilhado
{
    public abstract class ConfiguracaoToolboxBase
    {
        public abstract string TipoCadastro { get; }

        public abstract string TooltipInserir { get; }

        public abstract string TooltipEditar { get; }

        public abstract string TooltipExcluir { get; }

        public virtual string TooltipAtualizarQuestoes { get; }


        public virtual bool InserirHabilitado { get { return true; } }

        public virtual bool AtualizarQuestoesHabilitado { get { return false; } }

        public virtual bool EditarHabilitado { get { return true; } }

        public virtual bool ExcluirHabilitado { get { return true; } }
    }
}