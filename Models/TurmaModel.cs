using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Controle_Escolar.Models
{
    public class TurmaModel
    {
        public int Id { get; set; }
        public ProfModel Professor { get; set; }
        public List<AtividadeModel> ListaAtv { get; set; }
    }
}