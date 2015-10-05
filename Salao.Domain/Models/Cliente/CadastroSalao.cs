using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class CadastroSalao
    {
        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int IdEmpresa { get; set; }

        [Required(ErrorMessage = "Informe o nome fantasia do salão")]
        [StringLength(60, ErrorMessage = "O nome fantasia é composto por no máximo 60 caracteres")]
        [Display(Name = "Nome fantasia")]
        public string Fantasia { get; set; }

        [Required]
        [Range(1, 2, ErrorMessage = "Selecione o tipo de pessoa (física/jurídica)")]
        [Display(Name = "Tipo de pessoa")]
        [HiddenInput(DisplayValue = false)]
        public Int16 TipoPessoa { get; set; }

        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o nome para contato na empresa")]
        [StringLength(60, ErrorMessage = "O nome do contato é composto por no máximo 60 caracteres")]
        public string Contato { get; set; }

        [DataType(DataType.MultilineText)]
        public string Sobre { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        public bool Cortesia { get; set; }

        public decimal Desconto { get; set; }

        [Display(Name = "Carência (meses)")]
        public int DescontoCarencia { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}")]
        [Range(-90, 90, ErrorMessage = "A latitude varia entre -90 e 90 graus")]
        public double Latitude { get; set; }

        [DisplayFormat(DataFormatString = "{0:N6}")]
        [Range(-179, 180, ErrorMessage = "A longitude varia entre -179 e 180 graus")]
        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        public int IdEndereco { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de endereço")]
        [Range(0, int.MaxValue, ErrorMessage = "Selecione o tipo de endereço")]
        [Display(Name = "Tipo endereço")]
        public int TipoEndereco { get; set; }

        [Required(ErrorMessage = "Informe o CEP")]
        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres", MinimumLength = 8)]
        public string Cep { get; set; }

        [Required(ErrorMessage = "Informe o logradouro")]
        [Display(Name = "Logradouro")]
        [StringLength(100, ErrorMessage = "O nome do logradouro é composto por no máximo 100 caracteres")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "Informe o número")]
        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "O número é composto por no máximo 10 caracteres")]
        public string Numero { get; set; }

        [StringLength(40, ErrorMessage = "O complemento é composto por no máximo 40 caracteres")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "Informe o bairro")]
        [StringLength(100, ErrorMessage = "O bairro é composto por no máximo 100 caracteres")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Informe a cidade")]
        [StringLength(100, ErrorMessage = "A cidade é composta por no máximo 100 caracteres")]
        public string Cidade { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione o Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage = "Informe o email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [StringLength(3, ErrorMessage = "O DD é composto por no máximo 3 caracteres")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "Informe o telefone")]
        [StringLength(20, ErrorMessage = "O telefone é composto por no máximo 20 caracteres")]
        public string Telefone { get; set; }

        [StringLength(6, ErrorMessage = "O ramal é composto por no máximo 6 caracteres")]
        public string Ramal { get; set; }

        public bool Aprovado { get; set; }

        [Display(Name = "Exibir site")]
        public bool Exibir { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Cadastrado em")]
        public DateTime CadastradoEm { get; set; }
    }
}
