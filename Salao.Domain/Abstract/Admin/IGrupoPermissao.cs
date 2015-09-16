using Salao.Domain.Models.Admin;
using System.Linq;

namespace Salao.Domain.Abstract.Admin
{
    public interface IGrupoPermissao
    {
        IQueryable<GrupoPermissao> Listar();
        void Incluir(int idGrupo, int idPermissao);
        void Excluir(int idGrupo, int idPermissao);
        void Gravar(int idGrupo, int[] permissoes);
    }
}
