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
    public class InventarioIngredienteModel : PageModel
    {

        private readonly RepositorioIngrediente repositorioIngrediente;
        public IEnumerable<Dominio.Ingrediente> Ingredientes {get;set;}
        [BindProperty]
        public Dominio.Ingrediente Ingrediente {get;set;}
        [BindProperty]
        public int cont { get;set; }

        public InventarioIngredienteModel(RepositorioIngrediente repositorioIngrediente ) 
        {
            this.repositorioIngrediente = repositorioIngrediente;
        }

        public void OnGet()
        {
            Ingredientes = repositorioIngrediente.GetAll();
        }
        
        public IActionResult OnPost() //Para Actualizar la cantidad de Ingredientes
        {
            if (Ingrediente.id >= 0)
            {
               int cantidadPaqueteProductos = int.Parse(Request.Form[$"cantidadIngredientesPaquete_{cont}"]);
               int cantidadPaquetes = int.Parse(Request.Form[$"cantidadPaquetes_{cont}"]);
               int cantidadAnadir = cantidadPaquetes * cantidadPaqueteProductos;
               Ingrediente = repositorioIngrediente.UpdateIngredienteWithCantidad(Ingrediente.id, cantidadAnadir);
            }
            return RedirectToPage("./List");
        }  
    }
}