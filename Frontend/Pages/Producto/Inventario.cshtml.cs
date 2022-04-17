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
    public class InventarioProductoModel : PageModel
    {

        private readonly RepositorioProducto repositorioProducto;
        public IEnumerable<Dominio.Producto> Productos {get;set;}
        [BindProperty]
        public Dominio.Producto Producto {get;set;}
        [BindProperty]
        public int cont { get;set; }

        public InventarioProductoModel(RepositorioProducto repositorioProducto ) 
        {
            this.repositorioProducto = repositorioProducto;
        }

        public void OnGet()
        {
            Productos = repositorioProducto.GetAll();
        }
        
        public IActionResult OnPost() //Para Actualizar la cantidad de productos
        {
            if (Producto.id >= 0)
            {
               int cantidadPaqueteProductos = int.Parse(Request.Form[$"cantidadProductosPaquete_{cont}"]);
               int cantidadPaquetes = int.Parse(Request.Form[$"cantidadPaquetes_{cont}"]);
               int cantidadAnadir = cantidadPaquetes * cantidadPaqueteProductos;
               Producto = repositorioProducto.UpdateProductoWithCantidad(Producto.id, cantidadAnadir);
            }
            return RedirectToPage("./List");
        }  
    }
}