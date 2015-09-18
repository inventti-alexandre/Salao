using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class SubAreaService: IBaseService<SubArea>
    {
        private IBaseRepository<SubArea> repository;

        public SubAreaService()
        {
            repository = new EFRepository<SubArea>();
        }

        public IQueryable<SubArea> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(SubArea item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma sub área deste serviço cadastrada");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public SubArea Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var subArea = repository.Find(id);
                if (subArea != null)
                {
                    subArea.Ativo = false;
                    subArea.AlteradoEm = DateTime.Now;
                    return repository.Alterar(subArea);
                }
                return subArea;
                throw;
            }
        }

        public SubArea Find(int id)
        {
            return repository.Find(id);
        }
    }
}
