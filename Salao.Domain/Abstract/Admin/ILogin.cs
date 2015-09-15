using Salao.Domain.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Abstract.Admin
{
    public interface ILogin
    {
        Usuario ValidaLogin(string login, string senha);
        int GetIdUsuario(string login);
    }
}
