using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Salao.Domain.Models.Admin
{
    public class Area
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a área do serviço")]
        [StringLength(20, ErrorMessage = "A área do serviço é formada por no máximo 20 caracteres")]
        [Display(Name = "Área do serviço", Prompt="Ex: cabelo", Description="Cabelo, Fitness, Massagem...")]
        public string Descricao { get; set; }

        [Display(Name="Ativo", Description="Se a área esta ou não ativa para exibição nas telas do sistema")]
        public bool Ativo { get; set; }

        [Required]
        [Display(Name = "Alterado por")]
        public int AlteradoPor { get; set; }

        [Required]
        [Display(Name = "Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name = "Usuário")]
        public virtual Usuario Usuario
        {
            get
            {
                return new Salao.Domain.Service.Admin.UsuarioService().Find(AlteradoPor);
            }
            set { }
        }
    }
}
