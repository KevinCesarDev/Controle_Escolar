using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa_DS2.Models;

namespace Programa.Repository.Nota
{
    public interface INotaRepository
    {
        //NotaModel BuscarNota();
        NotaModel SalvarNota(int idAluno,int idEmenta, decimal nota);
        NotaModel EditarNota(NotaModel nota);
        List<NotaModel> NotasAluno(int idAluno);
        List<NotaModel> NotasEmentas(int idEmenta);
    }
}