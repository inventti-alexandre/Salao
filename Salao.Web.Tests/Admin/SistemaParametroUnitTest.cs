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

            var parametro3 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_USESSL", Descricao = "UseSsl", Valor = "true" };
            var parametro4 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SERVERSMTP", Descricao = "Smtp server", Valor = "smtp.gmail.com" };
            var parametro5 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SERVERPORT", Descricao = "Porta smtp", Valor = "587" };
            var parametro6 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SENDER", Descricao = "Sender (email)", Valor = "contact@scrumtopractice.com" };
            var parametro7 = new SistemaParametro { AlteradoPor = 1, Codigo = "EMAIL_SENDERPASSWORD", Descricao = "Password", Valor = "b8c7p2c6" };
             

            // Act
            int id1 = service.Gravar(parametro1);
            int id2 = service.Gravar(parametro2);
            int id3 = service.Gravar(parametro3);
            int id4 = service.Gravar(parametro4);
            int id5 = service.Gravar(parametro5);
            int id6 = service.Gravar(parametro6);
            int id7 = service.Gravar(parametro7);


            // Assert
            Assert.IsTrue(id6 > 0);
            Assert.IsTrue(id7 > 0);
        }        
    }
}
