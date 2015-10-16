using Salao.Domain.Service.Cliente;
using System.Linq;
using System.Web;

namespace Salao.Web.Areas.Empresa.Common
{
    public static class Identification
    {
        public static int IdEmpresa 
        {
            get
            {
                return new CliUsuarioService().Listar().FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name).IdEmpresa;  
            }
        }

        public static string Nome
        {
            get
            {
                return new CliUsuarioService().Listar().FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name).Nome;
            }
        }

        public static string EmpresaFantasia
        {
            get
            {
                return new CliUsuarioService().Listar().FirstOrDefault(x => x.Email == HttpContext.Current.User.Identity.Name).Empresa.Fantasia;
            }
        }
    }
}