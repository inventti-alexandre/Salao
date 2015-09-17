using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class AreaUnitTest
    {
        IBaseService<Area> service;

        public AreaUnitTest()
        {
            service = new AreaService();
        }

        [TestMethod]
        public void AreaIncluir()
        {
            // Arrange
            var area = new Area { AlteradoPor = 1, Descricao = "CABELO" };

            // Act
            var id = service.Gravar(area);
            var areaIncluida = service.Find(id);

            // Assert
            Assert.AreEqual(id, areaIncluida.Id);
        }
    }
}
