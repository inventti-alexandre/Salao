using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Service.Endereco;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Abstract;

namespace Salao.Web.Tests.Endereco
{
    [TestClass]
    public class TipoEnderecoUnitTest
    {
        TipoEnderecoService service;

        public TipoEnderecoUnitTest()
        {
            service = new TipoEnderecoService();
        }

        [TestMethod]
        public void TipoEnderecoIncluir()
        {
            // Arrange
            var tipo = new EnderecoTipoEndereco { Descricao = "COMERCIAL" };

            // Act
            int id = service.Gravar(tipo);

            // Assert
            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public void TipoEnderecoAlterar()
        {
            // Arrange
            var tipo = service.Find(1);

            // Act
            int id = service.Gravar(tipo);

            // Assert
            Assert.AreEqual(1, id);
        }
    }
}
