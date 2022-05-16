using FluentValidation;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.Compartilhado
{
    public class ValidadorBase<T> : AbstractValidator<T> where T : EntidadeBase<T>
    {
        public ValidadorBase()
        {
            RuleFor(x => x.Titulo)
                .NotNull().NotEmpty()
                .WithMessage("Tiutlo nao pode ser vazia");
        }
    }
}