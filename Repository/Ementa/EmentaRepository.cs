using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Programa.Data;
using Programa_DS2.Models;

namespace Programa_DS2.Repository.Emenda
{
    public class EmentaRepository : IEmentaRepository
    {
        public readonly DataContex _bancoContext;
        public EmentaRepository(DataContex bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public EmentaModel CadEmentaDisciplina(int diciplina, int turma, int prof)
        {
            EmentaModel novaDisciplinaEmenda = new EmentaModel
            {
                IdDisciplina = diciplina,
                IdTurma = turma,
                IdProf = prof
            };

            _bancoContext.Ementas.Add(novaDisciplinaEmenda);
            _bancoContext.SaveChanges();

            return novaDisciplinaEmenda;
        }

        public EmentaModel EncontrarEmentaId(int idEmenta)
        {
            var emenda = _bancoContext.Ementas.FirstOrDefault(u => u.Id == idEmenta);
            return emenda;
        }

        public List<EmentaModel> ListarDisciplinasProf(int idProf)
        {
            var disciplinas = _bancoContext.Ementas.Where(u => u.IdProf == idProf);
            return disciplinas.ToList();
        }

        public List<EmentaModel> ListarDisciplinasTurma(int turma)
        {
            var disciplinas = _bancoContext.Ementas.Where(u => u.IdTurma == turma);
            return disciplinas.ToList();
        }
    }
}