using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salao.Domain.Models.Admin
{
    public class PreContato
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe seu nome")]
        [StringLength(1000, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Sub área do serviço")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome do seu salão de beleza, spa, estabelecimento")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Nome do salão")]
        public string NomeSalao { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe ao menos 1 telefone para contato")]
        [StringLength(20, ErrorMessage = "Máximo de 60 caracteres")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe a cidade do seu salão")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Cidade { get; set; }

        [Required(ErrorMessage="Selecione o Estado")]
        [Display(Name="Estado")]
        public int IdEstado { get; set; }

        [Required]
        [Display(Name="Contato em")]
        public DateTime ContatoEm { get; set; }

        [Display(Name="Atendido")]
        public bool Atendido { get; set; }

        [Display(Name="Atendido por")]
        public int? AtendidoPor { get; set; }

        [Display(Name="Observações")]
        public string Observ { get; set; }

        [Display(Name="Assinou conosco")]
        public bool Assinou { get; set; }

        [Display(Name="Contatar novamente")]
        public bool ContatarNovamente { get; set; }

        [NotMapped]
        [Display(Name="Estado")]
        public virtual Salao.Domain.Models.Endereco.EnderecoEstado Estado
        {
            get
            {
                return new Salao.Domain.Service.Endereco.EstadoService().Find(IdEstado);
            }
        }
    }
}
