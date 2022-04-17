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
    public class FormProductoModel : PageModel
    {
        private readonly RepositorioProducto repositorioProducto;
        private readonly RepositorioIngrediente repositorioIngrediente;
        public IEnumerable<Dominio.Ingrediente> ingredientes { get; set; }
        private List<Dominio.Ingrediente> listIngredientes;
        private List<int> idesIngredientes;
        private Dominio.Ingrediente ingrediente;
        [BindProperty]
        public Dominio.Producto Producto { get; set; }

        public FormProductoModel(RepositorioProducto repositorioProducto, RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioProducto = repositorioProducto;
            this.repositorioIngrediente = repositorioIngrediente;
            idesIngredientes = repositorioIngrediente.GetAllId();
            ingrediente = new Dominio.Ingrediente();
            ingredientes = repositorioIngrediente.GetAll();
        }

        public void OnGet()
        {
            ingredientes = repositorioIngrediente.GetAll();
        }

        public IActionResult OnPost()  //Para ACtualizar en data base
        {
            if (!ModelState.IsValid) return Page();
            listIngredientes = new List<Dominio.Ingrediente>();
            string infoIngredientes = "";
            int lengthIngrediente = ingredientes.Count();
            for (int i = 0; i < lengthIngrediente; i++)
            {
                int Cantidad = int.Parse(Request.Form[$"cantidad_{i}"]);
                int ingredienteId = int.Parse(Request.Form[$"id_{i}"]);
                ingrediente = repositorioIngrediente.GetIngredienteWithId(ingredienteId); // Traemos el producto
                if (i== 0) infoIngredientes = $"{ingredienteId}-{ingrediente.nombre}-{Cantidad}";// Guardamos una copia de seguridad  ID-NOMBRE -CANTIDAD DE INGREDIENTE EN EL PRODUCTO
                else       infoIngredientes = $"{infoIngredientes},{ingredienteId}-{ingrediente.nombre}-{Cantidad}"; 
                listIngredientes.Add(ingrediente);
            }

            if (Producto.id >= 0)
            {
                Producto.ingredienteString = infoIngredientes;
                Producto = repositorioProducto.Create(Producto);
            }
            return RedirectToPage("./List");
        }
    }
}