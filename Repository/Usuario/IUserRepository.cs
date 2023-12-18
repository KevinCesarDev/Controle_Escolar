using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Data;
using Programa.Repository.Prof;
using Programa.Models;

namespace Programa.Repository.Prof
{
    public interface IUserRepository
    {
        UsuarioModel EncontrarUsuario(string login, string senha);
        UsuarioModel CadastroUsuario(string nome, string login, string senha, int idConta,int idTurma);
        bool ValidarLogin(string login, string senha);
        UsuarioModel BuscarUsuarioId(int idUsuario);
        List<UsuarioModel>TodosProf();
        List<UsuarioModel>AlunosTurma(int idTurma);
    }
}