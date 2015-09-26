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
            var parametro1 = new SistemaParametro { AlteradoPor = 1, Codigo = "PROMO_DESCONTO", Descricao = "DESCONTO PRADRAO NA PROMOCAO", Valor = "100" };
            var parametro2 = new SistemaParametro { AlteradoPor = 1, Codigo = "PROMO_CARENCIA", Descricao = "MESES DE CARENCIA NA PROMOCAO", Valor = "3" };

            // Act
            int id1 = service.Gravar(parametro1);
            int id2 = service.Gravar(parametro2);

            // Assert
            Assert.IsTrue(id1 > 0);
            Assert.IsTrue(id2 > 0);
        }        
    }
}
