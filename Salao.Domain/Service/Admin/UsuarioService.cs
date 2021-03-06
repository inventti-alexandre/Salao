﻿using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Repository;
using System;
using System.Linq;

namespace Salao.Domain.Service.Admin
{
    public class UsuarioService : IBaseService<Usuario>, ILogin, ITrocaSenha
    {
        private IBaseRepository<Usuario> repository;

        public UsuarioService()
        {
            repository = new EFRepository<Usuario>();
        }

        public IQueryable<Usuario> Listar()
        {
            return repository.Listar();
        }

        public int Gravar(Usuario item)
        {
            // formata
            item.Email = item.Email.ToLower().Trim();
            item.Nome = item.Nome.ToUpper().Trim();
            item.Telefone = item.Telefone.ToUpper().Trim();
            if (string.IsNullOrEmpty(item.Roles))
            {
                item.Roles = "";
            }
            item.Roles = item.Roles.ToLower().Trim();

            // valida
            if (repository.Listar().Where(x => x.Nome == item.Nome && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Usuário já cadastrado");
            }

            if (repository.Listar().Where(x => x.Login == item.Login && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Login já utilizado por outro usuário");
            }

            if (repository.Listar().Where(x => x.Email == item.Email && x.Id != item.Id).Count() > 0)
            {
                throw new ArgumentException("Já existe um usuário cadastrado com este e-mail");
            }

            // grava
            if (item.Id == 0)
            {
                item.Ativo = true;
                item.CadastradoEm = DateTime.Now;
                return repository.Incluir(item).Id;
            }

            return repository.Alterar(item).Id;
        }

        public Usuario Excluir(int id)
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

        public Usuario Find(int id)
        {
            return repository.Find(id);
        }

        public Usuario ValidaLogin(string login, string senha)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(senha))
            {
                return repository.Listar().Where(x => x.Ativo == true && x.Login == login && x.Senha == senha).FirstOrDefault();
            }

            return null;
        }

        public int GetIdUsuario(string login)
        {
            var usuario = repository.Listar().Where(x => x.Ativo == true && x.Login == login).FirstOrDefault();

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

            if (usuario.Senha != senhaAnterior)
            {
                throw new ArgumentException("Senha atual não confere");
            }

            usuario.Senha = novaSenha;
            repository.Alterar(usuario);

            if (enviarEmail == true)
            {
                var mensagem = string.Format("Sua nova senha de acesso é {0}.", usuario.Senha);
                EnviarNovaSenha(usuario, mensagem);
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
            usuario.Senha = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8);
            repository.Alterar(usuario);

            // enviar nova senha para o usuario
            var mensagem = string.Format("Sua nova senha de acesso é {0}.", usuario.Senha);
            EnviarNovaSenha(usuario, mensagem);
        }

        private void EnviarNovaSenha(Usuario usuario, string mensagem)
        {
            var email = new Email.EnviarEmail();
            // TODO: assunto deve conter o nome do app
            email.Enviar(usuario.Nome, usuario.Email, "Nova senha para acesso", mensagem.ToString());
        }
    }

}
