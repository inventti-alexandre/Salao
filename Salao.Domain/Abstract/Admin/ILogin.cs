using Salao.Domain.Models.Admin;

namespace Salao.Domain.Abstract.Admin
{
    public interface ILogin
    {
        Usuario ValidaLogin(string login, string senha);
        int GetIdUsuario(string login);
    }
}
