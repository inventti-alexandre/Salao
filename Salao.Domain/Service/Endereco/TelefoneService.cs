using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class TelefoneService: IBaseService<EnderecoTelefone>
    {
        private IBaseRepository<EnderecoTelefone> repository;

        public TelefoneService()
        {
            repository = new EFRepository<EnderecoTelefone>();
        }

        public IQueryable<EnderecoTelefone> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(EnderecoTelefone item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Contato = item.Contato.ToUpper().Trim();
            item.DDD = item.DDD.ToUpper().Trim();
            item.Ramal = item.Ramal.ToUpper().Trim();
            item.Telefone = item.Telefone.ToUpper().Trim();
            
            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public EnderecoTelefone Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public EnderecoTelefone Find(int id)
        {
            return repository.Find(id);
        }
    }
}
