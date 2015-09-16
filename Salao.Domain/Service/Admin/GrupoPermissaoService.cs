using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class GrupoPermissaoService : IGrupoPermissao
    {
        private EFDbContext db = new EFDbContext();

        public IQueryable<Models.Admin.GrupoPermissao> Listar()
        {
            return db.GrupoPermissao;
        }

        public void Incluir(int idGrupo, int idPermissao)
        {
            // valida
            if (db.Grupo.ToList().Where(x => x.Id == idGrupo).Count() == 0)
            {
                throw new ArgumentException("Usuário inválido");
            }

            if (db.Permissao.ToList().Where(x => x.Id == idPermissao).Count() == 0)
            {
                throw new ArgumentException("Permissão inválida");
            }

            // gravar
            db.GrupoPermissao.Add(new GrupoPermissao { IdGrupo = idGrupo, IdPermissao = idPermissao });
            db.SaveChanges();
        }

        public void Excluir(int idGrupo, int idPermissao)
        {
            var grupoPermissao = db.GrupoPermissao.ToList().Where(x => x.IdGrupo == idGrupo && x.IdPermissao == idPermissao).FirstOrDefault();

            if (grupoPermissao != null)
            {
                db.GrupoPermissao.Remove(grupoPermissao);
                db.SaveChanges();
            }
        }

        public void Gravar(int idGrupo, int[] permissoes)
        {
            // remove permissoes do grupo
            var permissoesCadastradas = db.GrupoPermissao.ToList().Where(x => x.IdGrupo == idGrupo).ToList();
            if (permissoesCadastradas.Count() > 0)
            {
                db.GrupoPermissao.RemoveRange(permissoesCadastradas);
                db.SaveChanges();
            }

            // inclui novas permissoes
            if (permissoes.Count() > 0)
            {
                foreach (var item in permissoes)
                {
                    db.GrupoPermissao.Add(new GrupoPermissao { IdGrupo = idGrupo, IdPermissao = item });
                    db.SaveChanges();
                }
            }
        }
    }
}
