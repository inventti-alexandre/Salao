using Salao.Domain.Models.Cliente;
using System.Linq;

namespace Salao.Domain.Abstract.Cliente
{
    public interface ISalaoFormaPgto
    {
        IQueryable<SalaoFormaPgto> Listar();
        void Excluir(int idSalao, int idFormaPgto);
        void Gravar(int idSalao, int[] idFormaPgto);
    }
}
