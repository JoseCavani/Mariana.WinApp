using FluentValidation;
using FluentValidation.Results;
using Marina.Dominio.Compartilhado;
using System.Collections.Generic;
using System.Linq;

namespace Marina.Infra.Arquivos
{
    public abstract class RepositorioEmArquivoBase<T> where T : EntidadeBase<T>
    {
        protected DataContext dataContext;

        protected int contador = 0;

        protected T registro;

        public RepositorioEmArquivoBase(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public abstract List<T> ObterRegistros();

        public abstract AbstractValidator<T> ObterValidador();

        protected abstract int PegarContador();

        public virtual ValidationResult Inserir(T novoRegistro)
        {
            novoRegistro.Numero = PegarContador();
            var resultadoValidacao = Validar(novoRegistro);

            if (resultadoValidacao.IsValid)
            {
              
                var registros = ObterRegistros();

                registros.Add(novoRegistro);
            }

            return resultadoValidacao;
        }

        public virtual ValidationResult Editar(T registro)
        {
            var resultadoValidacao = Validar(registro);

            if (resultadoValidacao.IsValid)
            {
                var registros = ObterRegistros();

                foreach (var item in registros)
                {
                    if (item.Numero == registro.Numero)
                    {
                        item.Atualizar(registro);
                        break;
                    }
                }
            }

            return resultadoValidacao;
        }

        protected virtual  ValidationResult Validar(T registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var nomeEncontrado = ObterRegistros()
                .Where(x => x.Numero != registro.Numero)
                .ToList()
               .Select(x => x.Titulo.ToLower())
               .Contains(registro.Titulo.ToLower());




            if (nomeEncontrado)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Nome já está cadastrado"));

            return resultadoValidacao;
        }



        public virtual ValidationResult Excluir(T registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            return resultadoValidacao;
        }

        public virtual List<T> SelecionarTodos()
        {
            return ObterRegistros().ToList();
        }

        public virtual T SelecionarPorNumero(int numero)
        {
            return ObterRegistros()
                .FirstOrDefault(x => x.Numero == numero);
        }
    }
}
