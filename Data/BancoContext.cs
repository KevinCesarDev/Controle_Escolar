using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controle_Escolar.Models;
using Microsoft.EntityFrameworkCore;

namespace Controle_Escolar.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options) { }

        public DbSet<AtividadeModel> Atividades { get; set; }
        public DbSet<ProfModel> Professores { get; set; }
        public DbSet<TurmaModel> Turmas { get; set; }
    }
}