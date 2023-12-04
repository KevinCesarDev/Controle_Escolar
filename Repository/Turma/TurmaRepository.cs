using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controle_Escolar.Data;

namespace Controle_Escolar.Repository.Turma
{
    public class TurmaRepository : ITurmaRepository
    {
        public readonly BancoContext _bancoContext;

        public TurmaRepository(BancoContext bancoContext){
            _bancoContext = bancoContext;
        }
    }
}