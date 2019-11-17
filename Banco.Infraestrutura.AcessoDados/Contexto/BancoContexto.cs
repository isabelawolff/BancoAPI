using Banco.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banco.Infraestrutura.AcessoDados.Contexto
{
    public class BancoContexto : DbContext
    {
        public DbSet<Cotista> Cotistas { get; set; }
        public DbSet<Fundo> Fundos { get; set; }
        public DbSet<ContaCotista> ContasCotistas { get; set; }
        public DbSet<OrdemAplicacao> OrdensAplicacao { get; set; }
        public DbSet<OrdemResgate> OrdensResgate { get; set; }

        public BancoContexto()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=tcp:isabelaserver.database.windows.net,1433;Initial Catalog=BancoDB;Persist Security Info=False;User ID=usuarioisabela;Password=jkuik*123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

    }
}
