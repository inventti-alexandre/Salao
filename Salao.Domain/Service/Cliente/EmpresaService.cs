using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class EmpresaService: IBaseService<Empresa>
    {
        private IBaseRepository<Empresa> repository;

        public EmpresaService()
        {
            repository = new EFRepository<Empresa>();
        }

        public IQueryable<Empresa> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Empresa item)
        {
            // formata
            item.Fantasia = item.Fantasia.ToUpper().Trim();
            item.RazaoSocial = item.RazaoSocial.ToUpper().Trim();
            item.Contato = item.Contato.ToUpper().Trim();
            item.Observ = item.Observ.ToUpper().Trim();
            item.AlteradoEm = DateTime.Now;

            // valida
            if (item.TipoPessoa == 2 && string.IsNullOrEmpty(item.Cnpj))
            {
                throw new ArgumentException("Informe o CNPJ");
            }
            else
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
                item.PrecoAvaliacao = 0;
                item.ClienteAvaliacao = 0;
                item.Aprovado = false;
                item.Exibir = false;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Empresa Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var empresa = repository.Find(id);
                if (empresa != null)
                {
                    empresa.AlteradoEm = DateTime.Now;
                    empresa.Ativo = false;
                    return repository.Alterar(empresa);
                }
                return empresa;
            }
        }

        public Empresa Find(int id)
        {
            return repository.Find(id);
        }
    }
}
