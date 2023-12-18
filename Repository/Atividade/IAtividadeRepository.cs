using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Programa.Models;

namespace Programa.Repository.Atividade
{
    public interface IAtividadeRepository
    {
        AtividadeModel CadastrarAtv(AtividadeModel novaAtv);
        List<AtividadeModel> ListaAtividades(int idTurma);

        AtividadeModel ExcluirAtividade(int idAtividade);

        AtividadeModel BuscarAtividadeId(int idAtividade);
    }
}