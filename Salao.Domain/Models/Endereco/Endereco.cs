using Salao.Domain.Service.Endereco;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using System.Linq;

namespace Salao.Domain.Models.Endereco
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Logradouro",Prompt="Av. Paulista")]
        [StringLength(100, ErrorMessage = "O nome do logradouro é composto por no máximo 100 caracteres")]
        public string Logradouro { get; set; }

        [Required]
        [Display(Name = "Número",Prompt="Ex: 2100")]
        [StringLength(10, ErrorMessage = "O número é composto por no máximo 10 caracteres")]
        public string Numero { get; set; }

        [Display(Name="Complemento",Description="Complemento do endereço, ex.: apto 22")]
        [StringLength(40, ErrorMessage="O complemento é composto por no máximo 40 caracteres")]
        public string Complemento { get; set; }

        [Required]
        [Display(Name = "CEP", Prompt="99999-999", Description="Código de endereçamento postal")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres",MinimumLength=8)]
        public string Cep { get; set; }

        public bool Ativo { get; set; }

        [Required(ErrorMessage = "Selecione o bairro")]
        [Range(0,int.MaxValue,ErrorMessage="Selecione o bairro")]
        [HiddenInput(DisplayValue=false)]
        [Display(Name="Bairro")]
        public int IdBairro { get; set; }

        [Required(ErrorMessage = "Selecione a cidade")]
        [Range(0, int.MaxValue, ErrorMessage = "Selecione a cidade")]
        [HiddenInput(DisplayValue = false)]
        [Display(Name="Cidade")]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Selecione o Estado")]
        [Range(0, int.MaxValue, ErrorMessage = "Selecione o Estado")]
        [HiddenInput(DisplayValue = false)]
        [Display(Name="Estado")]
        public int IdEstado { get; set; }

        [Required(ErrorMessage="Selecione o tipo de endereço")]
        [Range(0, int.MaxValue, ErrorMessage = "Selecione o tipo de endereço")]
        [HiddenInput(DisplayValue = false)]
        [Display(Name="Tipo endereço")]
        public int IdTipoEndereco { get; set; }

        [NotMapped]
        [Display(Name = "Bairro")]
        public virtual EnderecoBairro Bairro
        {
            get
            {
                return new BairroService().Find(IdBairro);
            }
        }

        [NotMapped]
        [Display(Name = "Cidade")]
        public virtual EnderecoCidade Cidade
        {
            get
            {
                return new CidadeService().Find(IdCidade);
            }
        }

        [NotMapped]
        [Display(Name = "Estado")]
        public virtual EnderecoEstado Estado
        {
            get
            {
                return new EstadoService().Find(IdEstado);
            }
        }

        [NotMapped]
        [Display(Name = "Tipo endereço")]
        public virtual EnderecoTipoEndereco TipoEndereco
        {
            get
            {
                return new TipoEnderecoService().Find(IdTipoEndereco);
            }
        }

        [NotMapped]
        [Display(Name = "Telefone")]
        public virtual EnderecoTelefone Telefone
        {
            get {
                return new TelefoneService().Listar().FirstOrDefault(x => x.IdEndereco == Id);
            }
        }

        [NotMapped]
        [Display(Name = "E-mail")]
        public virtual EnderecoEmail Email
        {
            get{
                return new EmailService().Listar().FirstOrDefault(x => x.IdEndereco == Id);
            }
        }
    }
}
