using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Abstract.Endereco;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Models.Endereco;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class CadastroSalaoService : ICadastroSalao
    {

        IBaseService<Salao.Domain.Models.Cliente.Salao> service;
        IBaseService<Salao.Domain.Models.Endereco.Endereco> serviceEndereco;
        IBaseService<Salao.Domain.Models.Endereco.EnderecoEstado> serviceEstado;
        IBaseService<Salao.Domain.Models.Endereco.EnderecoTelefone> serviceTelefone;
        IBaseService<Salao.Domain.Models.Endereco.EnderecoEmail> serviceEmail;
        IEnderecoService<Salao.Domain.Models.Endereco.EnderecoBairro> serviceBairro;
        IEnderecoService<Salao.Domain.Models.Endereco.EnderecoCidade> serviceCidade;

        public CadastroSalaoService()
        {
            service = new SalaoService();
            serviceEndereco = new Endereco.EnderecoService();
            serviceEstado = new Salao.Domain.Service.Endereco.EstadoService();
            serviceCidade = new Salao.Domain.Service.Endereco.CidadeService();
            serviceBairro = new Salao.Domain.Service.Endereco.BairroService();
            serviceTelefone = new Salao.Domain.Service.Endereco.TelefoneService();
            serviceEmail = new Salao.Domain.Service.Endereco.EmailService();
        }

        public int Gravar(Salao.Domain.Models.Cliente.CadastroSalao cadastro)
        {
            int idEndereco = 0;
            int idEmail = 0;
            int idTelefone = 0;

            try
            {
                // grava endereco
                idEndereco = gravarEndereco(cadastro);
                idEmail = gravarEmail(cadastro, idEndereco);
                idTelefone = gravarTelefone(cadastro, idEndereco);

                // grava salao
                return gravarSalao(cadastro, idEndereco);
            }
            catch (Exception e)
            {
                serviceEmail.Excluir(idEmail);
                serviceTelefone.Excluir(idTelefone);
                serviceEndereco.Excluir(idEndereco);
                throw new ArgumentException(e.Message);
            }
        }

        public Salao.Domain.Models.Cliente.Salao Excluir(int id)
        {
            return service.Excluir(id);
        }

        public Salao.Domain.Models.Cliente.CadastroSalao Find(int id)
        {
            var salao = service.Find(id);

            if (salao == null)
            {
                return null;
            }

            // valida

            var endereco = salao.Endereco;
            var telefone = endereco.Telefone;
            var email = endereco.Email;

            return new CadastroSalao
            {
                AlteradoEm = salao.AlteradoEm,
                Aprovado = salao.Aprovado,
                Ativo = salao.Ativo,
                Bairro = endereco.Bairro.Descricao,
                CadastradoEm = salao.CadastradoEm,
                Cep = endereco.Cep,
                Cidade = endereco.Cidade.Descricao,
                Cnpj = salao.Cnpj,
                Complemento = endereco.Complemento,
                Contato = salao.Contato,
                Cortesia = salao.Cortesia,
                Cpf = salao.Cpf,
                DDD = telefone.DDD,
                Desconto = salao.Desconto,
                DescontoCarencia = salao.DescontoCarencia,
                Email = email.Email,
                IdEstado = endereco.Estado.Id,
                Exibir = salao.Exibir,
                Fantasia = salao.Fantasia,
                Id = salao.Id,
                IdEmpresa = salao.IdEmpresa,
                IdEndereco = endereco.Id,
                Latitude = salao.Latitude,
                Longitude = salao.Longitude,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Observ = salao.Observ,
                Ramal = telefone.Ramal,
                Sobre = salao.Sobre,
                Telefone = telefone.Telefone,
                TipoEndereco = endereco.TipoEndereco.Id,
                TipoPessoa = salao.TipoPessoa
            };
        }

        #region [ privates ]

        private int gravarSalao(Models.Cliente.CadastroSalao cadastro, int idEndereco)
        {
            var salao = service.Find(cadastro.Id);
            if (salao == null)
            {
                salao = new Salao.Domain.Models.Cliente.Salao();
                salao.CadastradoEm = DateTime.Now;
            }

            salao.AlteradoEm = DateTime.Now;
            salao.Aprovado = cadastro.Aprovado;
            salao.Ativo = cadastro.Ativo;
            salao.Cnpj = "" + cadastro.Cnpj;
            salao.Contato = "" + cadastro.Contato;
            salao.Cortesia = cadastro.Cortesia;
            salao.Cpf = cadastro.Cpf;
            salao.Desconto = cadastro.Desconto;
            salao.DescontoCarencia = cadastro.DescontoCarencia;
            salao.Exibir = cadastro.Exibir;
            salao.Fantasia = "" + cadastro.Fantasia;
            salao.IdEmpresa = cadastro.IdEmpresa;
            salao.IdEndereco = idEndereco;
            salao.Latitude = cadastro.Latitude;
            salao.Longitude = cadastro.Longitude;
            salao.Observ = "" + cadastro.Observ;
            salao.Sobre = "" + cadastro.Sobre;
            salao.TipoPessoa = cadastro.TipoPessoa;

            return service.Gravar(salao);           
        }

        private int gravarTelefone(Models.Cliente.CadastroSalao cadastro, int idEndereco)
        {
            var telefone = new EnderecoTelefone { Ativo = true, Contato = "" + cadastro.Contato, DDD = "" + cadastro.DDD, IdEndereco = idEndereco, Ramal = "" + cadastro.Ramal, Telefone = cadastro.Telefone };
            if (idEndereco != 0)
            {
                var telefoneDb = serviceTelefone.Listar().Where(x => x.IdEndereco == idEndereco).FirstOrDefault();
                if (telefoneDb != null)
                {
                    telefoneDb.Ativo = true;
                    telefoneDb.Contato = telefone.Contato;
                    telefoneDb.DDD = telefone.DDD;
                    telefoneDb.Ramal = telefone.Ramal;
                    telefoneDb.Telefone = telefone.Telefone;
                    return serviceTelefone.Gravar(telefoneDb);
                }
            }
            return serviceTelefone.Gravar(telefone);
        }

        private int gravarEmail(Models.Cliente.CadastroSalao cadastro, int idEndereco)
        {
            var email = new EnderecoEmail { Ativo = true, Email = cadastro.Email, IdEndereco = idEndereco };
            if (idEndereco != 0)
            {
                var emailDb = serviceEmail.Listar().Where(x => x.IdEndereco == idEndereco).FirstOrDefault();
                if (emailDb != null)
                {
                    emailDb.Ativo = true;
                    emailDb.Email = email.Email;
                    return serviceEmail.Gravar(emailDb);
                }
            }
            return serviceEmail.Gravar(email);
        }

        private int gravarEndereco(Models.Cliente.CadastroSalao cadastro)
        {
            // grava endereco
            var endereco = new Salao.Domain.Models.Endereco.Endereco();
            endereco.Id = cadastro.IdEndereco;
            endereco.IdEstado = cadastro.IdEstado;
            endereco.IdCidade = serviceCidade.GetId(cadastro.Cidade, cadastro.IdEstado);
            endereco.IdBairro = serviceBairro.GetId(cadastro.Bairro, endereco.IdCidade);
            endereco.Cep = cadastro.Cep;
            endereco.Complemento = "" + cadastro.Complemento;
            endereco.IdTipoEndereco = cadastro.TipoEndereco;
            endereco.Logradouro = cadastro.Logradouro;
            endereco.Numero = cadastro.Numero;
            return serviceEndereco.Gravar(endereco);
        }

        #endregion
    }
}
