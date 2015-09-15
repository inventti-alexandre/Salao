using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Service.Admin;
using System.Linq;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class UsuarioGrupoUnitTest
    {
        IUsuarioGrupo service;

        public UsuarioGrupoUnitTest()
        {
            service = new UsuarioGrupoService();
        }

        [TestMethod]
        public void Incluir()
        {
            // Arrange
            var idUsuario = 1;
            var idGrupo = 1;
            
            // Act
            service.Incluir(idUsuario, idGrupo);
            var usuarioGrupo = service.Listar().Where(x => x.IdUsuario == 1).FirstOrDefault();

            // Assert
            Assert.AreEqual(idUsuario, usuarioGrupo.IdUsuario);
        }

        [TestMethod]
        public void Excluir()
        {
            // Arrange
            var idUsuario = 1;
            var idGrupo = 1;

            // Act
            service.Excluir(idUsuario, idGrupo);
            var usuarioGrupo = service.Listar().Where(x => x.IdUsuario == 1).FirstOrDefault();

            // Assert
            Assert.AreEqual(null, usuarioGrupo);
        }
    }
}
