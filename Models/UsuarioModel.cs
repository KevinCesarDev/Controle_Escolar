using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programa.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int IdConta { get; set; }
        public int ? IdTurma {get;set;}
    }
}