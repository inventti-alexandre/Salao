using System.Web.Mvc;

namespace Salao.Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin",
                "Admin",
                new { Controller = "Home", action = "Index" }
            );

            context.MapRoute(
                "TrocarSenha",
                "TrocarSenha",
                new { Controller = "Usuario", action = "TrocarSenha" }
            );

            context.MapRoute(
                "GetSubAreas",
                "Admin/SubArea/GetSubAreas/{idArea}",
                new { Controller ="SubArea", action = "GetSubAreas", idArea = 0 }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}