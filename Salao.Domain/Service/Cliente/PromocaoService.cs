using Salao.Domain.Abstract;
using Salao.Domain.Models.Admin;
using Salao.Domain.Models.Cliente;
using Salao.Domain.Service.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salao.Domain.Service.Cliente
{
    public class PromocaoService
    {
        IBaseService<SistemaParametro> service;
        private const string promoDesconto = "PROMO_DESCONTO";
        private const string promoCarencia = "PROMO_CARENCIA";
        
        SistemaParametro parDesconto;
        SistemaParametro parCarencia;
        
        public PromocaoService()
        {
            service = new SistemaParametroService();
            parDesconto = service.Listar().First(x => x.Codigo == promoDesconto);
            parCarencia = service.Listar().First(x => x.Codigo == promoCarencia);
        }

        public void Set(Promocao promocao)
        {
            parDesconto.Valor = promocao.Desconto.ToString();
            parCarencia.Valor = promocao.DescontoCarencia.ToString();
            service.Gravar(parDesconto);
            service.Gravar(parCarencia);
        }

        public Promocao Get()
        {
            return new Promocao
            {
                Desconto = Convert.ToDecimal(parDesconto.Valor),
                DescontoCarencia = Convert.ToInt32(parCarencia.Valor)
            };
        }
    }
}
