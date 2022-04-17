using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Producto
    {
        public int id { get;set; }
        [Required]
        public int precio { get;set; }
        public string? imagen { get;set; }
        public string? nombre { get;set; }
        public string? descripcion { get;set; }
        public TiposDesicion? desicion { get;set; }
        public int? cantidad { get;set; } // Para inventario
        public int? descuento { get;set; }
        public int? cantidadProvicional { get;set; } // Para pedido
        public List<Ingrediente>? ingredientes { get;set; }
        public string? ingredienteString { get;set; }
        
        public override string ToString()
        {
            return $"{id} - {nombre} - {precio} - {cantidad} - {descuento}";
        }
    }
}