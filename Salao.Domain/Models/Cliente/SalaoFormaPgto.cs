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

        public virtual Salao Salao
        {
            get
            {
                return new Service.Cliente.SalaoService().Find(IdSalao);
            }
        }

        public virtual Models.Admin.FormaPgto FormaPagamento
        {
            get
            {
                return new Service.Admin.FormaPgtoService().Find(IdFormaPgto);
            }
        }
    }
}
