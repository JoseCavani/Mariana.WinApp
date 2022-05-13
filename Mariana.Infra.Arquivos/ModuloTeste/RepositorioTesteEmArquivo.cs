using FluentValidation;
using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloQuestao;
using Mariana.Dominio.ModuloTeste;
using Marina.Dominio.Compartilhado;
using Marina.Infra.Arquivos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.Arquivos.ModuloTeste
{
    public class RepositorioTesteEmArquivo : RepositorioEmArquivoBase<Teste>, IRepositorioTeste
    {
        public RepositorioTesteEmArquivo(DataContext dataContext) : base(dataContext)
        {
            if (dataContext.Teste.Count > 0)
                contador = dataContext.Teste.Max(x => x.Numero);
        }


     
        public List<Disciplina> ObterDiscplinas()
        {
           return dataContext.Disciplinas;
        }

        public override List<Teste> ObterRegistros()
        {
            return dataContext.Teste;
        }


        public override AbstractValidator<Teste> ObterValidador()
        {
            return new ValidadorTeste();
        }

        public List<Questao> SelecionarQuestoes()
        {
            return dataContext.Questoes; 
        }

        protected override int PegarContador()
        {
            if (dataContext.Teste.Count > 0)
            {
                contador = dataContext.Teste.Max(x => x.Numero);
                return ++contador;
            }
            else
                return contador;
        }
    }
}