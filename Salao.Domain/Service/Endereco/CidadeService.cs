using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Endereco;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class CidadeService: IEnderecoService<EnderecoCidade>
    {
        private IBaseRepository<EnderecoCidade> repository;

        public CidadeService()
        {
            repository = new EFRepository<EnderecoCidade>();
        }

        public IQueryable<EnderecoCidade> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(EnderecoCidade item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            
            // valida
            if (item.IdEstado == 0)
            {
                throw new ArgumentException("Selecione o Estado");
            }
            
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.IdEstado == item.IdEstado && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma cidade cadastrado com este nome neste Estado");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public EnderecoCidade Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var cidade = repository.Find(id);

                if (cidade != null)
                {
                    cidade.Ativo = false;
                    return repository.Alterar(cidade);
                }

                return cidade;
            }
        }

        public EnderecoCidade Find(int id)
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
            var cidade = repository.Listar().Where(x => x.Descricao == descricao && x.IdEstado == idOrigem).FirstOrDefault();
            if (cidade == null)
            {
                // inclui nova cidade
                return Gravar(new EnderecoCidade { Descricao = descricao, IdEstado = idOrigem });
            }

            return cidade.Id;
        }
    }
}
