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

        public int Gravar(CadastroEmpresa cadastro)
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

                // grava empresa
                return gravarEmpresa(cadastro, idEndereco);
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
                    IdEndereco = empresa.IdEndereco,
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

        #region [ privates ]

        private int gravarEmpresa(CadastroEmpresa cadastro, int idEndereco)
        {
            var empresa = serviceEmpresa.Find(cadastro.Id);
            if (empresa == null)
            {
                empresa = new Empresa();
                empresa.AlteradoPor = 0;
                empresa.ClienteAvaliacao = 0;
                empresa.PrecoAvaliacao = 0;
            }

            empresa.AlteradoEm = DateTime.Now;
            empresa.CadastradoEm = DateTime.Now;
            empresa.CadastradoPor = cadastro.CadastradoPor;
            empresa.Cnpj = "" + cadastro.Cnpj;
            empresa.Contato = "" + cadastro.Contato;
            empresa.Cpf = "" + cadastro.Cpf;
            empresa.Cortesia = cadastro.Cortesia;
            empresa.Desconto = cadastro.Desconto;
            empresa.DescontoCarencia = cadastro.DescontoCarencia;
            empresa.Id = cadastro.Id;
            empresa.IdEndereco = idEndereco;
            empresa.Observ = "" + cadastro.Observ;
            empresa.Fantasia = "" + cadastro.Fantasia;
            empresa.RazaoSocial = "" + cadastro.RazaoSocial;
            empresa.TipoPessoa = cadastro.TipoPessoa;

            return serviceEmpresa.Gravar(empresa);
        }

        private int gravarTelefone(CadastroEmpresa cadastro, int idEndereco)
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

        private int gravarEmail(CadastroEmpresa cadastro, int idEndereco)
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

        private int gravarEndereco(CadastroEmpresa cadastro)
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
