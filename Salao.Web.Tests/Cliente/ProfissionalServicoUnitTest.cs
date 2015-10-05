using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Models.Cliente;
using System.Linq;

namespace Salao.Web.Tests.Cliente
{
    [TestClass]
    public class ProfissionalServicoUnitTest
    {
        IProfissionalServico service;

        public ProfissionalServicoUnitTest()
        {
            service = new ProfissionalServicoService();
        }

        [TestMethod]
        public void ProfissionalServicoIncluir()
        {
            // Arrange
            int idProfissional = 1;
            int[] idServico = new int[2];            
            idServico[0] = 4;
            idServico[1] = 6;

            // Act
            service.Gravar(idProfissional, idServico);
            var lista = service.Listar().Where(x => x.IdProfissional == idProfissional).ToList();

            // Assert
            Assert.AreEqual(2, lista.Count);
            
        }
    }
}
