using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class CliUsuarioService: IBaseService<CliUsuario>, ILogin
    {
        private IBaseRepository<CliUsuario> repository;

        public CliUsuarioService()
        {
            repository = new EFRepository<CliUsuario>();
        }

        public IQueryable<CliUsuario> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(CliUsuario item)
        {
            // formata
            item.Email = item.Email.ToLower().Trim();
            item.Nome = item.Nome.ToUpper().Trim();
            item.Telefone = item.Telefone.ToUpper().Trim();

            // valida
            if (repository.Listar().Where(x => x.Email == item.Email && x.IdEmpresa == item.IdEmpresa && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um usuário cadastrado com este e-mail nesta empresa");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                item.CadastradoEm = DateTime.Now;
                // TODO: quando incluir um novo usuario tem que enviar e-mail com a senha para ele
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public CliUsuario Excluir(int id)
        {
            try
            {
                return repository.Excluir(id);
            }
            catch (Exception)
            {
                // BD nao permite exclusao por FK, inativo
                var usuario = repository.Find(id);
                if (usuario != null)
                {
                    usuario.Ativo = false;
                    return repository.Alterar(usuario);
                }
                return usuario;
            }
        }

        public CliUsuario Find(int id)
        {
            return repository.Find(id);
        }

        CliUsuario ILogin.ValidaLogin(string email, string senha)
        {
            email = email.ToLower().Trim();

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(senha))
            {
                return repository.Listar().Where(x => x.Ativo == true && x.Email == email && x.Password == senha).FirstOrDefault();
            }

            return null;
        }

        public int GetIdCliUsuario(string email)
        {
            email = email.ToLower().Trim();

            var usuario = repository.Listar().Where(x => x.Ativo == true && x.Email == email).FirstOrDefault();

            if (usuario != null)
            {
                return usuario.Id;
            }

            return 0;
        }

        public int GetIdUsuarioByNome(string nome, int idEmpresa)
        {
            var usuario = repository.Listar().Where(x => x.Nome == nome && x.IdEmpresa == idEmpresa).FirstOrDefault();

            if (usuario != null)
            {
                return usuario.Id;
            }

            return 0;
        }
    }
}
