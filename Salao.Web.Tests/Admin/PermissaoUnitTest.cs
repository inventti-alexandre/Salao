using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Service.Admin;
using Salao.Domain.Models.Admin;
using System.Collections.Generic;
using System.Linq;

namespace Salao.Web.Tests.Admin
{
    [TestClass]
    public class PermissaoUnitTest
    {
        PermissaoService service;

        public PermissaoUnitTest()
        {
            service = new PermissaoService();
        }

        [TestMethod]
        public void PermissaoIncluir()
        {
            // Arrange
            var permissao = new Permissao
            {
                AlteradoPor = 1,
                Descricao = "CADASTRAR PERMISSAO"
            };

            // Act
            int id = service.Gravar(permissao);

            // Assert
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void PermissaoAlterar()
        {
            // Arrange
            var permissao = service.Find(1);

            // Act
            permissao.Descricao = "CADASTRAR GRUPO";
            service.Gravar(permissao);
            var descricao = service.Find(1).Descricao;

            // Assert
            Assert.AreEqual("CADASTRAR GRUPO", descricao);
        }

        [TestMethod]
        public void PermissaoListar()
        {
            // Arrange
            List<Permissao> permissoes;
   
            // Act
            permissoes = service.Listar().ToList();

            // Assert
            Assert.AreNotEqual(0, permissoes.Count());
        }

        [TestMethod]
        public void PermissaoExcluir()
        {
            // Arrange 
            Permissao permissao;
            
            // Act
            permissao = service.Excluir(3);

            // Assert
            Assert.AreEqual(3, permissao.Id);
        }
    
    }
}
