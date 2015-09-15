﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class UsuarioUnitTest
    {
        UsuarioService service;

        public UsuarioUnitTest()
        {
            service = new UsuarioService();
        }

        [TestMethod]
        public void Listar()
        {
            // Arrange
            IEnumerable<Usuario> usuarios;

            // Act
            usuarios = service.Listar().ToList();

            // Assert
            Assert.AreEqual(2, usuarios.Count());
        }

        [TestMethod]
        public void Incluir()
        {
            // Arrange
            var usuario = new Usuario
            {
                Email = "pedro.freitas@gmail.com",
                Login = "pedro",
                Nome = "Pedro Freitas",
                Ramal = "1",
                Senha = "123456",
                Telefone = "99804-8177"
            };

            // Act
            int id = service.Gravar(usuario);

            // Assert
            Assert.AreEqual(2, id);
        }

        [TestMethod]
        public void Gravar()
        {
            // Arrange
            var usuario = service.Find(1);

            // Act
            service.Gravar(usuario);
            var usuario2 = service.Find(1);

            // Assert
            Assert.AreEqual(usuario.Nome, usuario2.Nome);
        }

        [TestMethod]
        public void ValidaLogin()
        {
            // Arrange
            var login = "jose";
            var senha = "123456";

            // Act
            var usuario = service.ValidaLogin(login, senha);

            // Assert
            Assert.AreNotEqual(null, usuario);
        }

        [TestMethod]
        public void GetIdUsuario()
        {
            // Arrange
            var login = "jose";

            // Act
            var id = service.GetIdUsuario(login);

            // Assert
            Assert.AreEqual(1, id);
        }
    }
}
