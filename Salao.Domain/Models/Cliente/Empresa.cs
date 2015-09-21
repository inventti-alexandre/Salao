using Salao.Domain.Models.Admin;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o nome fantasia da empresa")]
        [StringLength(60,ErrorMessage="Máximo de 60 caracteres")]
        [Display(Name="Nome fantasia")]
        public string Fantasia { get; set; }

        [StringLength(60, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Razão social")]
        public string RazaoSocial { get; set; }

        [Required]
        [Range(1,999999999,ErrorMessage="Endereço inválido")]
        [HiddenInput(DisplayValue=false)]
        public int IdEndereco { get; set; }

        [Required]
        [Range(1,2,ErrorMessage="Selecione o tipo de pessoa (física/jurídica)")]
        [Display(Name="Tipo de pessoa")]
        [HiddenInput(DisplayValue=false)]
        public Int16 TipoPessoa { get; set; }

        [StringLength(16, ErrorMessage = "O CNPJ é composto por 16 números",MinimumLength=16)]
        [Display(Name = "CNPJ")]
        public string Cnpj { get; set; }

        [StringLength(11, ErrorMessage = "O CPF é composto por 11 números",MinimumLength=11)]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o nome para contato na empresa")]
        [StringLength(60, ErrorMessage = "O nome do contato é composto por no máximo 60 caracteres")]
        public string Contato { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }
        
        public bool Cortesia { get; set; }
        public decimal Desconto { get; set; }

        [Display(Name="Carência (meses)")]
        public int DescontoCarencia { get; set; }

        [Required(ErrorMessage="Avaliação do preço inválida")]
        [Display(Name="Avaliação preço")]
        [Range(0,5,ErrorMessage="Avaliação do preço inválida")]
        public Int16 PrecoAvaliacao { get; set; }

        [Required(ErrorMessage="Avaliação do cliente inválida")]
        [Display(Name="Avaliação cliente")]
        [Range(0,5,ErrorMessage="Avaliação do cliente inválida")]        
        public Int16 ClienteAvaliacao { get; set; }

        public bool Aprovado { get; set; }

        [Display(Name="Exibir site")]
        public bool Exibir { get; set; }
        
        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Cadastrado em")]
        public DateTime CadastradoEm { get; set; }

        [Required]
        [Display(Name="Cadastrado por")]
        public int CadastradoPor { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [Display(Name="Alterado por")]
        public int? AlteradoPor { get; set; }


        [NotMapped]
        public virtual Endereco.Endereco Endereco
        {
            get
            {
                return new Salao.Domain.Service.Endereco.EnderecoService().Find(IdEndereco);
            }
        }

        [NotMapped]
        [Display(Name = "Cadastrado por")]
        public virtual Usuario CadastradoPorUsuario
        {
            get
            {
                return new Salao.Domain.Service.Admin.UsuarioService().Find(CadastradoPor);
            }
        }
    }
}
