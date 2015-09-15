using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Salao.Web.Areas.Admin.Models
{
    public class GruposUsuario
    {
        public int Id { get; set; }
        
        [Display(Name="Grupo")]
        public string Descricao { get; set; }

        public bool Selecionado { get; set; }
    }
}