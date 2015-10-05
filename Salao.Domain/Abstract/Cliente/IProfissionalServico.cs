using Salao.Domain.Models.Cliente;
using System.Linq;

namespace Salao.Domain.Abstract.Cliente
{
    public interface IProfissionalServico
    {
        IQueryable<ProfissionalServico> Listar();
        void Excluir(int idProfissional, int idServico);
        void Gravar(int idProfissional, int[] idServico);
    }
}
