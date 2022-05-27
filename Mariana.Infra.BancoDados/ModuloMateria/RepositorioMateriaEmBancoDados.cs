using FluentValidation;
using FluentValidation.Results;
using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Infra.BancoDados.Compartilhado;
using Mariana.Infra.BancoDados.ModuloDisciplina;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Infra.BancoDados.ModuloMateria
{
    public class RepositorioMateriaEmBancoDados : RepositorioEmBancoBase<Materia>, IRepositorioMateria
    {
        private RepositorioDisciplinaEmBancoDados repositorioDiscplinaEmBancoDados;
        public RepositorioMateriaEmBancoDados(RepositorioDisciplinaEmBancoDados repositorioDiscplinaEmBancoDados)
        {
            this.repositorioDiscplinaEmBancoDados = repositorioDiscplinaEmBancoDados;
        }
        private const string enderecoBanco =
             "Data Source=(LocalDB)\\MSSqlLocalDB;" +
              "Initial Catalog=DB;" +
              "Integrated Security=True;" +
              "Pooling=False";


        private const string sqlInserir =
           @"INSERT INTO [TBMATERIA] 
                (
                    [TITULO],
                    [SERIE],
                    [DISCPLINA_ID]
	            )
	            VALUES
                (
                    @TITULO,
                    @SERIE,
                    @Discplina_id
                );SELECT SCOPE_IDENTITY();";




        private const string sqlEditar =
           @"UPDATE [TBMATERIA]	
		        SET
			        [TITULO] = @TITULO,
                    [SERIE] = @SERIE,
                    [DISCPLINA_ID] = @DISCPLINA_ID
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBMATERIA]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
            @"SELECT 
		            [NUMERO], 
		            [TITULO],
                    [SERIE],
                    [DISCPLINA_ID]
	            FROM 
		            [TBMATERIA]";

        private const string sqlSelecionarPorID =
            @"SELECT 
		            [NUMERO], 
		            [TITULO],
                    [SERIE],
                    [DISCPLINA_ID]
	            FROM 
		            [TBMATERIA]
		        WHERE
                    [NUMERO] = @NUMERO";



        public ValidationResult Inserir(Materia novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            conexaoComBanco.Open();


            var resultadoValidacao = Validar("SELECT * FROM TBMATERIA WHERE ([TITULO] = '" + novoRegistro.Titulo + "')", novoRegistro, conexaoComBanco);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoInsercao);

            var id = comandoInsercao.ExecuteScalar();
            novoRegistro.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }


        public ValidationResult Editar(Materia novoRegistro)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            conexaoComBanco.Open();


            var resultadoValidacao = Validar("SELECT * FROM TBMATERIA WHERE ([TITULO] = '" + novoRegistro.Titulo + "')", novoRegistro, conexaoComBanco);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;


            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametros(novoRegistro, comandoEdicao);

            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Materia novoRegistro)
        {
            var resultadoValidacao = new ValidationResult();
            int IDRegistrosExcluidos = 0;
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);

            conexaoComBanco.Open();
            try
            {
                IDRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException)
            {
                resultadoValidacao.Errors.Add(new ValidationFailure("", "existe teste com essa materia"));
            }



            if (IDRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            List<Materia> registros = new List<Materia>();

            while (leitor.Read())
            {
                Materia registro = ConverterParaRegistro(leitor);

                registros.Add(registro);
            }

            conexaoComBanco.Close();

            return registros;
        }

        public Materia SelecionarPorNumero(int Numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorID, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("NUMERO", Numero);

            conexaoComBanco.Open();
            SqlDataReader leitor = comandoSelecao.ExecuteReader();

            Materia registro = null;
            if (leitor.Read())
                registro = ConverterParaRegistro(leitor);

            conexaoComBanco.Close();

            return registro;
        }

        private Materia ConverterParaRegistro(SqlDataReader leitor)
        {
            int id = Convert.ToInt32(leitor["NUMERO"]);
            string titulo = Convert.ToString(leitor["TITULO"]);
            int serie = Convert.ToInt32(leitor["SERIE"]);
            Disciplina disciplina = repositorioDiscplinaEmBancoDados.SelecionarPorNumero(Convert.ToInt32(leitor["DISCPLINA_ID"]));





            var materia = new Materia
            {
                Numero = id,
                Titulo = titulo,
                Serie = serie,
                Disciplina = disciplina
            };

            return materia;
        }






        private void ConfigurarParametros(Materia novoRegistro, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novoRegistro.Numero);
            comando.Parameters.AddWithValue("TITULO", novoRegistro.Titulo);
            comando.Parameters.AddWithValue("SERIE", novoRegistro.Serie);
            comando.Parameters.AddWithValue("DISCPLINA_ID", novoRegistro.Disciplina.Numero);
        }

        public List<Disciplina> ObterDisciplinas()
        {
            return repositorioDiscplinaEmBancoDados.SelecionarTodos();
        }

        protected override AbstractValidator<Materia> ObterValidador()
        {
            return new ValidadorMateria();
        }
    }
}