using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Models.Cliente
{
    public class Salao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Range(0,999999999999,ErrorMessage="Selecione a empresa")]
        public int IdEmpresa { get; set; }

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

        public string Sobre { get; set; }

        [Display(Name = "Observações")]
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        [Required]
        [Display(Name = "Cadastrado em")]
        public DateTime CadastradoEm { get; set; }

        public bool Cortesia { get; set; }

        public decimal Desconto { get; set; }

        [Display(Name = "Carência (meses)")]
        public int DescontoCarencia { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [Display(Name = "Alterado por")]
        public int? AlteradoPor { get; set; }
    }
}
