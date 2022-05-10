using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.WinApp.ModuloQuestao
{
    internal class ConfiguracaoToolboxQuestao : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Questao";

        public override string TooltipInserir { get { return "Inserir uma nova Questao"; } }

        public override string TooltipEditar { get { return "Editar uma Questao existente"; } }

        public override string TooltipExcluir { get { return "Excluir uma Questao existente"; } }
    }
}
