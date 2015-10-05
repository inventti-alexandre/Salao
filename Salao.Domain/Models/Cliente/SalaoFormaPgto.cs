using System;
using System.ComponentModel.DataAnnotations;

namespace Salao.Domain.Models.Cliente
{
    public class SalaoFormaPgto
    {
        [Key]
        public int IdSalao { get; set; }

        [Key]
        public int IdFormaPgto { get; set; }

        public bool Ativo { get; set; }

        public DateTime AlteradoEm { get; set; }
    }
}
