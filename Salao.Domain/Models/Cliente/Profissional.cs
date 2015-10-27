using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class Profissional
    {
        [Key]
        public int Id { get; set; }

        [HiddenInput]
        public int IdSalao { get; set; }

        [Required(ErrorMessage="Informe o nome do colaborador")]
        [StringLength(60,ErrorMessage="Máximo de 60 caracteres")]
        [Display(Name="Nome", Prompt="Nome do colaborador")]
        public string Nome { get; set; }

        [Required(ErrorMessage="Informe o telefone")]
        [StringLength(100, ErrorMessage="Máximo de 100 caracteres")]
        [Display(Name="Telefone", Prompt="9999-9999")]
        public string Telefone { get; set; }

        [Required(ErrorMessage="Informe o email do colaborador")]
        [StringLength(100,ErrorMessage="Máximo de 100 caracteres")]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email", Prompt="nome@dominio.com.br")]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        [NotMapped]
        [Display(Name="Salão")]
        public virtual Salao Salao 
        {
            get
            {
                return new Service.Cliente.SalaoService().Find(IdSalao);
            }
        }

        [NotMapped]
        [Display(Name="Empresa")]
        public virtual Empresa Empresa
        {
            get
            {
                return new Service.Cliente.EmpresaService().Find(Salao.IdEmpresa);
            }
        }
    }
}
