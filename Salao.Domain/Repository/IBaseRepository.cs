using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Repository
{
    public interface IBaseRepository<T> where T: class
    {
        IQueryable<T> Listar();
        T Incluir(T item);
        T Alterar(T item);
        T Excluir(int id);
        T Find(int id);
    }
}
