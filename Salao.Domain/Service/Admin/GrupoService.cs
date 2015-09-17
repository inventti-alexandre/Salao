using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Domain.Service.Admin
{
    [Authorize]
    public class GrupoService: IBaseService<Grupo>
    {
        private IBaseRepository<Grupo> repository;

        public GrupoService()
        {
            repository = new EFRepository<Grupo>();
        }

        public IQueryable<Grupo> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Grupo item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo cadastrado com este nome");
            }

            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;

        }

        public Grupo Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var grupo = repository.Find(id);

                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(grupo);
                }
                
                return grupo;
            }
        }

        public Grupo Find(int id)
        {
            return repository.Find(id);
        }
    }
}
