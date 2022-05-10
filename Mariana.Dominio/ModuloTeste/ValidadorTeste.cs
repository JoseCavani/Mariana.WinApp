using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloTeste
{
    public class ValidadorTeste : AbstractValidator<Teste>
    {
        public ValidadorTeste()
        {
            RuleFor(x => x.Questoes.Count).Equal(x => x.NumeroQuestoes).WithMessage("não ha questoes suficientes com esses criterios");
        }
    }
}
