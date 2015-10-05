using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Repository;
using System.Linq;
using System;

namespace Salao.Domain.Service.Cliente
{
    public class SalaoFormaPgtoService: ISalaoFormaPgto
    {
        private EFDbContext db = new EFDbContext();

        public IQueryable<Models.Cliente.SalaoFormaPgto> Listar()
        {
            return db.SalaoFormaPgto;
        }

        public void Excluir(int idSalao, int idFormaPgto)
        {
            var forma = db.SalaoFormaPgto.Where(x => x.IdSalao == idSalao && x.IdFormaPgto == idFormaPgto).FirstOrDefault();

            if (forma != null)
            {
                db.SalaoFormaPgto.Remove(forma);
                db.SaveChanges();
            }
        }

        public void Gravar(int idSalao, int[] idFormaPgto)
        {
            // remove todas as formas de pagamento deste salao
            var formas = db.SalaoFormaPgto.Where(x => x.IdSalao == idSalao).ToList();

            if (formas.Count > 0)
            {
                db.SalaoFormaPgto.RemoveRange(formas);
                db.SaveChanges();
            }

            // inclui novas forma sde pagamento para este salao
            if (idFormaPgto != null)
            {
                foreach (var item in idFormaPgto)
                {
                    db.SalaoFormaPgto.Add(new Models.Cliente.SalaoFormaPgto { IdSalao = idSalao, IdFormaPgto = item, Ativo = true, AlteradoEm = DateTime.Now });
                    db.SaveChanges();
                }
            }
        }
    }
}
