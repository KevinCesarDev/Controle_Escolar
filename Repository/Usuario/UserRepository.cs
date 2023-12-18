using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Data;
using Programa.Models;

namespace Programa.Repository.Prof
{
    public class UserRepository : IUserRepository
    {
        public readonly DataContex _bancoContext;

        public UserRepository(DataContex BancoContext)
        {
            _bancoContext = BancoContext;
        }

        public bool ValidarLogin(string login, string senha)
        {

            UsuarioModel usuario = new UsuarioModel();
            
            usuario = _bancoContext.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);

            if (usuario != null)
            {
                
                return true;
                
            }
            else
            {
                return false;
            }
        }


        public UsuarioModel EncontrarUsuario(string login, string senha)
        {

            var usuario = _bancoContext.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senha);
            return usuario;
        }
        public UsuarioModel BuscarUsuarioId(int idUsuario)
        {
            var usuario = _bancoContext.Usuarios.FirstOrDefault(u => u.Id == idUsuario);
            return usuario;

        }

        public UsuarioModel CadastroUsuario(string nome, string login, string senha, int idConta,int idTurma)
        {
            UsuarioModel novoUsuario = new UsuarioModel();
            novoUsuario.IdConta = idConta;
            novoUsuario.Nome = nome;
            novoUsuario.Login = login;
            novoUsuario.Senha = senha;

            if(idConta == 3 && idTurma != 0){
                novoUsuario.IdTurma = idTurma;
            }else{
                novoUsuario.IdTurma = null;
            }

            _bancoContext.Usuarios.Add(novoUsuario);
            _bancoContext.SaveChanges();

            return novoUsuario;
        }

        public List<UsuarioModel> TodosProf()
        {
            var professores = _bancoContext.Usuarios.Where(u=> u.IdConta == 2).ToList();
            return professores;
        }
        public List<UsuarioModel> AlunosTurma(int idturma){
            var alunos = _bancoContext.Usuarios.Where(u => u.IdTurma == idturma).ToList();
            return alunos;
        }
    }
}