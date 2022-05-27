using Mariana.Dominio.ModuloDisciplina;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>, ICloneable
    {

        public int Serie { get; set; }
        public Disciplina Disciplina { get; set; }

        public Materia(int serie, Disciplina disciplina, string titulo)
        {
            this.Serie = serie;
            this.Disciplina = disciplina;
            Titulo = titulo;
        }

        public Materia()
        {

        }
        public override bool Equals(object obj2)
        {
            Materia obj = obj2 as Materia;
            return obj != null && obj.Numero == this.Numero;
        }

        public override string ToString()
        {
            return $"{Titulo},{Serie}ª";
        }

        public override void Atualizar(Materia registro)
        {
            this.Titulo = registro.Titulo;
            this.Serie= registro.Serie;
            this.Disciplina= registro.Disciplina;
        }

        public object Clone()
        {
            return new Materia
            {
                Numero = this.Numero,
                Titulo = this.Titulo,
                Serie = this.Serie,
                Disciplina = this.Disciplina,
            };
        }
    }
}
