using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Salao.Domain.Service.Endereco
{
    [Authorize]
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
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var telefone = repository.Find(id);

                if (telefone != null)
                {
                    telefone.Ativo = false;
                    return repository.Alterar(telefone);
                }
                return telefone;
            }
        }

        public EnderecoTelefone Find(int id)
        {
            return repository.Find(id);
        }
    }
}
