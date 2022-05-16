using FluentValidation;
using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Marina.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.Arquivos.ModuloMateria
{
    public class RepositorioMaterEmArquivo : RepositorioEmArquivoBase<Materia>, IRepositorioMateria
    {
        public RepositorioMaterEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Materias.Count > 0)
                contador = dataContext.Materias.Max(x => x.Numero);
        }


        public override ValidationResult Excluir(Materia registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            if (resultadoValidacao.IsValid)
            {
                dataContext.Questoes.RemoveAll(x => x.materia == registro);
            }

            return resultadoValidacao;
        }

        public List<Disciplina> ObterDisciplinas()
        {
           return dataContext.Disciplinas;
        }

        public override List<Materia> ObterRegistros()
        {
            return dataContext.Materias;
        }

        public override AbstractValidator<Materia> ObterValidador()
        {
            return new ValidadorMateria();
        }

        protected override int PegarContador()
        {
            if (dataContext.Materias.Count > 0)
            {
                contador = dataContext.Materias.Max(x => x.Numero);
                return ++contador;
            }
            else
                return contador;
        }
    }
}
