using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.WinApp.ModuloTeste
{
    internal class ConfiguraçãoToolboxTeste : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Teste";

        public override string TooltipInserir { get { return "Inserir um novo Teste"; } }

        public override string TooltipEditar { get { return "Editar um Teste existente"; } }

        public override string TooltipExcluir { get { return "Excluir um Teste existente"; } }

        public override string TooltipAtualizarQuestoes => "Atualizar Questao do Teste";

        public override bool AtualizarQuestoesHabilitado => true;

    }
}
