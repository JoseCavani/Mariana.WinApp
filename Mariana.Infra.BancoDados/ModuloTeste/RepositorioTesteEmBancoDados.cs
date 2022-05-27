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
                    M.[Numero] AS NUMEROMATERIA,
                    M.[TITULO] AS TITULOMATERIA,
                    M.[SERIE] AS SERIE,
                    D.[ID],
                    D.[TITULO] AS DISCPLINATITULO
	            FROM 
		            [TBTESTE] AS T INNER JOIN
					[TBDisciplina] AS D 
					ON
					D.Id = T.Discplina_id INNER JOIN
                    [TBMateria] AS M
                    ON
                    T.[Materia_Numero] = M.Numero";

        private const string sqlSelecionarPorID =
            @"SELECT 
	                T.[NUMERO], 
		            T.[TITULO],
                    T.[NUMEROQUESTOES],
                    T.[DISCPLINA_ID],
                    T.[DATA],
                    T.[MATERIA_NUMERO],
                    M.[Numero] AS NUMEROMATERIA,
                    M.[TITULO] AS TITULOMATERIA,
                    M.[SERIE] AS SERIE,
                    D.[ID],
                    D.[TITULO] AS DISCPLINATITULO
	            FROM 
		            [TBTESTE] AS T INNER JOIN
					[TBDisciplina] AS D 
					ON
					D.Id = T.Discplina_id   INNER JOIN
                    [TBMateria] AS M
                    ON
                    T.[Materia_Numero] = M.Numero
                  WHERE
                   T.[NUMERO] = @NUMERO";

        private const string sqlInserirQuestoes =
        @"INSERT INTO [TBTesteQuestao] 
                (
                    [NUMERO_QUESTAO],
                    [NUMERO_TESTE]
	            )
	            VALUES
                (
                    @NUMERO_QUESTAO,
                    @NUMERO_TESTE
                );";


        private const string sqlSelecionarQuestoes =
             @"SELECT 
                    Q.[NUMERO] AS NUMERO,
		            Q.[TITULO] AS TITULO,
                    Q.[BIMESTRE] AS BIMESTRE,
                    Q.[MATERIA_NUMERO]
	            FROM 
		            [TBQUESTAO] AS Q INNER JOIN
                    [TBTESTEQUESTAO] AS TQ
                ON 
                    Q.NUMERO = TQ.NUMERO_QUESTAO INNER JOIN
                    [TBTESTE] AS T
                ON
                    T.NUMERO = TQ.NUMERO_TESTE
                WHERE
                    T.NUMERO = @NUMERO
";



        public ValidationResult Inserir(Teste novoRegistro)
        {
            var validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            SqlCommand InserirQuestoes = new SqlCommand(sqlInserirQuestoes, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);


            foreach (var item in novoRegistro.Questoes)
            {

                ConfigurarParametrosQuestao(item,novoRegistro,InserirQuestoes);

                InserirQuestoes.ExecuteScalar();



            }




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



        private Questao ConverterParaQuestao(SqlDataReader leitor, SqlConnection conexaoComBanco)
        {
            int id = Convert.ToInt32(leitor["NUMERO"]);
            string titulo = Convert.ToString(leitor["TITULO"]);
            int bimestre = Convert.ToInt32(leitor["BIMESTRE"]);


            Dictionary<string, bool> dic = ReceberAlternativas(conexaoComBanco, id);

            var Questao = new Questao
            {
                Numero = id,
                Titulo = titulo,
                bimestre = bimestre,
                opcoes = dic,
            };

            //todo
            return Questao;

         
        }

        private const string sqlSelecionarAlternativas =
       @"SELECT 
		            [TITULO],
                    [CORRETA]
	            FROM 
		            [TBALTERNATIVAS]
		        WHERE
                    [QUESTAO_NUMERO] = @NUMERO";


        private Dictionary<string, bool> ReceberAlternativas(SqlConnection conexaoComBanco, int id)
        {
            SqlCommand comandoSelecaoAlternativa = new SqlCommand(sqlSelecionarAlternativas, conexaoComBanco);

            comandoSelecaoAlternativa.Parameters.AddWithValue("NUMERO", id);

            SqlDataReader leitorAlternativa = comandoSelecaoAlternativa.ExecuteReader();
            Dictionary<string, bool> dic = new();


            dic = ConverterParaAlternativas(leitorAlternativa);
            return dic;
        }


        private Dictionary<string, bool> ConverterParaAlternativas(SqlDataReader leitorAlternativa)
        {
            Dictionary<string, bool> dic = new Dictionary<string, bool>();



            while (leitorAlternativa.Read())
            {
                string titulo = Convert.ToString(leitorAlternativa["TITULO"]);

                bool certo = Convert.ToBoolean(leitorAlternativa["CORRETA"]);

                dic.Add(titulo, certo);
            }

            return dic;
        }




        public List<Teste> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            SqlCommand comandoPegarQuestao = new SqlCommand(sqlSelecionarQuestoes, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Teste> registros = new List<Teste>();

            while (leitor.Read())
            {
                Teste registro = ConverterParaRegistro  (leitor);


                comandoPegarQuestao.Parameters.AddWithValue("NUMERO", registro.Numero);

                SqlDataReader leitor2 = comandoPegarQuestao.ExecuteReader();

                while (leitor2.Read())
                    registro.Questoes.Add(ConverterParaQuestao(leitor2, conexaoComBanco));
                

                leitor2.Close();
                comandoPegarQuestao.Parameters.Clear();

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }

        public Teste SelecionarPorNumero(int Numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorID, conexaoComBanco);

            SqlCommand comandoPegarQuestao = new SqlCommand(sqlSelecionarQuestoes, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", Numero);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Teste registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);


            comandoPegarQuestao.Parameters.AddWithValue("NUMERO", registro.Numero);

            SqlDataReader leitor2 = comandoPegarQuestao.ExecuteReader();

            while (leitor2.Read())
                registro.Questoes.Add(ConverterParaQuestao(leitor2, conexaoComBanco));



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

        private void ConfigurarParametrosQuestao(Questao questao,Teste novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO_TESTE", novoRegistro.Numero);
            comando.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);
        }
        



    }
}