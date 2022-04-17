using System;
using System.ComponentModel.DataAnnotations;
namespace Dominio{
    public class Pedido{
        public int id { get;set; }
        public List<Producto>? productos { get;set; }
        public int? totalPagar { get;set; }
        public int? descuento { get;set; }
        public string? infoProductos  { get; set;}
    }
}