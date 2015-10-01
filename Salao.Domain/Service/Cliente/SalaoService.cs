using Salao.Domain.Abstract;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class SalaoService : IBaseService<Models.Cliente.Salao>
    {
        private IBaseRepository<Models.Cliente.Salao> repository;

        public SalaoService()
        {
            repository = new EFRepository<Models.Cliente.Salao>();
        }

        public IQueryable<Models.Cliente.Salao> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Models.Cliente.Salao item)
        {
            // formata
            item.Fantasia = item.Fantasia.ToUpper().Trim();
            item.Contato = item.Contato.ToUpper().Trim();
            item.Observ = item.Observ.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (item.TipoPessoa == 2)
            {
                if (string.IsNullOrEmpty(item.Cnpj))
                {
                    throw new ArgumentException("Informe o CNPJ");
                }
            }
            else if (item.TipoPessoa == 1)
            {
                if (string.IsNullOrEmpty(item.Cpf))
                {
                    throw new ArgumentException("CPF inválido");
                }
            }

            if (item.Desconto > 0 && item.DescontoCarencia == 0)
            {
                throw new ArgumentException("Informe o número de meses de carência para desconto");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                item.CadastradoEm = DateTime.Now;
                item.Exibir = false;
                item.Aprovado = false;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Models.Cliente.Salao Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var salao = repository.Find(id);
                if (salao != null)
                {
                    salao.Ativo = false;
                    salao.AlteradoEm = DateTime.Now;
                    return repository.Alterar(salao);
                }
                return salao;
            }
        }

        public Models.Cliente.Salao Find(int id)
        {
            return repository.Find(id);
        }
    }
}
