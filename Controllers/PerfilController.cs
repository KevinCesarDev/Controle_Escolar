using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Programa.Models;
using Programa.Repository.Prof;
using Programa.Repository.Conta;
using Programa.Repository.Turma;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Programa_DS2.Repository.Diciplina;
using Programa_DS2.Repository.Emenda;
using Programa_DS2.Models;
using Programa.Repository.Nota;

namespace Programa.Controllers
{
    [Authorize]
    public class Perfil : Controller
    {
        public readonly IContaRepository _contaReposuitory;
        public readonly IUserRepository _userRepository;
        public readonly ITurmaRepository _turmaRepository;
        public readonly IDisciplinaRepository _disciplinaRepository;
        public readonly IEmentaRepository _ementaRepository;
        public readonly INotaRepository _notaRepository;

        public Perfil(IContaRepository contaRepository, IUserRepository userRepository, ITurmaRepository turmaRepository, IDisciplinaRepository disciplinaRepository, IEmentaRepository ementaRepository, INotaRepository notaRepository)
        {
            _turmaRepository = turmaRepository;
            _contaReposuitory = contaRepository;
            _userRepository = userRepository;
            _disciplinaRepository = disciplinaRepository;
            _ementaRepository = ementaRepository;
            _notaRepository = notaRepository;
        }
        [Authorize(Roles = "Coordenador, Professor")]
        public IActionResult Index(int idTurma)
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            DTOModel bag = new DTOModel();
            List<TurmaModel> turmas = new List<TurmaModel>();

            var usuario = _userRepository.BuscarUsuarioId(idUsuario);

            if (usuario.IdConta == 1)
            {
                turmas = _turmaRepository.ListaTurmas();
            }
            else if (usuario.IdConta == 2)
            {
                var emendas = _ementaRepository.ListarDisciplinasProf(idUsuario);
                bag.Ementas = emendas;

                foreach (var item in emendas)
                {
                    bool existe = true;
                    var turma = _turmaRepository.BuscarTurma(item.IdTurma);

                    foreach (var item2 in turmas)
                    {
                        if (turma == item2)
                        {
                            existe = false;
                            break;
                        }
                    }
                    if (existe)
                    {
                        turmas.Add(turma);
                    }

                }
            }
        
            bag.Turmas = turmas;

            bag.Usuario = usuario;


            return View(bag);
        }
        [Authorize(Roles = "Coordenador")]
        public IActionResult CadTurma()
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var usuario = _userRepository.BuscarUsuarioId(idUsuario);

            return View(usuario);
        }
        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public IActionResult ValidarTurma(string criarTurma, string nomeTurma)
        {

            var usuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            if (criarTurma == "SIM")
            {
                _turmaRepository.CadastrarTurma(nomeTurma);
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Coordenador")]
        public IActionResult CriarDisciplina()
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var usuario = _userRepository.BuscarUsuarioId(idUsuario);

            return View(usuario);
        }
        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public IActionResult ValidarDisciplina(string nomeDisciplina, string criarDisciplina)
        {
            if (criarDisciplina == "SIM")
            {
                _disciplinaRepository.CriarDisciplina(nomeDisciplina);

            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public IActionResult CadDisciplina(int idTurma, int idDisciplina, int idProf)
        {
            var ementa = _ementaRepository.CadEmentaDisciplina(idDisciplina, idTurma, idProf);
            var alunos = _userRepository.AlunosTurma(idTurma);
            
            foreach(var item in alunos){
                _notaRepository.SalvarNota(item.Id,ementa.Id,0);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Coordenador, Professor")]
        public IActionResult AlunosMatriculados(int idTurma)
        {

            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            List<DisciplinaModel> disciplinas = new List<DisciplinaModel>();
            List<NotaModel> notas = new List<NotaModel>();

            DTOModel bag = new DTOModel();
            bag.Usuario = _userRepository.BuscarUsuarioId(idUsuario);
            bag.Ementas = _ementaRepository.ListarDisciplinasTurma(idTurma);
            bag.Usuarios = _userRepository.AlunosTurma(idTurma);
            

            foreach (var item in bag.Ementas)
            {
                disciplinas.Add(_disciplinaRepository.BuscarDisciplinaId(item.IdDisciplina));
                notas.AddRange(_notaRepository.NotasEmentas(item.Id));
            }
            bag.Notas = notas;
            bag.Disciplinas = disciplinas;

            return View(bag);
        }

        [Authorize(Roles = "Professor")]
        [HttpPost]
        public IActionResult AlterarNota(int idNota, decimal nota, int idAluno, int idEmenta)
        {
            var usuario = _userRepository.BuscarUsuarioId(idAluno);
            
            NotaModel editarNota = new NotaModel(){
                Id = idNota,
                IdAluno = idAluno,
                IdEmenta = idEmenta,
                Nota = nota
            };
            
            _notaRepository.EditarNota(editarNota);

            return RedirectToAction("AlunosMatriculados", new { idTurma = usuario.IdTurma });
        }

    }
}