using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class CliGrupoPermissaoService: IGrupoPermissao
    {
        private EFDbContext db;

        public CliGrupoPermissaoService()
        {
            db = new EFDbContext();
        }

        public IQueryable<CliGrupoPermissao> Listar()
        {
            return db.CliGrupoPermissao;
        }

        public void Incluir(int idGrupo, int idPermissao)
        {
            // valida
            if (db.CliGrupo.Find(idGrupo) == null)
            {
                throw new ArgumentException("Grupo inválido");
            }

            if (db.CliPermissao.Find(idPermissao) == null)
            {
                throw new ArgumentException("Permissão inválida");
            }

            // grava
            db.CliGrupoPermissao.Add(new CliGrupoPermissao { IdGrupo = idGrupo, IdPermissao = idPermissao });
            db.SaveChanges();
        }

        public void Excluir(int idGrupo, int idPermissao)
        {
            var grupoPermissao = db.CliGrupoPermissao.Where(x => x.IdGrupo == idGrupo && x.IdPermissao == idPermissao).FirstOrDefault();

            if (grupoPermissao != null)
            {
                db.CliGrupoPermissao.Remove(grupoPermissao);
                db.SaveChanges();
            }
        }

        public void Gravar(int idGrupo, int[] permissoes)
        {
            // remove permissoes do grupo
            var permissoesCadastradas = db.CliGrupoPermissao.Where(x => x.IdGrupo == idGrupo).ToList();
            if (permissoesCadastradas.Count > 0)
            {
                db.CliGrupoPermissao.RemoveRange(permissoesCadastradas);
                db.SaveChanges();
            }
            
            // inclui novas permissoes
            if (permissoes.Count() > 0)
            {
                foreach (var item in permissoes)
                {
                    db.CliGrupoPermissao.Add(new CliGrupoPermissao { IdGrupo = idGrupo, IdPermissao = item });
                    db.SaveChanges();
                }
            }
        }
    }
}
