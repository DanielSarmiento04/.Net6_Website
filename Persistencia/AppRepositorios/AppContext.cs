using Microsoft.EntityFrameworkCore;
using Dominio;

namespace Persistencia
{
    public class AppContext: DbContext{

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<Ingrediente> Ingredientes { get; set; }    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if(!optionsBuilder.IsConfigured){
                optionsBuilder.UseSqlServer("put here your credential for sql server");
            }
        }
    }

}
