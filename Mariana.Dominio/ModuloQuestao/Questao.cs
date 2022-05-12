using Mariana.Dominio.ModuloDisciplina;
using Mariana.Dominio.ModuloMateria;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloQuestao
{
    public class Questao : EntidadeBase<Questao>, ICloneable
    {

        public Dictionary<string,bool> opcoes = new();
        public Materia materia;
        public  int Bimestre;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Titulo);

            foreach (var item in opcoes)
            {
                sb.AppendLine(item.Key);
            }

            return sb.ToString();
        }

        public override void Atualizar(Questao registro)
        {
            this.Titulo = registro.Titulo;
            this.opcoes = registro.opcoes;
            this.materia = registro.materia;
            this.Bimestre = registro.Bimestre;
        }

    
       public object Clone()
        {
            return new Questao
            {
                Numero = this.Numero,
                Titulo = this.Titulo,
                opcoes = this.opcoes,
                materia = this.materia,
                Bimestre = this.Bimestre,
            };
        }
    }
}
