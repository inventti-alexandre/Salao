using Salao.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Salao.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<EFDbContext>(null);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported == true)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    try
                    {
                        // usuario autenticado
                        string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                        string roles = string.Empty;

                        // roles Usuario.tab
                        var usuario = new Salao.Domain.Service.Admin.UsuarioService().Listar().FirstOrDefault(x => x.Login == username);
                        if (usuario != null)
                        {
                            roles = usuario.Roles;
                        }
                        
                        // atribui roles a identidade Principal
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                          new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                    }
                    catch (Exception)
                    {
                        //somehting went wrong
                    }
                }
            }
        }
    }

    public class AreaAuthorizeAttribute : AuthorizeAttribute
{
    private readonly string area;

    public AreaAuthorizeAttribute(string area)
    {
        this.area = area;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        string loginUrl = "";

        if (area == "Admin")
        {
            loginUrl = "~/Admin/Login";
        }
        else if (area == "Empresa")
        {
            loginUrl = "~/Empresa/Login";
        }

        filterContext.Result = new RedirectResult(loginUrl + "?returnUrl=" + filterContext.HttpContext.Request.Url.PathAndQuery);
    }
}
}
