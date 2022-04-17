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
    public class DetailsProductoModel : PageModel
    {
        private readonly RepositorioProducto repositorioProducto;
        public Dominio.Producto Producto { get; set; }
        public DetailsProductoModel(RepositorioProducto repositorioProducto)
        {
            this.repositorioProducto = repositorioProducto;
        }
        public IActionResult OnGet(int ProductoId)
        {
            Producto = repositorioProducto.GetProductoWithId(ProductoId);
            return Page();
        }
    }
}
