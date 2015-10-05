using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Abstract;
using Salao.Domain.Service.Cliente;

namespace Salao.Web.Tests.Cliente
{
    [TestClass]
    public class ServicoUnitTest
    {
        private IBaseService<Servico> service;

        public ServicoUnitTest()
        {
            service = new ServicoService();
        }

        [TestMethod]
        public void ServicoIncluir()
        {
            // Arrange
            var servico = new Servico
            {
                AlteradoEm = DateTime.Now,
                Descricao = "Corte simples feminino",
                Detalhe = "Inclui lavagem",
                IdSalao = 3,
                IdSubArea = 1,
                Preco = 80M,
                PrecoSemDesconto = 100M,
                Tempo = new TimeSpan(0, 40, 0)
            };

            // Act
            int id = service.Gravar(servico);

            // Assert
            Assert.AreEqual(id, service.Find(id).Id);
        }
    }
}
