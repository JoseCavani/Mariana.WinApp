using FluentValidation;
using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
using Mariana.Infra.BancoDados.Compartilhado;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.BancoDados.ModuloDisciplina
{
    public class RepositorioDisciplinaEmBancoDados : RepositorioEmBancoBase<Disciplina>, IRepositorioDisciplina
    {

        private const string enderecoBanco =
              "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog=DB;" +
               "Integrated Security=True;" +
               "Pooling=False";


        private const string sqlInserir =
           @"INSERT INTO [TBDISCIPLINA] 
                (
                    [TITULO]
	            )
	            VALUES
                (
                    @TITULO
                );SELECT SCOPE_IDENTITY();";




        private const string sqlSelecionarQuestoes =
            @"SELECT 
		            Q.[NUMERO] AS NUMERO,
		            Q.[TITULO] AS TITULO,
                    Q.[BIMESTRE],
                    Q.[MATERIA_NUMERO],
                    M.[NUMERO] AS NUMEROMATERIA,
                    M.[TITULO] AS TITULOMATERIA,
                    M.[SERIE],
                    M.[DISCPLINA_ID]
	           FROM
                [TBQUESTAO] AS Q INNER JOIN 
                [TBMATERIA] AS M
            ON
              M.Numero = Q.Materia_Numero 

                WHERE
            
               @ID = M.Discplina_id";          




        private const string sqlEditar =
           @"UPDATE [TBDISCIPLINA]	
		        SET
			        [TITULO] = @TITULO
		        WHERE
			        [ID] = @ID";

        private const string sqlExcluir =
            @"DELETE FROM [TBDISCIPLINA]
		        WHERE
			        [ID] = @ID";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [ID], 
		            [TITULO]
	            FROM 
		            [TBDISCIPLINA]";

        private const string sqlSelecionarPorID =
            @"SELECT 
		            [ID], 
		            [TITULO]
	            FROM 
		            [TBDISCIPLINA]
		        WHERE
                    [ID] = @ID";



        public ValidationResult Inserir(Disciplina novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            conexaoComBanco.Open();


            var resultadoValidacao = Validar("SELECT * FROM TBDISCIPLINA WHERE ([TITULO] = '" + novoRegistro.Titulo + "')",novoRegistro, conexaoComBanco);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

      

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosDiscplina(novoRegistro, comandoInsercao);

            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public ValidationResult Editar(Disciplina novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
            conexaoComBanco.Open();


            var resultadoValidacao = Validar("SELECT * FROM TBDISCIPLINA WHERE ([TITULO] = '" + novoRegistro.Titulo + "')", novoRegistro, conexaoComBanco);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosDiscplina(novoRegistro, comandoEdicao);

            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Disciplina novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("ID", novoRegistro.Numero);

            conexaoComBanco.Open();
            int IDRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (IDRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Disciplina> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Disciplina> registros = new List<Disciplina>();

            while (leitor.Read())
            {
                Disciplina registro = ConverterParaRegistro(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }

        public Disciplina SelecionarPorNumero(int ID)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorID, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", ID);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Disciplina registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);

            conexaoComBanco.Close();

            return registro;
        }

        private  Disciplina ConverterParaRegistro(SqlDataReader leitor)
        {
            int id = Convert.ToInt32(leitor["ID"]);
            string titulo = Convert.ToString(leitor["TITULO"]);

            var disciplina = new Disciplina
            {
                Numero = id,
                Titulo = titulo,
            };
            disciplina.questoes = SelecionarQuestoes(disciplina);

            return disciplina;
        }



        private static void ConfigurarParametrosDiscplina(Disciplina novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", novoRegistro.Numero);
            comando.Parameters.AddWithValue("TITULO", novoRegistro.Titulo);
        }


        private Questao ConverterParaQuestao(SqlDataReader leitor,Disciplina discplina)
        {
            int id = Convert.ToInt32(leitor["NUMERO"]);
            string titulo = Convert.ToString(leitor["TITULO"]);
            int bimestre = Convert.ToInt32(leitor["BIMESTRE"]);

            int idMateria = Convert.ToInt32(leitor["NUMEROMATERIA"]);
            string tituloMateria = Convert.ToString(leitor["TITULOMATERIA"]);
            int serie = Convert.ToInt32(leitor["SERIE"]);

            var materia = new Materia
            {
                Titulo = tituloMateria,
                Numero = idMateria,
                Serie = serie,
                Disciplina = discplina,
            };


            var Questao = new Questao
            {
                Numero = id,
                Titulo = titulo,
                bimestre = bimestre,
                materia = materia,
            };


            return Questao;
        }

        public List<Questao> SelecionarQuestoes(Disciplina novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarQuestoes, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("ID", novoRegistro.Numero);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Questao> registros = new List<Questao>();

            while (leitor.Read())
            {
                Questao registro = ConverterParaQuestao(leitor, novoRegistro);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }

        protected override AbstractValidator<Disciplina> ObterValidador()
        {
            return new ValidadorDisciplina();
        }
    }
}
