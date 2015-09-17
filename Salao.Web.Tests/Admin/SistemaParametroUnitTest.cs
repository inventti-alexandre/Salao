using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Domain.Abstract;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class SistemaParametroUnitTest
    {
        IBaseService<SistemaParametro> service;

        public SistemaParametroUnitTest()
        {
            service = new SistemaParametroService();
        }

        [TestMethod]
        public void SistemaParametroIncluir()
        {
            // Arrange
            var parametro = new SistemaParametro { AlteradoPor = 1, Codigo = "VALOR CODIGO", Descricao = "VALOR DESCRICAO", Valor = "VALOR VALOR" };

            // Act
            int id = service.Gravar(parametro);

            // Assert
            Assert.AreNotEqual(0, id);
        }        
    }
}
