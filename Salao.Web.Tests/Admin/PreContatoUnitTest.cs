using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Domain.Abstract;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class PreContatoUnitTest
    {
        IBaseService<PreContato> service;

        public PreContatoUnitTest()
        {
            service = new PreContatoService();
        }

        [TestMethod]
        public void PreContatoIncluir()
        {
            // Arrange
            var contato = new PreContato
            {
                Cidade = "SAO PAULO",
                Email = "salao@gmail.com",
                IdEstado = 1,
                Nome = "Florinda",
                NomeSalao = "Dona Flor",
                Telefone = "9999-99999"
            };

            // Act
            var id = service.Gravar(contato);
            var contatoIncluido = service.Find(id);

            // Assert
            Assert.AreEqual(id, contatoIncluido.Id);
        }
    }
}
