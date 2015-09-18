using Salao.Domain.Models.Cliente;

namespace Salao.Domain.Abstract.Admin
{
    public interface ICadastroEmpresa
    {
        int ChecarCadastroAnterior(string documento);
        int Cadastrar(CadastroEmpresa cadastro);
    }
}
