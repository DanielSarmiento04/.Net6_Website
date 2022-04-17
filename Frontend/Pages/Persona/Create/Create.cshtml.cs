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
    public class FormPersonaModel : PageModel
    {
        private readonly RepositorioPersona repositorioPersona;
        [BindProperty]
        public Dominio.Persona Persona {get;set;}

        public FormPersonaModel(RepositorioPersona repositorioPersona)
        {
            this.repositorioPersona = repositorioPersona;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if(!ModelState.IsValid) return Page();

            Persona = repositorioPersona.Create(Persona);
            if (Persona != null)   return RedirectToPage("../List/List");
            
            return RedirectToPage("../Frontend/Pages/Error");            
        }
    }
}