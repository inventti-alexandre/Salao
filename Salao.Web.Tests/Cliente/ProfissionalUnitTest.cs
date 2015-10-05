using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Abstract;
using Salao.Domain.Service.Cliente;

namespace Salao.Web.Tests.Cliente
{
    [TestClass]
    public class ProfissionalUnitTest
    {
        IBaseService<Profissional> service;

        public ProfissionalUnitTest()
        {
            service = new ProfissionalService();
        }

        [TestMethod]
        public void ProfissionalIncluir()
        {
            // Arrange
            var profissional = new Profissional
            {
                AlteradoEm = DateTime.Now,
                Email = "jb.alessandro@gmail.com",
                IdSalao = 1,
                Nome = "JOSÉ",
                Telefone = "99721-8670"
            };

            // Act
            var id = service.Gravar(profissional);

            // Assert
            Assert.AreEqual(id, service.Find(id).Id);
        }
    }
}
