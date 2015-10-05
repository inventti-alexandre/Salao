using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Salao.Domain.Models.Cliente
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [HiddenInput]
        [Range(1,int.MaxValue, ErrorMessage="Selecione a sub área")]
        public int IdSubArea { get; set; }

        [Required]
        [HiddenInput]
        [Range(1,int.MaxValue, ErrorMessage="Selecione o salão")]
        public int IdSalao { get; set; }

        [Required(ErrorMessage="Informe a descrição do serviço")]
        [Display(Name="Descrição")]
        [StringLength(60, ErrorMessage="Máximo de 60 caracteres")]
        public string Descricao { get; set; }

        [Required(ErrorMessage="Informe mais detalhes sobre o serviço")]
        [Display(Name="Detalhes")]
        public string Detalhe { get; set; }

        [Required(ErrorMessage="Informe o tempo para realização do serviço")]
        [Display(Name="Duração do serviço")]
        public DateTime Tempo { get; set; }

        [Required(ErrorMessage="Informe o preço sem desconto")]
        [Display(Name="Preço sem desconto")]
        public decimal PrecoSemDesconto { get; set; }

        [Required(ErrorMessage="Informe o preço final de venda")]
        [Display(Name="Preço final de venda")]
        public decimal Preco { get; set; }

        [Required]
        [Display(Name="Alterado em")]
        public DateTime AlteradoEm { get; set; }

        public bool Ativo { get; set; }
    }
}
