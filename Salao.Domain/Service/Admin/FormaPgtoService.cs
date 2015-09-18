using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class FormaPgtoService: IBaseService<FormaPgto>
    {
        private IBaseRepository<FormaPgto> repository;

        public FormaPgtoService()
        {
            repository = new EFRepository<FormaPgto>();
        }

        public IQueryable<FormaPgto> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(FormaPgto item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe uma forma de pagamento cadastrada com esta descrição");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public FormaPgto Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var forma = repository.Find(id);
                if (forma != null)
                {
                    forma.Ativo = false;
                    forma.AlteradoEm = DateTime.Now;
                    return repository.Alterar(forma);
                }
                return forma;
            }
        }

        public FormaPgto Find(int id)
        {
            return repository.Find(id);
        }
    }
}
