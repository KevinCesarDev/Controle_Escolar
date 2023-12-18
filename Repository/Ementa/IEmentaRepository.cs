using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa_DS2.Models;

namespace Programa_DS2.Repository.Emenda
{
    public interface IEmentaRepository
    {
        EmentaModel CadEmentaDisciplina(int diciplina, int turma, int prof);
        List<EmentaModel> ListarDisciplinasTurma(int turma);
        List<EmentaModel> ListarDisciplinasProf(int idProf);
        EmentaModel EncontrarEmentaId(int idEmenda);
    }
}