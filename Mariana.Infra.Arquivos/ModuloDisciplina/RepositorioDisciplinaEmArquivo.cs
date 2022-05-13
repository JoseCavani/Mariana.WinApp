using FluentValidation;
using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloQuestao;
using Marina.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.Arquivos.ModuloDisciplina
{
    public class RepositorioDisciplinaEmArquivo : RepositorioEmArquivoBase<Disciplina>, IRepositorioDisciplina
    {
        public RepositorioDisciplinaEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Disciplinas.Count > 0)
                contador = dataContext.Disciplinas.Max(x => x.Numero);
        }
        public override List<Disciplina> ObterRegistros()
        {
            return dataContext.Disciplinas;
        }


       

        public override ValidationResult Excluir(Disciplina registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();

            foreach (var item in dataContext.Materias)
            {
                if (item.Disciplina == registro)                
                    resultadoValidacao.Errors.Add(new ValidationFailure("", "Disciplina contem materias"));
            }

            if (resultadoValidacao.Errors.Count == 0)
            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            return resultadoValidacao;
        }




   


        public override AbstractValidator<Disciplina> ObterValidador()
        {
            return new ValidadorDisciplina();
        }

        public List<Questao> SelecionarQuestoes()
        {
            return dataContext.Questoes;
        }

        protected override int PegarContador()
        {
            contador = dataContext.Disciplinas.Max(x => x.Numero);
            return ++contador;
        }
    }
}
