using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.Disciplina
{
    public class Questao : EntidadeBase<Questao>
    {
        string questao;
        Dictionary<string,bool> opcoes = new();
        Materia materia;
        int Bimestre;

        public override void Atualizar(Questao registro)
        {
            
        }
    }
}
