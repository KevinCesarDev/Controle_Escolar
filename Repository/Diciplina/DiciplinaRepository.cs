using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Data;
using Programa_DS2.Models;

namespace Programa_DS2.Repository.Diciplina
{
    public class DisciplinaRepository : IDisciplinaRepository
    {
        public readonly DataContex _bancoContext;
        public DisciplinaRepository(DataContex bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public DisciplinaModel BuscarDisciplinaId(int idDiciplina)
        {
           var diciplina = _bancoContext.Disciplinas.FirstOrDefault(u => u.Id == idDiciplina);
           return diciplina;
        }

        public DisciplinaModel CriarDisciplina(string nome)
        {
            DisciplinaModel novaDisciplina = new DisciplinaModel{
                Disciplina = nome
            };
            _bancoContext.Disciplinas.Add(novaDisciplina);
            _bancoContext.SaveChanges();
            return novaDisciplina;
        }

        public List<DisciplinaModel> ListarDisciplina()
        {
            var diciplinas = _bancoContext.Disciplinas.ToList();
            return diciplinas;
        }
    }
}