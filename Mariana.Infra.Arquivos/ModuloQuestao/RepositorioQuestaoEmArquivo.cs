using FluentValidation;
using FluentValidation.Results;
using Mariana.Dominio.ModuloQuestao;
using Marina.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.Arquivos.ModuloQuestao
{
    public class RepositorioQuestaoEmArquivo : RepositorioEmArquivoBase<Questao>, IRepositorioQuestao
    {
        public RepositorioQuestaoEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Questoes.Count > 0)
                contador = dataContext.Questoes.Max(x => x.Numero);
        }

        public virtual ValidationResult Excluir(Questao registro)
        {
            var resultadoValidacao = new ValidationResult();

            var registros = ObterRegistros();



            foreach (var item in dataContext.Teste)
            {
                foreach (var item2 in item.Questoes)
                {
                    if (item2 == registro)
                    {
                        item.Questoes.Remove(item2);
                        item.NumeroQuestoes -= 1;
                        break;
                    }
                }

            }


            if (registros.Remove(registro) == false)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            return resultadoValidacao;
        }

        public override List<Questao> ObterRegistros()
        {
            return dataContext.Questoes;
        }

        public override AbstractValidator<Questao> ObterValidador()
        {
            return new ValidadorQuestao();
        }

        public List<Questao> SelecionarQuestoes()
        {
            return dataContext.Questoes;
        }

        protected override int PegarContador()
        {
            if (dataContext.Questoes.Count > 0)
            {
                contador = dataContext.Questoes.Max(x => x.Numero);
                return ++contador;
            }
            else
                return contador;
        }
    }
}
