using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Repository;
using System.Linq;

namespace Salao.Web.Areas.Cliente.Controllers
{
    public class SalaoFormaPgtoService: ISalaoFormaPgto
    {
        private EFDbContext db = new EFDbContext();

        public IQueryable<Domain.Models.Cliente.SalaoFormaPgto> Listar()
        {
            return db.SalaoFormaPgto;
        }

        public void Excluir(int idSalao, int idFormaPgto)
        {
            var formas = db.SalaoFormaPgto.Where(x => x.IdSalao == idSalao && x.IdFormaPgto == idFormaPgto).ToList();

            if (formas != null)
            {
                db.SalaoFormaPgto.RemoveRange(formas);
                db.SaveChanges();
            }
        }

        public void Gravar(int idSalao, int[] idFormaPgto)
        {
            // remove formas cadastradas neste salao
            var formasCadastradas = db.SalaoFormaPgto.Where(x => x.IdSalao == idSalao).ToList();

            if (formasCadastradas.Count > 0)
            {
                db.SalaoFormaPgto.RemoveRange(formasCadastradas);
                db.SaveChanges();
            }

            // inclui novas formas
            if (idFormaPgto != null)
            {
                foreach (var item in idFormaPgto)
                {
                    db.SalaoFormaPgto.Add(new Domain.Models.Cliente.SalaoFormaPgto { IdSalao = idSalao, IdFormaPgto = item, Ativo = true });
                    db.SaveChanges();
                }
            }
        }
    }
}
