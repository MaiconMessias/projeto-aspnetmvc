using Microsoft.EntityFrameworkCore;

namespace primeira_projeto_aspnetmvc.Models
{
    public class Context : DbContext {

        public DbSet<Pessoa> Pessoas { get;set; }
        public DbSet<Endereco> Enderecos { get;set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=agenda;User Id=postgres;Password=n");
        }

    }
}