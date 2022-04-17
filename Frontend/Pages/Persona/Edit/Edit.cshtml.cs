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
    public class EditPersonaModel : PageModel
    {
        private readonly RepositorioPersona repositorioPersona;
        [BindProperty]
        public Dominio.Persona Persona {get;set;}

        public EditPersonaModel(RepositorioPersona repositorioPersona)
        {
            this.repositorioPersona = repositorioPersona;
        }

        public IActionResult OnGet(int PersonaId)
        {
            Persona = repositorioPersona.GetPersonaWithId(PersonaId);
            return Page();
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            if(Persona.id >= 0)
            {
                Persona = repositorioPersona.Update(Persona);
            }
            return RedirectToPage("../List/List");
        }

    }
}