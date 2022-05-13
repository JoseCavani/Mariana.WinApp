using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.WinApp.ModuloTeste
{
    internal class ConfiguracaoToolboxTeste : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Teste";

        public override string TooltipInserir { get { return "Inserir um novo Teste"; } }

        public override string TooltipEditar { get { return "Editar um Teste existente"; } }

        public override string TooltipExcluir { get { return "Excluir um Teste existente"; } }

        public override string TooltipAtualizarQuestoes => "Atualizar Questao do Teste";

        public override string TooltipGabarito => "Fazer um PDF dos gabaritos";

        public override string TooltipPDF => "Biaxar um PDF das questoes";

        public override string TooltipDuplicar => "Duplicar o Teste";

        public override bool AtualizarQuestoesHabilitado => true;

        public override bool GabaritoHabilitado => true;

        public override bool PDFHabilitado => true;

        public override bool DuplicarHabilitado => true;
    }
}
