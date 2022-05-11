using Mariana.Dominio.ModuloDisciplina;
using Marina.Dominio.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mariana.Dominio.ModuloMateria
{
    public interface IRepositorioMateria : IRepositorio<Materia>
    {
        List<Disciplina> ObterDisciplinas();
    }
}
