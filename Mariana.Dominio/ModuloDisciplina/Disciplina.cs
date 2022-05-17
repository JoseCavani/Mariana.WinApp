using Mariana.Dominio.ModuloQuestao;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloDisciplina
{
    public class Disciplina : EntidadeBase<Disciplina>, ICloneable
    {
        public List<Questao> questoes = new List<Questao>();

        public Disciplina()
        {

        }

        public Disciplina(string titulo)
        {
            this.Titulo = titulo;
        }

        public override string ToString()
        {
            return Titulo;
        }

        public override void Atualizar(Disciplina registro)
        {
            this.Titulo = registro.Titulo;
        }

        public object Clone()
        {
          return  new Disciplina
            {
                Numero = this.Numero,
                Titulo = this.Titulo,
              questoes = this.questoes,
          };
        }

       
    }
}
