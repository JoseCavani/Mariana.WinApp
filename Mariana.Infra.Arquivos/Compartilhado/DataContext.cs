using Mariana.Dominio.Disciplina;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Marina.Infra.Arquivos
{
    [Serializable]
    public class DataContext //Container
    {
        private readonly ISerializador serializador;

        public List<Disciplina> Disciplinas { get; set; }

        public DataContext()
        {
            Disciplinas = new List<Disciplina>();
        }

        public DataContext(ISerializador serializador) : this()
        {
            this.serializador = serializador;

            CarregarDados();
        }

   

        public void GravarDados()
        {
            serializador.GravarDadosEmArquivo(this);
        }

        private void CarregarDados()
        {
            var ctx = serializador.CarregarDadosDoArquivo();
        }
    }
}
