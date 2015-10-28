using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Syntax;
using System.Web.Mvc;
using Salao.Domain.Abstract;
using Salao.Domain.Service.Admin;
using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Cliente;
using Salao.Domain.Abstract.Cliente;
using Salao.Domain.Service.Endereco;
using Salao.Domain.Models.Endereco;
using Salao.Domain.Abstract.Admin;

namespace Salao.Web.App_Start
{
    public class IocConfig
    {
        public static void ConfigurarDependencias()
        {
            // container
            IKernel kernel = new StandardKernel();

            // mapeamento - interfaces x classes
            kernel.Bind<IBaseService<Area>>().To<AreaService>();
            kernel.Bind<IBaseService<CliGrupo>>().To<CliGrupoService>();
            kernel.Bind<IBaseService<CliPermissao>>().To<CliPermissaoService>();
            kernel.Bind<IBaseService<CliUsuario>>().To<CliUsuarioService>();
            kernel.Bind<IBaseService<Grupo>>().To<GrupoService>();
            kernel.Bind<IBaseService<Salao.Domain.Models.Cliente.Empresa>>().To<EmpresaService>();
            kernel.Bind<IBaseService<EnderecoEstado>>().To<EstadoService>();
            kernel.Bind<IBaseService<FormaPgto>>().To<FormaPgtoService>();
            kernel.Bind<IBaseService<Permissao>>().To<PermissaoService>();
            kernel.Bind<IBaseService<PreContato>>().To<PreContatoService>();
            kernel.Bind<IBaseService<Profissional>>().To<ProfissionalService>();
            kernel.Bind<IBaseService<SistemaParametro>>().To<SistemaParametroService>();
            kernel.Bind<IBaseService<EnderecoTipoEndereco>>().To<TipoEnderecoService>();
            kernel.Bind<IBaseService<SubArea>>().To<SubAreaService>();
            kernel.Bind<IBaseService<Usuario>>().To<UsuarioService>();
            kernel.Bind<IProfissionalServico>().To<ProfissionalServicoService>();
            kernel.Bind<IBaseService<Salao.Domain.Models.Cliente.Salao>>().To<SalaoService>();
            kernel.Bind<IBaseService<Servico>>().To<ServicoService>();
            kernel.Bind<ICadastroEmpresa>().To<CadastroEmpresaService>();
            kernel.Bind<ICadastroSalao>().To<CadastroSalaoService>();
            kernel.Bind<Salao.Domain.Abstract.Cliente.IGrupoPermissao>().To<CliGrupoPermissaoService>();
            kernel.Bind<Salao.Domain.Abstract.Admin.IGrupoPermissao>().To<GrupoPermissaoService>();
            kernel.Bind<ICliUsuarioGrupo>().To<CliUsuarioGrupoService>();
            kernel.Bind<IUsuarioGrupo>().To<UsuarioGrupoService>();
            kernel.Bind<Salao.Domain.Abstract.Admin.ILogin>().To<UsuarioService>();
            kernel.Bind<ISalaoFormaPgto>().To<SalaoFormaPgtoService>();

            // registro do container
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        public NinjectDependencyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;    
        }

        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }

}