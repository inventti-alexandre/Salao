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

            modelBuilder.Entity<UsuarioGrupo>().HasKey(x => new { x.IdUsuario, x.IdGrupo });
            modelBuilder.Entity<GrupoPermissao>().HasKey(x => new { x.IdGrupo, x.IdPermissao });
        }

        // DbSets
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<GrupoPermissao> GrupoPermissao { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioGrupo> UsuarioGrupo { get; set; }
    }
}
