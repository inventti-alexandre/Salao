using Salao.Domain.Models.Admin;
using Salao.Domain.Service.Admin;
using Salao.Domain.Service.Endereco;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Endereco
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Logradouro")]
        [StringLength(100, ErrorMessage = "O nome do logradouro é composto por no máximo 100 caracteres")]
        public string Logradouro { get; set; }

        [Required]
        [Display(Name = "Número")]
        [StringLength(10, ErrorMessage = "O número é composto por no máximo 10 caracteres")]
        public string Numero { get; set; }

        [StringLength(40, ErrorMessage="O complemento é composto por no máximo 40 caracteres")]
        public string Complemento { get; set; }

        [Required]
        [Display(Name = "CEP")]
        [StringLength(8, ErrorMessage = "O CEP é composto por 8 caracteres",MinimumLength=8)]
        public string Cep { get; set; }

        public bool Ativo { get; set; }

        [Required]
        public int AlteradoPor { get; set; }

        [Required]
        public DateTime AlteradoEm { get; set; }

        [Required(ErrorMessage = "Selecione o bairro")]
        [Range(0,9999999999,ErrorMessage="Selecione o bairro")]
        [HiddenInput(DisplayValue=false)]
        public int IdBairro { get; set; }

        [Required(ErrorMessage = "Selecione a cidade")]
        [Range(0, 9999999999, ErrorMessage = "Selecione a cidade")]
        [HiddenInput(DisplayValue = false)]
        public int IdCidade { get; set; }

        [Required(ErrorMessage = "Selecione o Estado")]
        [Range(0, 9999999999, ErrorMessage = "Selecione o Estado")]
        [HiddenInput(DisplayValue = false)]
        public int IdEstado { get; set; }

        [Required(ErrorMessage="Selecione o tipo de endereço")]
        [Range(0, 9999999999, ErrorMessage = "Selecione o tipo de endereço")]
        [HiddenInput(DisplayValue = false)]
        public int IdTipoEndereco { get; set; }

        [NotMapped]
        [Display(Name = "Usuario")]
        public virtual Usuario Usuario
        {
            get
            {
                return new UsuarioService().Find(AlteradoPor);
            }
        }

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
        [Display(Name = "Usuario")]
        public virtual EnderecoTipoEndereco TipoEndereco
        {
            get
            {
                return new TipoEnderecoService().Find(IdTipoEndereco);
            }
        }
    }
}
