using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Data;
using Programa.Models;
using Programa.Repository.Prof;

namespace Programa.Repository.Turma
{
    public class TurmaRepository : ITurmaRepository
    {
        public readonly DataContex _bancoContext;
        public readonly IUserRepository _userRepository;
        public TurmaRepository(DataContex bancoContext, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _bancoContext = bancoContext;
        }
        public TurmaModel CadastrarTurma(string nome)
        {
            TurmaModel novaTurma = new TurmaModel();
            novaTurma.NomeTurma = nome;
            _bancoContext.Turmas.Add(novaTurma);
            _bancoContext.SaveChanges();
            
            return novaTurma;
            
        }

        public List<TurmaModel> ListaTurmas()
        {
            List<TurmaModel> todasTurmas = new List<TurmaModel>();
            
            todasTurmas = _bancoContext.Turmas.ToList();
            
            return todasTurmas;
        }

        public TurmaModel BuscarTurma(int idTurma){
            var turma = _bancoContext.Turmas.Find(idTurma);
            return turma;
        }

        public void ExcluirTurma(int idTurma){
            var turma = _bancoContext.Turmas.Find(idTurma);
            _bancoContext.Turmas.Remove(turma);
            _bancoContext.SaveChanges();
        }
    }
}