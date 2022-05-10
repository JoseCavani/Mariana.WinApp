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
        public Dictionary<string,bool> opcoes = new();
        public Materia materia;
        public  int Bimestre;

        public override void Atualizar(Questao registro)
        {
            
        }
    }
}
