using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programa.Models
{
    public class AtividadeModel
    {
        public int Id { get; set; }
        public string NomeAtividade { get; set; }
        public int IdEmenda { get; set; }
    }
}