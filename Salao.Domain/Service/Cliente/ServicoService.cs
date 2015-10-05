using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class ServicoService: IBaseService<Servico>
    {
        private IBaseRepository<Servico> repository;

        public ServicoService()
        {
            repository = new EFRepository<Servico>();
        }

        public IQueryable<Servico> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Servico item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.IdSalao == item.IdSalao && x.IdSubArea == item.IdSubArea && x.Descricao == item.Descricao && x.Id != item.Id).Count()> 0)
            {
                throw new ArgumentException("Já existe um serviço cadastrado com esta descrição");
            }
                
            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Servico Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var servico = repository.Find(id);
                if (servico != null)
                {
                    servico.AlteradoEm = DateTime.Now;
                    servico.Ativo = false;
                    return repository.Alterar(servico);
                }
                return servico;
            }
        }

        public Servico Find(int id)
        {
            return repository.Find(id);
        }
    }
}
