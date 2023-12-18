using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Programa_DS2.Models
{
    public class NotaModel
    {
        public int Id { get; set; }
        public int IdAluno { get; set; }
        public int IdEmenta { get; set; }

        [Column(TypeName = "decimal(4,2)")] 
        public decimal Nota { get; set; }
    }
}