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

    }
}
