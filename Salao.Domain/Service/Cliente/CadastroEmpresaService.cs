using Salao.Domain.Abstract;
using Salao.Domain.Abstract.Admin;
using Salao.Domain.Abstract.Endereco;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Models.Endereco;
using System;
using System.Linq;

namespace Salao.Domain.Service.Cliente
{
    public class CadastroEmpresaService : ICadastroEmpresa
    {
        IBaseService<Salao.Domain.Models.Endereco.Endereco> serviceEndereco;
        IBaseService<Salao.Domain.Models.Cliente.Empresa> serviceEmpresa;
        IBaseService<Salao.Domain.Models.Endereco.EnderecoEstado> serviceEstado;
        IBaseService<Salao.Domain.Models.Endereco.EnderecoTelefone> serviceTelefone;
        IBaseService<Salao.Domain.Models.Endereco.EnderecoEmail> serviceEmail;
        IEnderecoService<Salao.Domain.Models.Endereco.EnderecoBairro> serviceBairro;
        IEnderecoService<Salao.Domain.Models.Endereco.EnderecoCidade> serviceCidade;

        public CadastroEmpresaService()
        {
            serviceEndereco = new Endereco.EnderecoService();
            serviceEmpresa = new EmpresaService();
            serviceEstado = new Salao.Domain.Service.Endereco.EstadoService();
            serviceCidade = new Salao.Domain.Service.Endereco.CidadeService();
            serviceBairro = new Salao.Domain.Service.Endereco.BairroService();
            serviceTelefone = new Salao.Domain.Service.Endereco.TelefoneService();
            serviceEmail = new Salao.Domain.Service.Endereco.EmailService();
        }

        public int ChecarCadastroAnterior(string documento)
        {
            return serviceEmpresa.Listar().Where(x => x.Cpf == documento || x.Cnpj == documento).Count();
        }

        public int Incluir(CadastroEmpresa cadastro)
        {
            int idEndereco = 0;
            int idEmail = 0;
            int idTelefone = 0;

            try
            {
                // grava endereco
                var endereco = new Salao.Domain.Models.Endereco.Endereco();
                endereco.IdEstado = cadastro.IdEstado;
                endereco.IdCidade = serviceCidade.GetId(cadastro.Cidade, cadastro.IdEstado);
                endereco.IdBairro = serviceBairro.GetId(cadastro.Bairro, endereco.IdCidade);
                endereco.Cep = cadastro.Cep;
                endereco.Complemento = cadastro.Complemento;
                endereco.IdTipoEndereco = cadastro.TipoEndereco;
                endereco.Logradouro = cadastro.Logradouro;
                endereco.Numero = cadastro.Numero;
                endereco.Id = serviceEndereco.Gravar(endereco);
                idEndereco = endereco.Id;

                // grava email
                if (!string.IsNullOrEmpty(cadastro.Email))
                {
                    var email = new EnderecoEmail { Email = cadastro.Email, IdEndereco = endereco.Id };
                    idEmail = serviceEmail.Gravar(email);
                }

                // grava telefone
                if (!string.IsNullOrEmpty(cadastro.Telefone))
                {
                    var telefone = new EnderecoTelefone { Contato = cadastro.Contato, DDD = cadastro.DDD, Ramal = cadastro.Ramal, Telefone = cadastro.Telefone, IdEndereco = idEndereco };
                    idTelefone = serviceTelefone.Gravar(telefone);
                }

                // grava empresa
                var empresa = new Empresa
                {
                    AlteradoEm = DateTime.Now,
                    AlteradoPor = 0,
                    CadastradoEm = DateTime.Now,
                    CadastradoPor = cadastro.CadastradoPor,
                    ClienteAvaliacao = 0,
                    Cnpj = "" + cadastro.Cnpj,
                    Contato = "" + cadastro.Contato,
                    Cpf = "" + cadastro.Cpf,
                    Cortesia = cadastro.Cortesia,
                    Desconto = cadastro.Desconto,
                    DescontoCarencia = cadastro.DescontoCarencia,
                    IdEndereco = endereco.Id,
                    Observ = "" + cadastro.Observ,
                    Fantasia = cadastro.Fantasia,
                    PrecoAvaliacao = 0,
                    RazaoSocial = "" + cadastro.RazaoSocial,
                    TipoPessoa = cadastro.TipoPessoa
                };
                return serviceEmpresa.Gravar(empresa);
            }
            catch (Exception ex)
            {
                serviceEmail.Excluir(idEmail);
                serviceTelefone.Excluir(idTelefone);
                serviceEndereco.Excluir(idEndereco);
                throw new ArgumentException(ex.Message);
            }

            throw new ArgumentException("Não foi possível cadastrar esta empresa");
        }

        public int Alterar(CadastroEmpresa cadastro)
        {
            throw new NotImplementedException();
        }

        public Empresa Excluir(int id)
        {
            return serviceEmpresa.Excluir(id);
        }

        public CadastroEmpresa Find(int id)
        {
            // apenas empresas que ainda possuem cadastro sujeito a aprovacao
            var empresa = serviceEmpresa.Find(id);

            if (empresa != null)
            {
                if (empresa.Aprovado == true || (empresa.Aprovado == false && empresa.AlteradoPor != 0))
                {
                    // esta empresa ja foi avalida
                    throw new ArgumentException("Esta empresa já foi avalida anteriormente. Acesse o cadastro de empresas.");

                }

                var endereco = empresa.Endereco;
                var telefone = endereco.Telefone;
                var email = endereco.Email;

                return new CadastroEmpresa
                {
                    Bairro = endereco.Bairro.Descricao,
                    CadastradoPor = empresa.CadastradoPor,
                    Cep = endereco.Cep,
                    Cidade = endereco.Cidade.Descricao,
                    Cnpj = empresa.Cnpj,
                    Complemento = endereco.Complemento,
                    Contato = empresa.Contato,
                    Cortesia = empresa.Cortesia,
                    Cpf = empresa.Cpf,
                    DDD = telefone.DDD,
                    Desconto = empresa.Desconto,
                    DescontoCarencia = empresa.DescontoCarencia,
                    Email = email.Email,
                    Fantasia = empresa.Fantasia,
                    Id = empresa.Id,
                    IdEstado = endereco.IdEstado,
                    Logradouro = endereco.Logradouro,
                    Numero = endereco.Numero,
                    Observ = empresa.Observ,
                    RazaoSocial = empresa.RazaoSocial,
                    Telefone = endereco.Telefone.Telefone,
                    TipoEndereco = endereco.TipoEndereco.Id,
                    TipoPessoa = empresa.TipoPessoa
                };
            }

            return null;
        }


    }
}
