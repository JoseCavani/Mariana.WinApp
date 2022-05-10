using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloMateria
{
    public class ValidadorMateria : AbstractValidator<Materia>
    {
        public ValidadorMateria()
        {
            RuleFor(x => x.Serie)
                .NotNull().NotEmpty().InclusiveBetween(1,2)
                .WithMessage("Serie nao pode ser vazia e tem que estar entre 1 e 2");
        }
    }
}
