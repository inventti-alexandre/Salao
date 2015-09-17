using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using System.Collections.Generic;
using System.Linq;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class GrupoUnitTest
    {
        IBaseService<Grupo> service;

        public GrupoUnitTest()
        {
            service = new GrupoService();
        }

        [TestMethod]
        public void GrupoIncluir()
        {
            // Arrange
            var grupo = new Grupo { Descricao = "USUARIO PADRAO" };

            // Act
            var id = service.Gravar(grupo);

            // Assert
            Assert.AreNotEqual(0, grupo);
        }

        [TestMethod]
        public void GrupoAlterar()
        {
            // Arrange
            Grupo grupo;

            // Act
            grupo = service.Find(1);

            // Assert
            Assert.AreEqual("ADMINISTRADOR", grupo.Descricao);
        }

        [TestMethod]
        public void GrupoExcluir()
        {
            // Arrange
            Grupo grupo;

            // Act
            grupo = service.Excluir(2);

            // Assert
            Assert.AreEqual("USUARIO PADRAO", grupo.Descricao);
        }

        [TestMethod]
        public void Listar()
        {
            // Arrange
            List<Grupo> usuarios;

            // Act
            usuarios = service.Listar().ToList();

            // Assert
            Assert.AreNotEqual(0, usuarios.Count());
        }
    }
}
