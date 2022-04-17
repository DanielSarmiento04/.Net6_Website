using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Ingrediente
    {
        public int id { get;set; }
        public string? imagen { get;set; }
        public string? nombre { get;set; }
        public int? precio { get;set; }
        public string? descripcion { get;set; }
        public int? cantidad { get;set; } // Para inventario
        public int? cantidadProvicional { get;set; } // Para pedido
        
        public override string ToString()
        {
            return $"{id} - {nombre}";
        }
    }
}