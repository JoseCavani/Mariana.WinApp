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
    public class Teste : EntidadeBase<Teste> , ICloneable
    {
        public DateTime Data { get ; set; }
        public int NumeroQuestoes
        {
            get 
            {
                return Questoes.Count;
            }
        }
        public List<Questao> Questoes { get; set; }
        public Disciplina Disciplina { get ; set; }
        public Materia Materia { get; set; }


        public Teste()
        {
            Data = DateTime.Now;
            Questoes = new List<Questao>();
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
            this.Data = registro.Data;
            this.Titulo = registro.Titulo;
            this.Questoes = registro.Questoes;
            this.Disciplina = registro.Disciplina;
            this.Materia = registro.Materia;
        }

        public object Clone()
        {
            Teste obj = new Teste();
            obj.Numero = this.Numero;
            obj.Data = Data;
            obj.Titulo = Titulo;
            obj.Questoes = Questoes;
            obj.Disciplina = Disciplina;
            obj.Materia = Materia;
            return obj;
        }
    }
}
