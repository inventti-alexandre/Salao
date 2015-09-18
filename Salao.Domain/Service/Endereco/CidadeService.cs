using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class CidadeService: IBaseService<EnderecoCidade>
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
    }
}
