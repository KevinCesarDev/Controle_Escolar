using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Programa.Models;
using Programa.Repository.Atividade;
using Programa.Repository.Nota;
using Programa.Repository.Prof;
using Programa.Repository.Turma;
using Programa_DS2.Repository.Diciplina;
using Programa_DS2.Repository.Emenda;

namespace Programa.Controllers
{
    [Authorize]
    public class TurmaController : Controller
    {
        public readonly IUserRepository _userRepository;
        public readonly ITurmaRepository _turmaRepository;
        public readonly IAtividadeRepository _atividadeRepository;
        public readonly IDisciplinaRepository _disciplinaRepository;
        public readonly IEmentaRepository _ementaReposity;
        public readonly INotaRepository _notaRespository;
        public TurmaController(IUserRepository userRepository, ITurmaRepository turmaRepository, IAtividadeRepository atividadeRepository, IDisciplinaRepository disciplinaRepository, IEmentaRepository ementaRepository,INotaRepository notaRepository)
        {
            _userRepository = userRepository;
            _turmaRepository = turmaRepository;
            _atividadeRepository = atividadeRepository;
            _disciplinaRepository = disciplinaRepository;
            _ementaReposity = ementaRepository;
            _notaRespository = notaRepository;
        }


        public IActionResult Index(int idTurma)
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            DTOModel bag = new DTOModel();


            bag.Usuario = _userRepository.BuscarUsuarioId(idUsuario);
            bag.Turma = _turmaRepository.BuscarTurma(idTurma);
            bag.Ementas = _ementaReposity.ListarDisciplinasTurma(idTurma);
            bag.Disciplinas = _disciplinaRepository.ListarDisciplina();

            List<AtividadeModel> atividades = new List<AtividadeModel>();
            List<UsuarioModel> profs = new List<UsuarioModel>();

            foreach (var item1 in bag.Ementas)
            {
                profs.Add(_userRepository.BuscarUsuarioId(item1.IdProf));
                atividades.AddRange(_atividadeRepository.ListaAtividades(item1.Id));
            }
            bag.Notas = _notaRespository.NotasAluno(idUsuario);

            bag.Atividades = atividades;
            bag.Usuarios = profs;


            return View(bag);
        }
        [Authorize(Roles ="Coordenador")]
        public IActionResult DisciplinaTurma(int idTurma)
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            DTOModel bag = new DTOModel();
            bag.Usuario = _userRepository.BuscarUsuarioId(idUsuario);
            bag.Turma = _turmaRepository.BuscarTurma(idTurma);
            bag.Usuarios = _userRepository.TodosProf();
            bag.Disciplinas = _disciplinaRepository.ListarDisciplina();

            return View(bag);
        }

        public IActionResult AtvTurma(int idEmenda)
        {
            System.Console.WriteLine(idEmenda);
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            DTOModel bag = new DTOModel();
            bag.Usuario = _userRepository.BuscarUsuarioId(idUsuario);
            bag.Ementa = _ementaReposity.EncontrarEmentaId(idEmenda);
            bag.Disciplina = _disciplinaRepository.BuscarDisciplinaId(bag.Ementa.IdDisciplina);
            bag.Atividades = _atividadeRepository.ListaAtividades(idEmenda);
            bag.Turma = _turmaRepository.BuscarTurma(bag.Ementa.IdTurma);

            return View(bag);
        }
        [Authorize(Roles ="Professor")]
        public IActionResult CadAtividade(int idEmenda)
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            DTOModel bag = new DTOModel();
            bag.Usuario = _userRepository.BuscarUsuarioId(idUsuario);
            bag.Ementa = _ementaReposity.EncontrarEmentaId(idEmenda);
            bag.Disciplina = _disciplinaRepository.BuscarDisciplinaId(bag.Ementa.IdDisciplina);

            return View(bag);
        }
        [Authorize(Roles ="Professor")]
        [HttpPost]
        public IActionResult ValidarAtividade(int idEmenda, string nomeAtividade, string criarAtividade)
        {

            if ("SIM" == criarAtividade)
            {
                AtividadeModel novaAtv = new AtividadeModel();
                novaAtv.NomeAtividade = nomeAtividade;
                novaAtv.IdEmenda = idEmenda;
                if (novaAtv.NomeAtividade != null)
                {
                    _atividadeRepository.CadastrarAtv(novaAtv);
                }

            }
            return RedirectToAction("Index", "Perfil");
        }
        [Authorize(Roles ="Coordenador")]
        public IActionResult ExcluirTurma(int idTurma)
        {
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            DTOModel bag = new DTOModel();
            var usuario = _userRepository.BuscarUsuarioId(idUsuario);
            var turma = _turmaRepository.BuscarTurma(idTurma);
            bag.Usuario = usuario;
            bag.Turma = turma;

            return View(bag);
        }
        [Authorize(Roles ="Coordenador")]
        [HttpPost]
        public IActionResult ConfirmarExclusao(int idTurma, string excluirTurma)
        {
            if (excluirTurma == "SIM")
            {
                _turmaRepository.ExcluirTurma(idTurma);
            }
            return RedirectToAction("Index","Perfil");
        }
        [Authorize(Roles ="Professor")]
        public IActionResult ExcluirAtividade(int idAtividade){
            var idUsuario = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            DTOModel bag = new DTOModel();
            bag.Usuario = _userRepository.BuscarUsuarioId(idUsuario);
            bag.Atividade = _atividadeRepository.BuscarAtividadeId(idAtividade);

            return View(bag);
        }
        [Authorize(Roles ="Professor")]
        [HttpPost]
        public IActionResult ValidarExclusaoAtv(int idAtividade, string excluirAtividade){
            if(excluirAtividade == "SIM"){
                _atividadeRepository.ExcluirAtividade(idAtividade);
            }
            return RedirectToAction("Index","Perfil");
        }

    }
}