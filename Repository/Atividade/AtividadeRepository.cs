using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Programa.Data;
using Programa.Models;

namespace Programa.Repository.Atividade
{
    public class AtividadeRepository : IAtividadeRepository
    {
        public readonly DataContex _bancoContext;
        public AtividadeRepository(DataContex bancoContex)
        {
            _bancoContext = bancoContex;
        }
        public AtividadeModel CadastrarAtv(AtividadeModel novaAtv)
        {
            _bancoContext.Atividades.Add(novaAtv);
            _bancoContext.SaveChanges();
            return novaAtv;
            
        }

        public AtividadeModel ExcluirAtividade(int idAtividade)
        {
            var atv = _bancoContext.Atividades.Find(idAtividade);
            _bancoContext.Remove(atv);
            _bancoContext.SaveChanges();
            return atv;
        }

        public List<AtividadeModel> ListaAtividades(int idEmenda)
        {
            var atvs = _bancoContext.Atividades.Where(u => u.IdEmenda == idEmenda).ToList();
            return atvs;
        }
        public AtividadeModel BuscarAtividadeId(int idAtividade){
            var atv = _bancoContext.Atividades.FirstOrDefault(u => u.Id == idAtividade);
            return atv;
        }
    }
}