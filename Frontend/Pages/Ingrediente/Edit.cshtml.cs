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
    public class EditIngredienteModel : PageModel
    {
        private readonly RepositorioIngrediente repositorioIngrediente;
        [BindProperty]
        public Dominio.Ingrediente Ingrediente {get;set;}

        public EditIngredienteModel(RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioIngrediente = repositorioIngrediente;
        }

        public IActionResult OnGet(int IngredienteId)
        {
            Ingrediente = repositorioIngrediente.GetIngredienteWithId(IngredienteId);
            return Page();
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(Ingrediente.id >= 0)
            {

                Ingrediente = repositorioIngrediente.Update(Ingrediente);
            }
            return RedirectToPage("./List");
        }

    }
}   