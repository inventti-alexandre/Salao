using Salao.Domain.Models.Cliente;
using System.Linq;

namespace Salao.Domain.Abstract.Cliente
{
    public interface IGrupoPermissao
    {
        IQueryable<CliGrupoPermissao> Listar();
        void Incluir(int idGrupo, int idPermissao);
        void Excluir(int idGrupo, int idPermissao);
        void Gravar(int idGrupo, int[] permissoes);
    }
}
