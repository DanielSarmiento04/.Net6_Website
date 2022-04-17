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
    public class FormIngredienteModel : PageModel
    {
        private readonly RepositorioIngrediente repositorioIngrediente;
        [BindProperty]
        public Dominio.Ingrediente Ingrediente { get; set; }

        public FormIngredienteModel(RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioIngrediente = repositorioIngrediente;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if (!ModelState.IsValid)  return Page();
            Ingrediente = repositorioIngrediente.Create(Ingrediente);
            return RedirectToPage("./List");
        }
    }
}