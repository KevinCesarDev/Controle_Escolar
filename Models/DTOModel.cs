using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa_DS2.Models;

namespace Programa.Models
{
    public class DTOModel
    {
        public UsuarioModel Usuario { get; set; }
        public List<TurmaModel> Turmas { get; set; }
        public TurmaModel Turma { get; set; }
        public List<AtividadeModel> Atividades { get; set; }
        public AtividadeModel Atividade { get; set; }
        public List<DisciplinaModel> Disciplinas { get; set; }
        public DisciplinaModel Disciplina { get; set; }
        public List<UsuarioModel> Usuarios { get; set; }
        public List<EmentaModel> Ementas { get; set; }
        public EmentaModel Ementa { get; set; }
        public List<NotaModel> Notas {get;set;}
    }
}