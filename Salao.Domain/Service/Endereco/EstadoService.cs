using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class EstadoService: IBaseService<EnderecoEstado>
    {
        private IBaseRepository<EnderecoEstado> repository;

        public EstadoService()
        {
            repository = new EFRepository<EnderecoEstado>();
        }

        public IQueryable<EnderecoEstado> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(EnderecoEstado item)
        {
            // formata
            item.Descricao = item.Descricao.ToUpper().Trim();
            item.UF = item.UF.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (repository.Listar().Where(x => x.UF == item.UF && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um Estado cadastrado com esta UF");
            }

            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um Estado cadastrado com este nome");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public EnderecoEstado Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var estado = repository.Find(id);

                if (estado != null)
                {
                    estado.AlteradoEm = DateTime.Now;
                    estado.Ativo = false;
                    return repository.Alterar(estado);
                }

                return estado;
            }
        }

        public EnderecoEstado Find(int id)
        {
            return repository.Find(id);
        }
    }
}
