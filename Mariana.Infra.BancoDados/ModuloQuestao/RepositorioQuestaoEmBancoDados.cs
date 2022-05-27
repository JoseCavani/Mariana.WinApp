using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.BancoDados.ModuloQuestao
{
    public class RepositorioQuestaoEmBancoDados : IRepositorioQuestao
    {
        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
            "MultipleActiveResultSets=true;" +
              "Initial Catalog=DB;" +
              "Integrated Security=True;" +
              "Pooling=False";


        private const string sqlInserir =
           @"INSERT INTO [TBQUESTAO] 
                (
                    [TITULO],
                    [BIMESTRE],
                    [MATERIA_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @BIMESTRE,
                    @MATERIA_NUMERO
                );SELECT SCOPE_IDENTITY();";


        private const string sqlEditar =
           @"UPDATE [TBQUESTAO]	
		        SET
			        [TITULO] = @TITULO,
                    [BIMESTRE] = @BIMESTRE,
                    [MATERIA_NUMERO] = @MATERIA_NUMERO
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBQUESTAO]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [TITULO],
                    [BIMESTRE],
                    [MATERIA_NUMERO]
	            FROM 
		            [TBQUESTAO]";

        private const string sqlSelecionarPorID =
            @"SELECT 
		            [NUMERO], 
		            [TITULO],
                    [BIMESTRE],
                    [MATERIA_NUMERO]
	            FROM 
		            [TBQUESTAO]
		        WHERE
                    [NUMERO] = @NUMERO";


        private const string sqlInserirAlternativa =
          @"INSERT INTO [TBALTERNATIVAS] 
                (
                    [TITULO],
                    [CORRETA],
                    [QUESTAO_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @CORRETA,
                    @QUESTAO_NUMERO
                );";





        public ValidationResult Inserir(Questao novoRegistro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            SqlCommand comandoInsercaoAlternativas = new SqlCommand(sqlInserirAlternativa, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoInsercao);
            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);


            foreach (var item in novoRegistro.opcoes)
            {
                ConfigurarParametrosAlternativas(item, comandoInsercaoAlternativas, novoRegistro);
                comandoInsercaoAlternativas.ExecuteNonQuery();
            }

        
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        private void ConfigurarParametrosAlternativas(KeyValuePair<string,bool> novoRegistro, SqlCommand comandoInsercaoAlternativas,Questao questao)
        {
            comandoInsercaoAlternativas.Parameters.AddWithValue("TITULO", novoRegistro.Key);
            comandoInsercaoAlternativas.Parameters.AddWithValue("CORRETA", novoRegistro.Value);
            comandoInsercaoAlternativas.Parameters.AddWithValue("QUESTAO_NUMERO", questao.Numero);
        }

        public ValidationResult Editar(Questao novoRegistro)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(novoRegistro);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            SqlCommand comandoInsercaoAlternativas = new SqlCommand(sqlInserirAlternativa, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoEdicao);

            conexaoComBanco.Open();

            foreach (var item in novoRegistro.opcoes)
            {
                var sqlCommand = new SqlCommand("SELECT * FROM TBALTERNATIVAS WHERE ([TITULO] = '" + item.Key + "') AND [QUESTAO_NUMERO] = " + novoRegistro.Numero, conexaoComBanco);
            

                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (!reader.HasRows)
                {
                        ConfigurarParametrosAlternativas(item, comandoInsercaoAlternativas, novoRegistro);
                        comandoInsercaoAlternativas.ExecuteNonQuery();

                }
                reader.Close();
                reader.Dispose();
            
        }


            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao novoRegistro)
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

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);


            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Questao> registros = new List<Questao>();

            while (leitor.Read())
            {
                Questao registro = ConverterParaRegistro(leitor, conexaoComBanco);


                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }

        public Questao SelecionarPorNumero(int Numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorID, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", Numero);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Questao registro = null;
            if (leitor.Read()) { 

            registro = ConverterParaRegistro(leitor, conexaoComBanco);
        }

            conexaoComBanco.Close();

            return registro;
        }

        private Questao ConverterParaRegistro(SqlDataReader leitor, SqlConnection conexaoComBanco)
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


            return Questao;
        }

        private Dictionary<string, bool> ReceberAlternativas(SqlConnection conexaoComBanco, int id)
        {
            SqlCommand comandoSelecaoAlternativa = new SqlCommand(sqlSelecionarAlternativas, conexaoComBanco);

            comandoSelecaoAlternativa.Parameters.AddWithValue("NUMERO", id);

            SqlDataReader leitorAlternativa = comandoSelecaoAlternativa.ExecuteReader();
            Dictionary<string, bool> dic = new();


            dic = ConverterParaAlternativas(leitorAlternativa);
            return dic;
        }


        private Dictionary<string, bool> ConverterParaAlternativas( SqlDataReader leitorAlternativa)
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

        private const string sqlSelecionarAlternativas =
        @"SELECT 
		            [TITULO],
                    [CORRETA]
	            FROM 
		            [TBALTERNATIVAS]
		        WHERE
                    [QUESTAO_NUMERO] = @NUMERO";




        private const string sqlSelecionarMaterias =
          @"  SELECT 
                    M.[NUMERO], 
		            M.[TITULO],
                    M.[SERIE]
	           FROM
                [TBMATERIA] AS M
            WHERE
                @ID = M.Discplina_id
";





        public List<Materia> Materias(Disciplina discplina)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarMaterias, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", discplina.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Materia> registros = new List<Materia>();

            while (leitor.Read())
            {
                Materia registro = ConverterParaRegistroMateria(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }



        private Materia ConverterParaRegistroMateria(SqlDataReader leitor)
        {
            int id = Convert.ToInt32(leitor["NUMERO"]);
            string titulo = Convert.ToString(leitor["TITULO"]);
            int serie = Convert.ToInt32(leitor["SERIE"]);


            var materia = new Materia
            {
                Numero = id,
                Titulo = titulo,
                Serie = serie,
            };

            return materia;
        }


        private void ConfigurarParametros(Questao novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);
            comando.Parameters.AddWithValue("TITULO", novoRegistro.Titulo);
            comando.Parameters.AddWithValue("BIMESTRE", novoRegistro.bimestre);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novoRegistro.materia.Numero);
        }

       
    }
}
