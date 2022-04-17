using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistencia.AppRepositorios;
using Dominio;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages
{
    [Authorize]
    public class DetailsPedidoModel : PageModel
    {
        private readonly RepositorioPedido repositorioPedido;
        public Dominio.Pedido? Pedido {get;set;}
        public DetailsPedidoModel ( RepositorioPedido repositorioPedido )
        {
            this.repositorioPedido = repositorioPedido;
        }
        public IActionResult OnGet(int PedidoId)
        {
            Pedido = repositorioPedido.GetPedidoWithId(PedidoId);
            return Page();
        }
    }
}
