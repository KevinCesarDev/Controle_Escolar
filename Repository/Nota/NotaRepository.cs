using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Data;
using Programa_DS2.Models;

namespace Programa.Repository.Nota
{
    public class NotaRepository : INotaRepository
    {
        public readonly DataContex _bancoContext;

        public NotaRepository(DataContex bancoContext)
        {
            _bancoContext = bancoContext;
        }

        public NotaModel EditarNota(NotaModel nota)
        {
          _bancoContext.Notas.Update(nota);
          _bancoContext.SaveChanges();
          return nota;
        }

        public List<NotaModel> NotasAluno(int idAluno)
        {
            var notas = _bancoContext.Notas.Where(u => u.IdAluno == idAluno).ToList();
            return notas;   
        }

        public List<NotaModel> NotasEmentas(int idEmenta)
        {
            var notas = _bancoContext.Notas.Where(u => u.IdEmenta == idEmenta).ToList();
            return notas;
        }

        public NotaModel SalvarNota(int idAluno, int idEmenta, decimal nota)
        {   
            NotaModel novaNota = new NotaModel();
            novaNota.IdAluno = idAluno;
            novaNota.IdEmenta = idEmenta;
            novaNota.Nota = nota;
            _bancoContext.Notas.Add(novaNota);
            _bancoContext.SaveChanges();
            return novaNota;
        }
    }
}