using Salao.Domain.Models.Cliente;

namespace Salao.Domain.Abstract.Admin
{
    public interface ICadastroEmpresa
    {
        int ChecarCadastroAnterior(string documento);
        int Incluir(CadastroEmpresa cadastro);
        int Alterar(CadastroEmpresa cadastro);
        Empresa Excluir(int id);
        CadastroEmpresa Find(int id);
    }
}
