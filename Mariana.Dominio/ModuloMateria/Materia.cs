using Mariana.Dominio.ModuloDisciplina;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {

        public string Titulo { get; set; }
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


        public override string ToString()
        {
            return $"{Titulo},{Serie}ª";
        }

        public override void Atualizar(Materia registro)
        {
            
        }
    }
}
