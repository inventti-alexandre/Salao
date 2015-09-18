using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class PermissaoService: IBaseService<Permissao>
    {
        private IBaseRepository<Permissao> repository;

        public PermissaoService()
        {
            repository = new EFRepository<Permissao>();
        }

        public IQueryable<Permissao> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Permissao item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count()> 0)
            {
                throw new ArgumentException("Já existe uma permissão cadastrada com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Permissao Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var permissao = repository.Find(id);

                if (permissao != null)
                {
                    permissao.AlteradoEm = DateTime.Now;
                    permissao.Ativo = false;
                    return repository.Alterar(permissao);
                }
                return permissao;
            }
        }

        public Permissao Find(int id)
        {
            return repository.Find(id);
        }
    }
}
