using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;

namespace Salao.Web.Tests.Endereco
{
    [TestClass]
    public class BairroUnitTest
    {
        private BairroService service;

        public BairroUnitTest()
        {
            service = new BairroService();
        }

        [TestMethod]
        public void BairroIncluir()
        {
            // Arrange
            var bairro = new EnderecoBairro { IdCidade = 1, Descricao = "MIRANDOPOLIS" };

            // Act
            var id = service.Gravar(bairro);

            // Assert
            Assert.AreNotEqual(0, id);
        }
    }
}
