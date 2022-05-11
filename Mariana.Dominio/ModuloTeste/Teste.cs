using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Mariana.Dominio.ModuloQuestao;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {
        public DateTime Data { get ; set; }
        public string Titulo { get ; set ; }
        public int NumeroQuestoes { get; set ; }
        public List<Questao> Questoes { get ; set ; }
        public Disciplina Disciplina { get ; set; }
        public Materia Materia { get; set; }


        public Teste()
        {
            Data = DateTime.Now;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Titulo);

            foreach (var item in Questoes)
            {
                sb.AppendLine(item.ToString());
            }


            return sb.ToString();
                
        }

        public override void Atualizar(Teste registro)
        {
          
        }
    }
}
