using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.WinApp.ModuloDisciplina
{
    public class ConfiguracaoToolboxDisciplina : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Disciplinas";

        public override string TooltipInserir { get { return "Inserir uma nova Disciplinas"; } }

        public override string TooltipEditar { get { return "Editar uma Disciplinas existente"; } }

        public override string TooltipExcluir { get { return "Excluir uma Disciplinas existente"; } }

        public override string TooltipAdicionarQuestoes => "Adicionar Questao para uma Disciplina";

        public override string TooltipAtualizarQuestoes => "Atualizar Questao da Disciplina";

        public override bool AdicionarQuestoesHabilitado => true;

        public override bool AtualizarQuestoesHabilitado => true;

    }
}
