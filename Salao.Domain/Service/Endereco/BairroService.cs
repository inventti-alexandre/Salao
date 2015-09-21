using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Endereco;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class BairroService: IEnderecoService<EnderecoBairro>
    {
        private IBaseRepository<EnderecoBairro> repository;

        public BairroService()
        {
            repository = new EFRepository<EnderecoBairro>();
        }

        public IQueryable<EnderecoBairro> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(EnderecoBairro item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdCidade == item.IdCidade && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um bairro cadastrado com este nome nesta cidade");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public EnderecoBairro Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var bairro = repository.Find(id);

                if (bairro != null)
                {
                    bairro.Ativo = false;
                    return repository.Alterar(bairro);
                }

                return bairro;
            }
        }

        public EnderecoBairro Find(int id)
        {
            return repository.Find(id);
        }

        public int GetId(string descricao, int idOrigem = 0)
        {
            if (string.IsNullOrEmpty(descricao))
            {
                return 0;
            }

            descricao = descricao.ToUpper().Trim();
            var bairro = repository.Listar().Where(x => x.Descricao == descricao && x.IdCidade == idOrigem).FirstOrDefault();
            if (bairro == null)
            {
                return Gravar(new EnderecoBairro { Descricao = descricao, IdCidade = idOrigem });
            }
            return bairro.Id;
        }
    }
}
