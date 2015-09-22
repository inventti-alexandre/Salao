using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;

namespace Salao.Web.Tests.Cliente
{
    [TestClass]
    public class CadastroUnitTest
    {
        ICadastroEmpresa service;

        public CadastroUnitTest()
        {
            service = new CadastroEmpresaService();
        }

        [TestMethod]
        public void EmpresaCadastrar()
        {
            // Arrange
            var cadastro = new CadastroEmpresa
            {
                CadastradoPor = 1,
                Bairro = "MIRANDOPOLIS",
                Cep = "04043200",
                Cidade = "SAO PAULO",
                Cnpj = "",
                Cpf = "12557634859",
                Complemento = "APTO 12",
                Contato = "JOSE",
                Cortesia = true,
                DDD = "11",
                Desconto = 100M,
                DescontoCarencia = 3,
                Email = "jb.alessandro34@gmail.com",
                Fantasia = "FLORISBELLA",
                IdEstado = 1,
                Logradouro = "RUA LUIS GOIS",
                Numero = "1850",
                Observ = "",
                RazaoSocial = "FLORISBELLA LTDA",
                Telefone = "997218670",
                TipoEndereco = 1,
                TipoPessoa = 2
            };
            
            // Act
            int id = service.Incluir(cadastro);

            // Assert
            Assert.AreNotEqual(0, id);
        }

        [TestMethod]
        public void EmpresaFind()
        {
            // Arrange
            CadastroEmpresa cadastro;

            // Act
            cadastro = service.Find(11);

            // Assert
            Assert.AreNotEqual(null, cadastro);
        }
    }
}
