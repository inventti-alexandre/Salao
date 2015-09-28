using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;

namespace Salao.Domain.Service.Cliente
{
    public class CliPermissaoService: IBaseService<CliPermissao>
    {
        IBaseRepository<CliPermissao> repository;

        public CliPermissaoService()
        {
            repository = new EFRepository<CliPermissao>();
        }

        public IQueryable<CliPermissao> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(CliPermissao item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
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

        public CliPermissao Excluir(int id)
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

        public CliPermissao Find(int id)
        {
            return repository.Find(id);
        }
    }
}
