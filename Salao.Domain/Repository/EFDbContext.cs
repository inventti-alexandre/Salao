using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Models.Cliente;
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

        // DbSets - admin - endereco
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<EnderecoBairro> EnderecoBairro { get; set; }
        public DbSet<EnderecoCidade> EnderecoCidade { get; set; }
        public DbSet<EnderecoEmail> EnderecoEmail { get; set; }
        public DbSet<EnderecoEstado> EnderecoEstado { get; set; }
        public DbSet<EnderecoTelefone> EnderecoTelefone { get; set; }
        public DbSet<EnderecoTipoEndereco> EnderecoTipoEndereco { get; set; }

        // DbSets - admin - outros
        public DbSet<Area> Area { get; set; }
        public DbSet<FormaPgto> FormaPgto { get; set; }
        public DbSet<Grupo> Grupo { get; set; }
        public DbSet<GrupoPermissao> GrupoPermissao { get; set; }
        public DbSet<Permissao> Permissao { get; set; }
        public DbSet<PreContato> PreContato { get; set; }
        public DbSet<SistemaParametro> SistemaParametro { get; set; }
        public DbSet<SubArea> SubArea { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<UsuarioGrupo> UsuarioGrupo { get; set; }

        // DbSets - cliente
        public DbSet<Empresa> Empresa { get; set; }
    }
}
