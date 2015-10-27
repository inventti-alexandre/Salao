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
        [Display(Name = "Nome", Prompt="Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o nome do seu salão de beleza, spa, estabelecimento")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Nome do salão", Prompt="Nome do salão de beleza, spa...")]
        public string NomeSalao { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name = "Email", Prompt="nome@dominio.com.br", Description="Email para contato")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe ao menos 1 telefone para contato")]
        [StringLength(20, ErrorMessage = "Máximo de 60 caracteres")]
        [Display(Name="Telefone", Prompt="9999-9999",Description="Telefone para contato")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe a cidade do seu salão")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        [Display(Name="Cidade",Prompt="Ex: São Paulo")]
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
        [DataType(DataType.MultilineText)]
        public string Observ { get; set; }

        [Display(Name="Assinou conosco")]
        public bool Assinou { get; set; }

        [Display(Name="Contatar novamente")]
        public bool ContatarNovamente { get; set; }

        public bool Ativo { get; set; }

        [NotMapped]
        [Display(Name="Estado")]
        public virtual Salao.Domain.Models.Endereco.EnderecoEstado Estado
        {
            get
            {
                return new Salao.Domain.Service.Endereco.EstadoService().Find(IdEstado);
            }
        }

        [NotMapped]
        [Display(Name="Atendido por")]
        public virtual string AtendidoPorUsuario
        {
            get
            {
                if (AtendidoPor.HasValue)
                {
                    return new Salao.Domain.Service.Admin.UsuarioService().Find((int)AtendidoPor).Nome;                    
                }
                return string.Empty;
            }
        }
    }
}
