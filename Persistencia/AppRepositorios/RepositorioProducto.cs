using System.Collections.Generic;
using Dominio;
using System.Linq;
using System;

namespace Persistencia.AppRepositorios
{
    public class RepositorioProducto
    {
        private readonly AppContext _appContext = new AppContext();
        List<Ingrediente> Ingredientes;
        private readonly RepositorioIngrediente repositorioIngrediente;

        public RepositorioProducto(RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioIngrediente = repositorioIngrediente; //Traemos el repositorio   
        }
        public IEnumerable<Producto> GetAll()  // Obtener todos los PRoductos
        {
            var Productos = _appContext.Productos;

            foreach (var producto in Productos)
            {
                Ingredientes = new List<Ingrediente>();
                var ArrayInfoIngredientes = producto.ingredienteString.Split(",");
                foreach (var infoIngrediente in ArrayInfoIngredientes)
                {
                    var atributosIngrediente = infoIngrediente.Split("-");
                    Ingrediente ingrediente = repositorioIngrediente.GetIngredienteWithId(int.Parse(atributosIngrediente[0]));
                    if (ingrediente == null)
                    {
                        ingrediente = new Ingrediente();
                        ingrediente.id = int.Parse(atributosIngrediente[0]);
                        ingrediente.nombre = atributosIngrediente[1];
                        ingrediente.cantidadProvicional = int.Parse(atributosIngrediente[2]);
                    }
                    Ingredientes.Add(ingrediente);
                }
                producto.ingredientes = Ingredientes;
            }
            
            return Productos;
        }

        public Producto GetProductoWithId(int id) // Obtener el producto con el id
        {
            try
            {
                return _appContext.Productos.Where(p => p.id == id).FirstOrDefault();
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        public Producto Update(Producto producto)    //Actualiza el Producto 
        {
            var productoDataBase = GetProductoWithId(producto.id); // Buscamos el producto en la base de datos

            productoDataBase.precio = producto.precio;
            productoDataBase.imagen = producto.imagen;
            productoDataBase.nombre = producto.nombre;
            productoDataBase.cantidad = producto.cantidad;
            productoDataBase.descuento = producto.descuento;
            productoDataBase.desicion = producto.desicion;
            productoDataBase.descripcion = producto.descripcion;
            productoDataBase.cantidadProvicional = producto.cantidadProvicional;
            productoDataBase.ingredientes = producto.ingredientes;
            productoDataBase.ingredienteString = producto.ingredienteString;
            _appContext.SaveChanges();
            return productoDataBase;
        }
        public Producto Create(Producto producto) // Creamos un producto traido de Frontend
        {
            var addProducto = _appContext.Productos.Add(producto);
            _appContext.SaveChanges();
            if (addProducto == null)
            {
                return null;
            }

            return addProducto.Entity;
        }
        public Producto Delete(int id)
        {
            var Producto = GetProductoWithId(id);
            _appContext.Productos.Remove(Producto);
            _appContext.SaveChanges();
            return Producto;
        }
        public int LengthProducts()
        {  // Obtenesmos la cantidad de pedidos totales
            return GetAll().Count();
        }

        public List<int> GetAllId()
        {
            return GetAll().Select(p => p.id).ToList();
        }
        public Producto UpdateProductoWithCantidad(int id, int cantidad) // Axtualiza la cantidad del producto incrementando la cantidad del produccto con cantidad
        {
            var producto = GetProductoWithId(id);
            producto.cantidad += cantidad;
            _appContext.SaveChanges();
            return producto;
        }
    }
}
