using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Service.Endereco;

namespace Salao.Web.Tests.Endereco
{
    [TestClass]
    public class CidadeUnitTest
    {
        private CidadeService service;

        public CidadeUnitTest()
        {
            service = new CidadeService();
        }

        [TestMethod]
        public void CidadeIncluir()
        {
            // Arrange
            var cidade = new EnderecoCidade { Descricao = "SAO PAULO", IdEstado = 1 };
            
            // Act
            var id = service.Gravar(cidade);

            // Assert
            Assert.AreEqual(1, id);
        }
    }
}
