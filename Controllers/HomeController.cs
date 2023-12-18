using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Programa.Models;
using Programa.Repository.Conta;
using Programa.Repository.Nota;
using Programa.Repository.Prof;
using Programa.Repository.Turma;
using Programa_DS2.Repository.Emenda;

namespace Programa.Controllers;

public class HomeController : Controller
{

    public readonly IContaRepository _contaRepository;
    public readonly IUserRepository _userRepository;
    public readonly ITurmaRepository _turmaRepository;
    public readonly INotaRepository _notaRepository;
    public readonly IEmentaRepository _ementaRepository;

    public HomeController(IContaRepository contaRepository, IUserRepository userRepository, ITurmaRepository turmaRepository, INotaRepository notaRepository, IEmentaRepository ementaRepository)
    {
        _contaRepository = contaRepository;
        _userRepository = userRepository;
        _turmaRepository = turmaRepository;
        _notaRepository = notaRepository;
        _ementaRepository = ementaRepository;
    }

    public IActionResult Index()
    {

        return View();
    }
    [HttpPost]
    public IActionResult Validar(string login, string senha)
    {

        if (_userRepository.ValidarLogin(login, senha))
        {
            var contaValida = _userRepository.EncontrarUsuario(login, senha);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, contaValida.Nome),
                new Claim(ClaimTypes.NameIdentifier, contaValida.Id.ToString()),
                new Claim(ClaimTypes.Role, _contaRepository.BuscarRole(contaValida.IdConta)),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
            });
            if (contaValida.IdTurma != null)
            {
                return RedirectToAction("Index", "Turma", new { idTurma = contaValida.IdTurma });
            }
            else
            {
                return RedirectToAction("Index", "Perfil");
            }
        }
        else
        {
            ViewBag.Erro = "Login Não Validado";
            ModelState.AddModelError(string.Empty, "Login Inválido");
            return View("Index", ViewBag.Erro);
        }



    }
    public IActionResult CadastroUsuario()
    {
        DTOModel bag = new DTOModel();
        bag.Turmas = _turmaRepository.ListaTurmas();
        return View(bag);
    }

    [HttpPost]
    public IActionResult CadastroUsuario(string login, string senha, string nome, string tipoConta, int idTurma)
    {

        if ((login != null && senha != null && nome != null && tipoConta == "Professor") || (login != null && senha != null && nome != null && tipoConta == "Aluno" && idTurma > 0))
        {
            var idConta = _contaRepository.BuscarIdTipo(tipoConta);

            var novoUsuario = _userRepository.CadastroUsuario(nome, login, senha, idConta, idTurma);
            if (novoUsuario.IdConta == 3)
            {
                var emetas = _ementaRepository.ListarDisciplinasTurma(idTurma);
                foreach (var item in emetas)
                {
                    _notaRepository.SalvarNota(novoUsuario.Id,item.Id,0);
                }
            }
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Erro = "Verifique os Campos";
            DTOModel bag = new DTOModel();
            bag.Turmas = _turmaRepository.ListaTurmas();
            return View("CadastroUsuario", bag);
        }


    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return View("Index");
    }
}
