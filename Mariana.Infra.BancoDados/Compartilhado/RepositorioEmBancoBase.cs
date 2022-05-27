using FluentValidation;
using FluentValidation.Results;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.BancoDados.Compartilhado
{
    public abstract class RepositorioEmBancoBase<T> where T : EntidadeBase<T>
    {

        protected virtual ValidationResult Validar(string sql, T registro, SqlConnection conexaoComBanco)
        {
            var validator = ObterValidador();

            var resultadoValidacao = validator.Validate(registro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            var sqlCommand = new SqlCommand(sql, conexaoComBanco);


            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Nome já está cadastrado"));

            reader.Close();
            reader.Dispose();


            return resultadoValidacao;
        }

        protected abstract AbstractValidator<T> ObterValidador();



    }
}
