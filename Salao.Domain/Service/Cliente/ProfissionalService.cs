using Salao.Domain.Abstract;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Service.Cliente
{
    public class ProfissionalService: IBaseService<Profissional>
    {
        private IBaseRepository<Profissional> repository;

        public ProfissionalService()
        {
            repository = new EFRepository<Profissional>();
        }

        public IQueryable<Profissional> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Profissional item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Email = item.Email.ToLower().Trim();
            item.Nome = item.Nome.ToUpper().Trim();
            item.Telefone = item.Telefone.ToUpper().Trim();

            // valida
            if (item.IdSalao <= 0)
            {
                throw new ArgumentException("Salão inválido");
            }

            if (repository.Listar().Where(x => x.Nome == item.Nome && x.IdSalao == item.IdSalao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um colaborador cadastrado com este nome neste salão");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Profissional Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var profissional = repository.Find(id);
                if (profissional != null)
                {
                    profissional.Ativo = false;
                    profissional.AlteradoEm = DateTime.Now;
                    return repository.Alterar(profissional);
                }
                return profissional;
            }
        }

        public Profissional Find(int id)
        {
            return repository.Find(id);
        }
    }
}
