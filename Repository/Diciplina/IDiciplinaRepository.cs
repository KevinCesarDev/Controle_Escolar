using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa_DS2.Models;

namespace Programa_DS2.Repository.Diciplina
{
    public interface IDisciplinaRepository
    {
        DisciplinaModel CriarDisciplina(string nome);
        List<DisciplinaModel> ListarDisciplina();
        DisciplinaModel BuscarDisciplinaId(int idDiciplina);
    }
}