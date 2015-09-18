using Salao.Domain.Abstract;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Endereco
{
    public class EmailService: IBaseService<EnderecoEmail>
    {
        private IBaseRepository<EnderecoEmail> repository;

        public EmailService()
        {
            repository = new EFRepository<EnderecoEmail>();
        }

        public IQueryable<EnderecoEmail> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(EnderecoEmail item)
        {
            // formata
            item.Email = item.Email.ToLower().Trim();

            // valida
            if (repository.Listar().Where(x => x.Email == item.Email && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("E-mail já cadastrado");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public EnderecoEmail Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var email = repository.Find(id);

                if (email != null)
                {
                    email.Ativo = false;
                    return repository.Alterar(email);
                }

                return email;
                    
            }
        }

        public EnderecoEmail Find(int id)
        {
            return repository.Find(id);
        }
    }
}
