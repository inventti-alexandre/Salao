using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Abstract.Endereco
{
    public interface IEnderecoService<T>: IBaseService<T> where T:class
    {
        int GetId(string descricao, int idOrigem = 0);
    }
}
