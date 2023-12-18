using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Models;

namespace Programa.Repository.Conta
{
    public interface IContaRepository
    {
        string BuscarRole(int idTipoConta);
        int BuscarIdTipo(string tipoConta);
    }
}