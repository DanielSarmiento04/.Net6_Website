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
    public class ListProductoModel : PageModel
    {

        private readonly RepositorioProducto repositorioProducto;
        public IEnumerable<Dominio.Producto> Productos {get;set;}
        [BindProperty]
        public Dominio.Producto Producto {get;set;}

        public ListProductoModel(RepositorioProducto repositorioProducto ) 
        {
            this.repositorioProducto = repositorioProducto;
        }

        public void OnGet()
        {
            Productos = repositorioProducto.GetAll();
        }
        
        public IActionResult OnPost() //Para Eliminar el producto
        {
            if (Producto.id >= 0)
            {
                Producto = repositorioProducto.Delete(Producto.id);
            }
            return RedirectToPage("./List");
        }  
    }
}