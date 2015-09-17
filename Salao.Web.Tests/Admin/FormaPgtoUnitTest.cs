using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class FormaPgtoUnitTest
    {
        FormaPgtoService service;

        public FormaPgtoUnitTest()
        {
            service = new FormaPgtoService();
        }

        [TestMethod]
        public void FormaPgtoIncluir()
        {
            // Arrange
            var forma = new FormaPgto { AlteradoPor = 1, Descricao = "DINHEIRO" };

            // Act
            var id = service.Gravar(forma);

            // Assert
            Assert.AreNotEqual(0, id);
        }
    }
}
