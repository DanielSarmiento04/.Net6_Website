using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
using Persistencia.AppRepositorios;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages
{
    [Authorize]
    public class ListPedidoModel : PageModel
    {

        private readonly RepositorioPedido repositorioPedido;

        public IEnumerable<Dominio.Pedido> Pedidos { get; set; }
        [BindProperty]
        public Dominio.Pedido Pedido { get; set; }

        public ListPedidoModel(RepositorioPedido repositorioPedido)
        {
            this.repositorioPedido = repositorioPedido;
        }

        public void OnGet()
        {
            Pedidos = repositorioPedido.GetAll();
        }

        public IActionResult OnPost()
        {
            if (Pedido.id >= 0)
            {
                Pedido = repositorioPedido.Delete(Pedido.id);
            }
            return RedirectToPage("./List");
        }
    }
}