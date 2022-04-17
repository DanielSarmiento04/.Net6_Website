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
    public class DetailsPersonaModel : PageModel
    {

        private readonly RepositorioPersona repositorioPersona;
        public Dominio.Persona Persona { get; set; }

        public DetailsPersonaModel(RepositorioPersona repositorioPersona)
        {
            this.repositorioPersona = repositorioPersona;
        }

        public IActionResult OnGet(int PersonaId)
        {
            Persona = repositorioPersona.GetPersonaWithId(PersonaId);
            return Page();
        }
    }
}