using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Web;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Persistencia.AppRepositorios;
using Dominio;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages
{
    [Authorize]
    public class EditPedidoModel : PageModel
    {

        private readonly RepositorioPedido repositorioPedido;

        private readonly RepositorioProducto repositorioProducto;
        public List<Dominio.Producto> Productos;
        [BindProperty]
        public Dominio.Pedido Pedido { get; set; }
        public int LenghtProductos; // para obtneer la informacion del formualario
        public int TotalPagar = 0;
        public List<int> idesProductos;
        private Dominio.Producto producto;

        public EditPedidoModel(RepositorioPedido repositorioPedido, RepositorioProducto repositorioProducto)
        {
            this.repositorioProducto = repositorioProducto;
            this.repositorioPedido = repositorioPedido;
            LenghtProductos = repositorioProducto.LengthProducts();  // Obtenemos la cantidad de productos
            idesProductos = repositorioProducto.GetAllId();  // Obtenemos todos los ides disponibles
        }

        public IActionResult OnGet(int PedidoId)
        {
            Pedido = repositorioPedido.GetPedidoWithId(PedidoId);
            return Page();
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if (!ModelState.IsValid) return Page();
            Productos = new List<Dominio.Producto>();
            string infoProductos = "";
            for (int i = 0; i < LenghtProductos; i++)
            {
                int Cantidad = int.Parse(Request.Form[$"cantidad_{i}"]);
                var productoId = int.Parse(Request.Form[$"id_{i}"]);
                producto = repositorioProducto.GetProductoWithId(productoId); // Traemos el producto
                if (i == 0) infoProductos = $"{producto.id}-{producto.nombre}-{producto.precio}-{Cantidad}";
                else infoProductos = $"{infoProductos},{producto.id}-{producto.nombre}-{producto.precio}-{Cantidad}";
                var porcentajeDescuento = (100 - (double)producto.descuento) / 100;
                TotalPagar += (int)(Cantidad * producto.precio * porcentajeDescuento);
                TotalPagar += (Cantidad * producto.precio);
                Productos.Add(producto);
            }

            if (Pedido.id >= 0)
            {
                Pedido.infoProductos = infoProductos;
                // Pedido.productos = Productos;
                Pedido.totalPagar = TotalPagar;
                Pedido = repositorioPedido.Update(Pedido);
            }
            return RedirectToPage("./List");
        }
    }
}