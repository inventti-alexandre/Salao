using Salao.Domain.Models.Cliente;

namespace Salao.Domain.Abstract.Cliente
{
    public interface ICadastroSalao
    {
        int Gravar(CadastroSalao cadastro);
        Salao.Domain.Models.Cliente.Salao Excluir(int id);
        CadastroSalao Find(int id);
    }
}
