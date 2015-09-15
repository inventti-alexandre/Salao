using Salao.Domain.Models.Admin;
using System.Linq;

namespace Salao.Domain.Abstract.Admin
{
    public interface IUsuarioGrupo
    {
        IQueryable<UsuarioGrupo> Listar();
        void Incluir(int idUsuario, int idGrupo);
        void Excluir(int idUsuario, int idGrupo);
    }
}
