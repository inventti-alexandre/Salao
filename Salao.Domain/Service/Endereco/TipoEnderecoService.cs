using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class TipoEnderecoService: IBaseService<EnderecoTipoEndereco>
    {
        private IBaseRepository<EnderecoTipoEndereco> repository;

        public TipoEnderecoService()
        {
            repository = new EFRepository<EnderecoTipoEndereco>();
        }

        public IQueryable<EnderecoTipoEndereco> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(EnderecoTipoEndereco item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();            

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um tipo de endereço cadastrado com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public EnderecoTipoEndereco Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var tipo = repository.Find(id);

                if (tipo != null)
                {
                    tipo.Ativo = false;
                    return repository.Alterar(tipo);
                }

                return tipo;
            }
        }

        public EnderecoTipoEndereco Find(int id)
        {
            return repository.Find(id);
        }
    }
}
