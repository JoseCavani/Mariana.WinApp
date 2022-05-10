using Mariana.WinApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.WinApp.ModuloMateria
{
    internal class ConfiguracaoToolboxMateria : ConfiguracaoToolboxBase
    {
        public override string TipoCadastro => "Controle de Materias";

        public override string TooltipInserir { get { return "Inserir uma nova Materia"; } }

        public override string TooltipEditar { get { return "Editar uma Materia existente"; } }

        public override string TooltipExcluir { get { return "Excluir uma Materia existente"; } }

    }
}
