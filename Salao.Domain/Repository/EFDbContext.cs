using Salao.Domain.Models.Admin;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Salao.Domain.Repository
{
    public class EFDbContext: DbContext
    {
        public EFDbContext()
            : base("SalaoConn")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        // DbSets
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
    }
}
