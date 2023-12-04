using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controle_Escolar.Data;

namespace Controle_Escolar.Repository.Atividade
{
    public class AtividadeRepository : IAtividadeRepository
    {
        public readonly BancoContext _bancoContext ;

        public AtividadeRepository(BancoContext bancoContext){
            _bancoContext = bancoContext;
        }
    }
}