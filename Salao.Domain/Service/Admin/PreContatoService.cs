using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class PreContatoService: IBaseService<PreContato>
    {
        private IBaseRepository<PreContato> repository;

        public PreContatoService()
        {
            repository = new EFRepository<PreContato>();
        }

        public IQueryable<PreContato> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(PreContato item)
        {
            // formata
            item.Cidade = item.Cidade.ToUpper().Trim();
            item.ContatoEm = DateTime.Now;
            item.Email = item.Email.ToLower().Trim();
            item.Nome = item.Nome.ToUpper().Trim();
            item.NomeSalao = item.NomeSalao.ToUpper().Trim();
            item.Telefone = item.Telefone.ToUpper().Trim();
            if (string.IsNullOrEmpty(item.Observ))
            {
                item.Observ = "";
            }
            else
            {
                item.Observ = item.Observ.ToUpper().Trim();
            }

            // valida

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }
            return repository.Alterar(item).Id;
        }

        public PreContato Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                return repository.Find(id);
            }
        }

        public PreContato Find(int id)
        {
            return repository.Find(id);
        }
    }
}
