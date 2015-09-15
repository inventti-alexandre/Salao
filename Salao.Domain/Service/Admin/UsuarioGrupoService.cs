using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
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
    }
}
