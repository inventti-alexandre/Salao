using System.Web.Mvc;

namespace Salao.Web.Areas.Admin
{
    public class ClienteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cliente";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CadastroEmpresa",
                "Empresa/Cadastro/{action}/{id}",
                new { Controller = "CadastroEmpresa", action = "Index", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Cliente_default",
                "Cliente/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}