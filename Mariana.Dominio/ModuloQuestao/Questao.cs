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
    public class Questao : EntidadeBase<Questao>
    {
        public string questao;
        public Dictionary<string,bool> opcoes;
        public Materia materia;
        public  int Bimestre;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(questao);

            foreach (var item in opcoes)
            {
                sb.AppendLine(item.Key);
            }

            return sb.ToString();
        }

        public override void Atualizar(Questao registro)
        {
            
        }
    }
}
