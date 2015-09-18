using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class AreaService: IBaseService<Area>
    {
        private IBaseRepository<Area> repository;

        public AreaService()
        {
            repository = new EFRepository<Area>();
        }

        public IQueryable<Area> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Area item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Área de serviço já cadastrada");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public Area Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var area = repository.Find(id);
                if (area != null)
                {
                    area.Ativo = false;
                    area.AlteradoEm = DateTime.Now;
                    return repository.Alterar(area);
                }
                return area;
            }
        }

        public Area Find(int id)
        {
            return repository.Find(id);
        }
    }
}
