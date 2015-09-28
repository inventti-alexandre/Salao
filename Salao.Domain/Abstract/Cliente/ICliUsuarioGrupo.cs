using Salao.Domain.Models.Cliente;
using System.Linq;

namespace Salao.Domain.Abstract.Cliente
{
    public interface ICliUsuarioGrupo
    {
        IQueryable<CliUsuarioGrupo> Listar();
        void Incluir(int idUsuario, int idGrupo);
        void Excluir(int idUsuario, int idGrupo);
        void Gravar(int idUsuario, int[] grupos);
    }
}
