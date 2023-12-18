using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Programa.Models;
using Programa_DS2.Models;

namespace Programa.Data
{
    public class DataContex : DbContext
    {
        public DataContex(DbContextOptions<DataContex> options) : base(options){}

        public DbSet<AtividadeModel> Atividades {get;set;} 
        public DbSet<ContaModel> Contas {get;set;}
        public DbSet<UsuarioModel> Usuarios {get;set;}

        public DbSet<TurmaModel> Turmas {get;set;}
        public DbSet<NotaModel> Notas {get;set;}
        public DbSet<DisciplinaModel> Disciplinas{get;set;}
        public DbSet<EmentaModel> Ementas{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<ContaModel>().HasData( new ContaModel {Id = 1,TipoConta = "Coordenador"},new ContaModel {Id = 2,TipoConta = "Professor"},new ContaModel{Id = 3,TipoConta="Aluno"});
        modelBuilder.Entity<UsuarioModel>().HasData( new UsuarioModel {Id = 1,Login = "admin",Senha = "admin",Nome = "admin", IdConta = 1});
    }

    }
}