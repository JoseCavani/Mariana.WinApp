﻿using Mariana.Dominio.ModuloDisciplina;
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


        public Teste()
        {

        }

   
        public override void Atualizar(Teste registro)
        {
            throw new NotImplementedException();
        }
    }
}