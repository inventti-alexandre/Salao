using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;

namespace Salao.Web.Tests.Endereco
{
    [TestClass]
    public class EnderecoUnitTest
    {

        EnderecoService service;

        public EnderecoUnitTest()
        {
            service = new EnderecoService();
        }

        [TestMethod]
        public void EnderecoIncluir()
        {
            // Arrange
            var endereco = new Salao.Domain.Models.Endereco.Endereco
            {
                IdBairro = 1,
                IdCidade = 1,
                IdEstado = 1,
                IdTipoEndereco = 1,
                Logradouro = "RUA LUIS GOIS",
                Numero = "1850",
                Complemento = "ap 12"
            };

            // Act
            var id = service.Gravar(endereco);

            // Assert
            Assert.AreEqual(1, id);
        }
    }
}
