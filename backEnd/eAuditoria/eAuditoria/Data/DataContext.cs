using eAuditoria.Data.Repository.ModelEntity;
using Microsoft.EntityFrameworkCore;

namespace eAuditoria.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Locacao> Locacao { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Filme> Filme { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=localhost; database=eAuditoria; uid=thiago; pwd=palmeiras51; port=3306");
            //optionsBuilder.UseSqlServer("Integrated Security=SSPI;Persist Security Info=False;User ID=thiago;Password=palmeiras51;Initial Catalog=eAuditoria;Data Source=127.0.0.1/3306");
        }
    }
}
