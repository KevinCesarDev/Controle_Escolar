using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Models;

namespace Programa.Repository.Turma
{
    public interface ITurmaRepository
    {
        TurmaModel CadastrarTurma(string nome);
        List<TurmaModel> ListaTurmas();

        TurmaModel BuscarTurma (int idTurma);

        void ExcluirTurma (int idTurma);
    }
}