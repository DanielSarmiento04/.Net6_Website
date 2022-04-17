using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Dominio;
using Persistencia.AppRepositorios;
using Microsoft.AspNetCore.Authorization;

namespace Frontend.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public IEnumerable<Dominio.Producto> Productos {get;set;}
    public List<Dominio.Producto> ProductosVista {get;set;}
    private readonly RepositorioProducto repositorioProducto;

    public IndexModel(ILogger<IndexModel> logger,RepositorioProducto repositorioProducto)
    {
        _logger = logger;
        this.repositorioProducto = repositorioProducto;
        ProductosVista = new List<Dominio.Producto>();
    }

    public void OnGet()
    {
        Productos = repositorioProducto.GetAll();
    }
}