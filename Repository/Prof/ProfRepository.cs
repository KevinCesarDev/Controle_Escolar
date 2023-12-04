using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controle_Escolar.Data;

namespace Controle_Escolar.Repository.Prof
{
    public class ProfRepository : IProfRepository
    {
        public readonly BancoContext _bancoContext;

        public ProfRepository (BancoContext bancoContext){
            _bancoContext = bancoContext;
        }
    }
}