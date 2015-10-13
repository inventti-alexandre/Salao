using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class CliUsuarioService: IBaseService<CliUsuario>, ILogin, ITrocaSenha
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

        public void TrocarSenha(int idUsuario, string senhaAnterior, string novaSenha, bool enviarEmail = true)
        {
            var usuario = repository.Find(idUsuario);

            if (usuario == null)
            {
                throw new ArgumentException("Usuário inválido");
            }

            if (usuario.Password != senhaAnterior)
            {
                throw new ArgumentException("Senha atual não confere");
            }

            usuario.Password = novaSenha;
            usuario.Ativo = true;
            repository.Alterar(usuario);

            if (enviarEmail == true)
            {
                EnviarNovaSenha(usuario);
                
            }
        }

        public void RedefinirSenha(int idUsuario)
        {
            var usuario = repository.Find(idUsuario);

            if (usuario == null)
            {
                throw new ArgumentException("Usuário inválido");
            }

            usuario.Ativo = true;
            usuario.Password = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            repository.Alterar(usuario);

            // envia nova senha para usuario
            EnviarNovaSenha(usuario);
        }

        private void EnviarNovaSenha(CliUsuario usuario)
        {
            var mensagem = new System.Text.StringBuilder()
            .Append("Sua nova senha para acesso é ")
            .Append(usuario.Password);

            var email = new Email.EnviarEmail();           
            // TODO: assunto deve conter o nome do app
            email.Enviar(usuario.Nome, usuario.Email, "Nova senha para acesso", mensagem.ToString());
        }

    }
}
