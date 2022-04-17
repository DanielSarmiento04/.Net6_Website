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
    public class ListIngredienteModel : PageModel
    {

        private readonly RepositorioIngrediente repositorioIngrediente;
        public IEnumerable<Dominio.Ingrediente> Ingredientes {get;set;}
        [BindProperty]
        public Dominio.Ingrediente Ingrediente {get;set;}

        public ListIngredienteModel(RepositorioIngrediente repositorioIngrediente ) 
        {
            this.repositorioIngrediente = repositorioIngrediente;
        }

        public void OnGet()
        {
            Ingredientes = repositorioIngrediente.GetAll();
        }
        
        public IActionResult OnPost() //Para Eliminar el producto
        {
            if (Ingrediente.id >= 0)
            {
                Ingrediente = repositorioIngrediente.Delete(Ingrediente.id);
            }
            return RedirectToPage("./List");
        }  
    }
}