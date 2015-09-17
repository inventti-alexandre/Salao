using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Domain.Service.Admin
{
    [Authorize]
    public class UsuarioGrupoService: IUsuarioGrupo
    {
        private EFDbContext db = new EFDbContext();

        public IQueryable<Models.Admin.UsuarioGrupo> Listar()
        {
            return db.UsuarioGrupo;
        }

        public void Incluir(int idUsuario, int idGrupo)
        {
            // valida
            if (db.Usuario.Where(x => x.Id == idUsuario).Count() == 0)
            {
                throw new ArgumentException("Usuário inválido");
            }

            if (db.Grupo.Where(x => x.Id == idGrupo).Count() == 0)
            {
                throw new ArgumentException("Grupo inválido");
            }

            // grava
            db.UsuarioGrupo.Add(new UsuarioGrupo { IdGrupo = idGrupo, IdUsuario = idUsuario });
            db.SaveChanges();
        }

        public void Excluir(int idUsuario, int idGrupo)
        {
            var usuarioGrupo = db.UsuarioGrupo.Where(x => x.IdUsuario == idUsuario && x.IdGrupo == idGrupo).FirstOrDefault();

            if (usuarioGrupo != null)
            {
                db.UsuarioGrupo.Remove(usuarioGrupo);
                db.SaveChanges();
            }
        }

        public void Gravar(int idUsuario, int[] grupos)
        {
            // remove todos os grupos do usuario
            var gruposCadastrados = db.UsuarioGrupo.Where(x => x.IdUsuario == idUsuario).ToList();
            if (gruposCadastrados.Count() > 0)
            {
                db.UsuarioGrupo.RemoveRange(gruposCadastrados);
                db.SaveChanges();
            }

            // inclui novos grupos
            if (grupos != null)
            {
                foreach (var item in grupos)
                {
                    db.UsuarioGrupo.Add(new UsuarioGrupo { IdUsuario = idUsuario, IdGrupo = item });
                    db.SaveChanges();
                }
            }
        }
    }
}
