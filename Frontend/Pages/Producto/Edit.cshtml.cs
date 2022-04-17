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
    public class EditProductoModel : PageModel
    {
        private readonly RepositorioProducto repositorioProducto;
        private readonly RepositorioIngrediente repositorioIngrediente;
        public IEnumerable<Dominio.Ingrediente> ingredientes { get; set; }
        private List<Dominio.Ingrediente> listIngredientes;
        private Dominio.Ingrediente ingrediente;
        [BindProperty]
        public Dominio.Producto Producto {get;set;}

        public EditProductoModel(RepositorioProducto repositorioProducto, RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioProducto = repositorioProducto;
            this.repositorioIngrediente = repositorioIngrediente;
            ingredientes = repositorioIngrediente.GetAll();
        }

        public IActionResult OnGet(int ProductoId)
        {
            Producto = repositorioProducto.GetProductoWithId(ProductoId);
            return Page();
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if(!ModelState.IsValid) return Page();
            
            listIngredientes = new List<Dominio.Ingrediente>();
            string infoIngredientes = "";
            int lengthIngrediente = ingredientes.Count();
            for (int i = 0; i < lengthIngrediente; i++)
            {
                int Cantidad = int.Parse(Request.Form[$"cantidad_{i}"]);
                int ingredienteId = int.Parse(Request.Form[$"id_{i}"]);
                ingrediente = repositorioIngrediente.GetIngredienteWithId(ingredienteId); // Traemos el producto
                if (ingrediente != null)
                {
                    if (i== 0) infoIngredientes = $"{ingrediente.id}-{ingrediente.nombre}-{Cantidad}";// Guardamos una copia de seguridad
                    else       infoIngredientes = $"{infoIngredientes},{ingrediente.id}-{ingrediente.nombre}-{Cantidad}";        
                    listIngredientes.Add(ingrediente);   
                }
                else
                {
                    if (i== 0) infoIngredientes = $"{ingrediente.id}-Nombre no encontrado-{Cantidad}";// Guardamos una copia de seguridad
                    else       infoIngredientes = $"{infoIngredientes},{ingrediente.id}-Nombre no encontrado-{Cantidad}";        
                    listIngredientes.Add(ingrediente);   
                }
            }

            if (Producto.id >= 0)
            {
                Producto.ingredienteString = infoIngredientes;
                Producto = repositorioProducto.Update(Producto);
            }
            return RedirectToPage("./List");
        }

    }
}   