using System;

namespace Dominio
{
    public class Persona
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public TiposCargo cargo { get; set; }

    }
}