using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Data;
using Programa.Models;
using Programa.Repository.Prof;

namespace Programa.Repository.Conta
{
    public class ContaRepository : IContaRepository
    {
        public readonly DataContex _bancoContext;
        public readonly IUserRepository _userRepository;

        public ContaRepository(DataContex BancoContext, IUserRepository userRepository)
        {
            _bancoContext = BancoContext;
            _userRepository = userRepository;
        }

        public int BuscarIdTipo(string tipoConta)
        {
            var idTipoConta = _bancoContext.Contas.FirstOrDefault(u => u.TipoConta == tipoConta);
            return idTipoConta.Id;
        }

        public string BuscarRole(int idTipoConta)
        {
            
            var Conta = _bancoContext.Contas.Find(idTipoConta);
            return Conta.TipoConta;
        }
    }
}