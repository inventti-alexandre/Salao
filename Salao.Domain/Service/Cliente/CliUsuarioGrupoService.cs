using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class CliUsuarioGrupoService : ICliUsuarioGrupo
    {
        private EFDbContext db = new EFDbContext();

        public IQueryable<CliUsuarioGrupo> Listar()
        {
            return db.CliUsuarioGrupo;
        }

        public void Incluir(int idUsuario, int idGrupo)
        {
            // valida
            if (db.CliUsuario.Find(idUsuario) == null)
            {
                throw new ArgumentException("Usuário inválido");
            }

            if (db.CliGrupo.Find(idGrupo) == null)
            {
                throw new ArgumentException("Grupo inválido");
            }

            // grava
            db.CliUsuarioGrupo.Add(new CliUsuarioGrupo { IdGrupo = idGrupo, IdUsuario = idUsuario });
            db.SaveChanges();
        }

        public void Excluir(int idUsuario, int idGrupo)
        {
            var usuarioGrupo = db.CliUsuarioGrupo.Where(x => x.IdUsuario == idUsuario && x.IdGrupo == idGrupo).FirstOrDefault();

            if (usuarioGrupo != null)
            {
                db.CliUsuarioGrupo.Remove(usuarioGrupo);
                db.SaveChanges();
            }
        }

        public void Gravar(int idUsuario, int[] grupos)
        {
            // remove todos os grupos do usuario
            var gruposCadastrados = db.CliUsuarioGrupo.Where(x => x.IdUsuario == idUsuario).ToList();
            if (gruposCadastrados.Count > 0)
            {
                db.CliUsuarioGrupo.RemoveRange(gruposCadastrados);
                db.SaveChanges();
            }

            // inclui novos grupos
            if (grupos != null)
            {
                foreach (var item in grupos)
                {
                    db.CliUsuarioGrupo.Add(new CliUsuarioGrupo { IdUsuario = idUsuario, IdGrupo = item });
                    db.SaveChanges();
                }
            }
        }
    }
}