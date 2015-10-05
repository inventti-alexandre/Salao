using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class CadastroEmpresa
    {
        public int Id { get; set; }
        public int IdEndereco { get; set; }

        [Required(ErrorMessage = "Informe o nome fantasia da empresa")]
        [StringLength(60, ErrorMessage = "Máximo de 60 caracteres")]
        [Display(Name = "Nome fantasia")]
        public string Fantasia { get; set; }

        [StringLength(60, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Razão social")]
        public string RazaoSocial { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Selecione o tipo de pessoa (física/jurídica)")]
        [Display(Name = "Tipo de pessoa")]
        [HiddenInput(DisplayValue = false)]
        public Int16 TipoPessoa { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required]
        [Range(1,2,ErrorMessage="Tipo de endereço inválido")]
        [Display(Name="Tipo de endereço")]
        public int TipoEndereco { get; set; }

        [Required]
        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        [Required]
        [Display(Name = "Logradouro")]
        [StringLength(100, ErrorMessage = "O nome do logradouro é composto por no máximo 100 caracteres")]
        public string Logradouro { get; set; }

        [Required]
        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "O número é composto por no máximo 10 caracteres")]
        public string Numero { get; set; }

        [StringLength(40, ErrorMessage = "O complemento é composto por no máximo 40 caracteres")]
        public string Complemento { get; set; }

        [Required]
        [Display(Name="Bairro")]
        [StringLength(100, ErrorMessage="O bairro é composto por no máximo 100 caracteres")]
        public string Bairro { get; set; }

        [Required]
        [Display(Name = "Cidade")]
        [StringLength(100, ErrorMessage = "A cidade é composto por no máximo 100 caracteres")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [Range(1,int.MaxValue,ErrorMessage="Selecione o Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Informe o nome para contato na empresa")]
        [StringLength(60, ErrorMessage = "O nome do contato é composto por no máximo 60 caracteres")]
        public string Contato { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o DDD")]
        [StringLength(20, ErrorMessage = "Máximo de 3 caracteres")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [StringLength(20, ErrorMessage = "Máximo de 20 caracteres")]
        public string Telefone { get; set; }

        [StringLength(6, ErrorMessage="Máximo de 6 caracteres")]
        public string Ramal { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        public bool Cortesia { get; set; }
        public decimal Desconto { get; set; }

        [Display(Name = "Carência (meses)")]
        public int DescontoCarencia { get; set; }

        public int CadastradoPor { get; set; }

        [NotMapped]
        public virtual string CadastradoPorNome
        {
            get
            {
                return new Domain.Service.Admin.UsuarioService().Find(CadastradoPor).Nome;
            }
        }

        [NotMapped]
        public virtual string Estado {
            get
            {
                return new Domain.Service.Endereco.EstadoService().Find(IdEstado).UF;
            }
        }

        [NotMapped]
        public virtual string TipoEnderecoDescricao
        {
            get
            {
                return new Domain.Service.Endereco.TipoEnderecoService().Find(TipoEndereco).Descricao;
            }
        }

    }
}
