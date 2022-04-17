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
    public class DetailsIngredienteModel : PageModel
    {
        private readonly RepositorioIngrediente repositorioIngrediente;
        public Dominio.Ingrediente Ingrediente { get; set; }
        public DetailsIngredienteModel(RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioIngrediente = repositorioIngrediente;
        }
        public IActionResult OnGet(int IngredienteId)
        {
            Ingrediente = repositorioIngrediente.GetIngredienteWithId(IngredienteId);
            return Page();
        }
    }
}
