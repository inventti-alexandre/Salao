using Salao.Domain.Models.Cliente;

namespace Salao.Domain.Abstract.Cliente
{
    public interface ILogin
    {
        CliUsuario ValidaLogin(string email, string senha);
        int GetIdCliUsuario(string email);
        int GetIdUsuarioByNome(string nome, int idEmpresa);
    }
}
