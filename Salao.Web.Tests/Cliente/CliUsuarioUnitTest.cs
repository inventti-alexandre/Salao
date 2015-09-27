using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Abstract;

namespace Salao.Web.Tests.Cliente
{
    [TestClass]
    public class CliUsuarioUnitTest
    {
        private IBaseService<CliUsuario> service;

        public CliUsuarioUnitTest()
        {
            service = new CliUsuarioService();
        }

        [TestMethod]
        public void CliUsuarioIncluir()
        {
            // Arrange
            var usuario = new CliUsuario
            {
                Email = "jb.alessandro@gmail.com",
                IdEmpresa = 1,
                Nome = "JOSE ALESSANDRO",
                Password = "123456",
                Telefone = "997218670"
            };

            // Act
            usuario.Id = service.Gravar(usuario);
            
            // Assert
            Assert.AreEqual(usuario.Email, service.Find(usuario.Id).Email);
        }
    }
}
