using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
using Mariana.Dominio.ModuloTeste;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.BancoDados.ModuloTeste
{
    public class RepositorioTesteEmBancoDados : IRepositorioTeste
    {
        private const string enderecoBanco =
            "Data Source=(LocalDB)\\MSSqlLocalDB;" +
           "MultipleActiveResultSets=true;" +
             "Initial Catalog=DB;" +
             "Integrated Security=True;" +
             "Pooling=False";


        private const string sqlInserir =
           @"INSERT INTO [TBTESTE] 
                (
                    [TITULO],
                    [NUMEROQUESTOES],
                    [DISCPLINA_ID],
                    [DATA],
                    [MATERIA_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @NUMEROQUESTOES,
                    @DISCPLINA_ID,
                    @DATA,
                    @MATERIA_NUMERO
                );SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
           @"UPDATE [TBTESTE]	
		        SET
			        [TITULO] = @TITULO,
                    [NUMEROQUESTOES] = @NUMEROQUESTOES,
                    [DISCPLINA_ID] = @DISCPLINA_ID,
                    [DATA] = @DATA,
                    [MATERIA_NUMERO] = @MATERIA_NUMERO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBTESTE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            T.[NUMERO] AS NUMERO, 
		            T.[TITULO] AS TITULO,
                    T.[DATA],
                    T.[NUMEROQUESTOES],
                    T.[DISCPLINA_ID],
                    T.[MATERIA_NUMERO],
                    D.[ID],
                    D.[TITULO] AS DISCPLINATITULO
	            FROM 
		            [TBTESTE] AS T INNER JOIN
					[TBDisciplina] AS D 
					ON
					D.Id = T.Discplina_id";

        private const string sqlSelecionarPorID =
            @"SELECT 
	                T.[NUMERO], 
		            T.[TITULO],
                    T.[NUMEROQUESTOES],
                    T.[DISCPLINA_ID],
                    T.[DATA],
                    T.[MATERIA_NUMERO],
                    D.[ID],
                    D.[TITULO] AS DISCPLINATITULO
	            FROM 
		            [TBTESTE] AS T INNER JOIN
					[TBDisciplina] AS D 
					ON
					D.Id = T.Discplina_id 
                  WHERE
                   T.[NUMERO] = @NUMERO";


        public ValidationResult Inserir(Teste novoRegistro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public ValidationResult Editar(Teste novoRegistro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Teste novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);

            conexaoComBanco.Open();
            int IDRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (IDRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public List<Teste> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Teste> registros = new List<Teste>();

            while (leitor.Read())
            {
                Teste registro = ConverterParaRegistro(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }

        public Teste SelecionarPorNumero(int Numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorID, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", Numero);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Teste registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);

            conexaoComBanco.Close();

            return registro;
        }

        private Teste ConverterParaRegistro(SqlDataReader leitor)
        {
            int MateriaNumero = 0;
            Materia materia = null;
            int id = Convert.ToInt32(leitor["NUMERO"]);
            string tituloTeste = Convert.ToString(leitor["TITULO"]);
            int discplina_id = Convert.ToInt32(leitor["DISCPLINA_ID"]);
            if(!Convert.IsDBNull(leitor["MATERIA_NUMERO"]))
             MateriaNumero = Convert.ToInt32(leitor["MATERIA_NUMERO"]);

            int disciplina_Id = Convert.ToInt32(leitor["ID"]);
            string discplinaTitulo = Convert.ToString(leitor["DISCPLINATITULO"]);

            var discplina = new Disciplina
            {
                Numero = discplina_id,
                Titulo = discplinaTitulo,
            };

            if (MateriaNumero != 0)
            {
                int materiaNumero = Convert.ToInt32(leitor["NUMEROMATERIA"]);
                string tituloMateria = Convert.ToString(leitor["TITULOMATERIA"]);
                int materiaSerie = Convert.ToInt32(leitor["SERIE"]);
                int materiaDiscpliaId = discplina_id;

                 materia = new Materia
                {
                    Numero = MateriaNumero,
                    Titulo = tituloMateria,
                    Serie = materiaSerie,
                    Disciplina = discplina
                };
            }          



            var teste = new Teste
            {
                Numero = id,
                Titulo = tituloTeste,
                Disciplina = discplina,
                Materia = materia,
            };

            return teste;
        }




        private void ConfigurarParametros(Teste novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);
            comando.Parameters.AddWithValue("TITULO", novoRegistro.Titulo);
            if (novoRegistro.Materia != null)
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novoRegistro.Materia.Numero);
            else
            comando.Parameters.AddWithValue("MATERIA_NUMERO", DBNull.Value);
            comando.Parameters.AddWithValue("DISCPLINA_ID", novoRegistro.Disciplina.Numero);
            comando.Parameters.AddWithValue("NUMEROQUESTOES", novoRegistro.NumeroQuestoes);
            comando.Parameters.AddWithValue("DATA", novoRegistro.Data);
        }
    }
}