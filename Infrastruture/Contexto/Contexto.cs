using MeusGastos.Domain.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeusGastos.Infrastructure.Contexto
{
    public class Contexto : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<Financas>()
                .HasMany(financa => financa.Ganhos)
                .WithOne(ganhos => ganhos.Financas)
                .HasForeignKey(ganhos => ganhos.FinancasId);

            builder.Entity<Financas>()
                .HasMany(financa => financa.Gastos)
                .WithOne(gastos => gastos.Financas)
                .HasForeignKey(gastos => gastos.FinancasId);
        }

        public DbSet<Financas> Financas { get; set; }
        public DbSet<Ganhos> Ganhos { get; set; }
        public DbSet<Gastos> Gastos { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
