using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class ProfissionalServicoService : IProfissionalServico
    {
        private EFDbContext db = new EFDbContext();

        public IQueryable<ProfissionalServico> Listar()
        {
            return db.ProfissionalServico;
        }

        public void Excluir(int idProfissional, int idServico)
        {
            var prof = db.ProfissionalServico.Where(x => x.IdProfissional == idProfissional && x.IdServico == idServico).FirstOrDefault();

            if (prof != null)
            {
                db.ProfissionalServico.Remove(prof);
                db.SaveChanges();
            }
        }

        public void Gravar(int idProfissional, int[] idServico)
        {
            // remove todos os servicos detes profissional
            var servicos = db.ProfissionalServico.Where(x => x.IdProfissional == idProfissional).ToList();

            if (servicos.Count > 0)
            {
                db.ProfissionalServico.RemoveRange(servicos);
                db.SaveChanges();
            }

            // inclui novos servicos para este profissional
            if (idServico != null)
            {
                foreach (var item in idServico)
                {
                    db.ProfissionalServico.Add(new ProfissionalServico { IdProfissional = idProfissional, IdServico = item });
                    db.SaveChanges();
                }
            }
        }
    }
}
