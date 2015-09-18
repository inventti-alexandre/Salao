using Salao.Domain.Abstract;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class EnderecoService: IBaseService<Salao.Domain.Models.Endereco.Endereco>
    {
        private IBaseRepository<Salao.Domain.Models.Endereco.Endereco> repository;

        public EnderecoService()
        {
            repository = new EFRepository<Salao.Domain.Models.Endereco.Endereco>();    
        }

        public IQueryable<Models.Endereco.Endereco> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Models.Endereco.Endereco item)
        {
            // formata
            item.Complemento = item.Complemento.ToUpper().Trim();
            item.Numero = item.Numero.ToUpper().Trim();
            item.Logradouro = item.Logradouro.ToUpper().Trim();

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Models.Endereco.Endereco Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var endereco = repository.Find(id);

                if (endereco != null)
                {
                    endereco.Ativo = false;
                    return repository.Alterar(endereco);
                }
                return endereco;
            }
        }

        public Models.Endereco.Endereco Find(int id)
        {
            return repository.Find(id);
        }
    }
}
