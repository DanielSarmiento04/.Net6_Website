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
    public class ListPersonaModel : PageModel
    {

        private readonly RepositorioPersona repositorioPersona;
        public IEnumerable<Dominio.Persona> Personas {get;set;}
        [BindProperty]
        public Dominio.Persona Persona {get;set;}

        public ListPersonaModel(RepositorioPersona repositorioPersona ) 
        {
            this.repositorioPersona = repositorioPersona;
        }


        public void OnGet()
        {
            Personas = repositorioPersona.GetAll();
        }
        
        public IActionResult OnPost() //Para Eliminar el persona
        {
            if (Persona.id >= 0)
            {
                Persona = repositorioPersona.Delete(Persona.id);
            }
            return RedirectToPage("../List/List");
        }  
    }
}