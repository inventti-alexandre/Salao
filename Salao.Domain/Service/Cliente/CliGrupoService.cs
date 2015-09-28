using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Abstract;
using Salao.Domain.Repository;

namespace Salao.Domain.Service.Cliente
{
    public class CliGrupoService: IBaseService<CliGrupo>
    {
        private IBaseRepository<CliGrupo> repository;

        public CliGrupoService()
        {
            repository = new EFRepository<CliGrupo>();
        }

        public IQueryable<CliGrupo> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(CliGrupo item)
        {
            // formata
            item.AlteradoEm = DateTime.Now;
            item.Descricao = item.Descricao.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Descricao == item.Descricao && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um grupo cadastrado com este nome");
            }

            // gravar
            if (item.Id == 0)
            {
                item.Ativo = true;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public CliGrupo Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var grupo = repository.Find(id);
                if (grupo != null)
                {
                    grupo.Ativo = false;
                    grupo.AlteradoEm = DateTime.Now;
                    return repository.Alterar(grupo);
                }
                return grupo;
            }
        }

        public CliGrupo Find(int id)
        {
            return repository.Find(id);
        }
    }
}
