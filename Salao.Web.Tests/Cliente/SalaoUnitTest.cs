using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Abstract;

namespace Salao.Web.Tests.Cliente
{
    [TestClass]
    public class SalaoUnitTest
    {
        IBaseService<Salao.Domain.Models.Cliente.Salao> service;

        public SalaoUnitTest()
        {
            service = new Salao.Domain.Service.Cliente.SalaoService();
        }

        [TestMethod]
        public void SalaoIncluir()
        {
            // Arrange
            var salao = new Salao.Domain.Models.Cliente.Salao
            {
                Contato = "Jose",
                Cortesia = true,
                Cnpj = "61756995000100",
                Desconto = 0M,
                DescontoCarencia = 3,
                Exibir = false,
                Fantasia = "LINDISSIMA",
                IdEmpresa = 14,
                IdEndereco = 52,
                Latitude = 0,
                Longitude = 0,
                Observ = "",
                Sobre = "Lindo salão tradicional da região de Moema",
                TipoPessoa = 2
            };
            int id;
            
            // Act
            id = service.Gravar(salao);

            // Assert
            Assert.AreEqual(salao.Id, id);
        }
    }
}
