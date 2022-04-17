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
    public class FormPedidoModel : PageModel
    {
        private readonly RepositorioPedido repositorioPedido;
        private readonly RepositorioProducto repositorioProducto;
        public List<Dominio.Producto> Productos;
        [BindProperty]
        public Dominio.Pedido Pedido { get; set; }
        public IEnumerable<Dominio.Producto> listProductos { get; set; }
        public int TotalPagar = 0;
        private Dominio.Producto producto;

        public FormPedidoModel(RepositorioPedido repositorioPedido, RepositorioProducto repositorioProducto)
        {
            this.repositorioPedido = repositorioPedido;
            this.repositorioProducto = repositorioProducto;
            listProductos = repositorioProducto.GetAll();
        }

        public void OnGet()
        {
            // Productos = repositorioProducto.GetAll();
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if (!ModelState.IsValid) return Page();
            Productos = new List<Dominio.Producto>();
            string infoProductos = "";
            var LengthProductos = listProductos.Count();
            for (int i = 0; i < LengthProductos; i++)
            {
                int Cantidad = int.Parse(Request.Form[$"cantidad_{i}"]);
                var productoId = int.Parse(Request.Form[$"id_{i}"]);
                producto = repositorioProducto.GetProductoWithId(productoId); // Traemos el producto
                producto.cantidadProvicional = Cantidad;
                if (i== 0) infoProductos = $"{producto.id}-{producto.nombre}-{producto.precio}-{Cantidad}";// Guardamos una copia de seguridad
                else       infoProductos = $"{infoProductos},{producto.id}-{producto.nombre}-{producto.precio}-{Cantidad}";        
                var porcentajeDescuento = (100 - (double)producto.descuento) / 100;
                TotalPagar += (int)(Cantidad * producto.precio * porcentajeDescuento);
                Productos.Add(producto);
            }

            Pedido.infoProductos = infoProductos;
            Pedido.totalPagar = TotalPagar;
            Pedido = repositorioPedido.Create(Pedido, Productos);
            
            return RedirectToPage("./List");

        }
    }
}