using DiarioDeClasse.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiarioDeClasse.Infra
{
    public class UsuarioDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<User> Usuarios { get; set; }

        public UsuarioDbContext(IConfiguration configuration, DbContextOptions<UsuarioDbContext> options) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = configuration.GetConnectionString("SqlServer");
            optionsBuilder.UseSqlServer(conn);
        }
    }
}
