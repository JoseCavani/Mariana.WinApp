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


            if (resultadoValidacao.IsValid == false || dataContext.Teste.Count == 0)
                return resultadoValidacao;


            foreach (var item in dataContext.Teste)
            {
                if (item.Disciplina == registro)                
                    resultadoValidacao.Errors.Add(new ValidationFailure("", "Disciplina contem testes"));
            }

            if (resultadoValidacao.Errors.Count == 0)
            if (dataContext.Disciplinas.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            return resultadoValidacao;
        }



        protected override ValidationResult Validar(Disciplina registro)
        {
            var validator = ObterValidador();

            var resultadoValidacao = base.Validar(registro);

            var nomeEncontrado = ObterRegistros()
               .Where(x => x.Numero != registro.Numero)
               .ToList()
              .Select(x => x.Titulo.ToLower())
              .Contains(registro.Titulo.ToLower());




            if (nomeEncontrado)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Nome já está cadastrado"));


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
            if (dataContext.Disciplinas.Count > 0)
            {
                contador = dataContext.Disciplinas.Max(x => x.Numero);
                return ++contador;
            }
            else
                return contador;
        }
    }
}
