using System.Collections.Generic;
using Dominio;
using System.Linq;
using System;
using Persistencia.AppRepositorios;

namespace Persistencia.AppRepositorios
{
    public class RepositorioPedido
    {
        private readonly AppContext _appContext = new AppContext();

        public List<Producto> Productos;
        public RepositorioProducto repositorioProducto;
        public RepositorioIngrediente repositorioIngrediente;

        public RepositorioPedido(RepositorioProducto repositorioProducto, RepositorioIngrediente repositorioIngrediente)
        {
            this.repositorioProducto = repositorioProducto; //Traemos el repositorio
            this.repositorioIngrediente = repositorioIngrediente;
        }

        public IEnumerable<Pedido> GetAll()
        {
            var Pedidos = _appContext.Pedidos;
            foreach (var pedido in Pedidos)
            {
                Productos = new List<Producto>();
                var ArrayInfoProductos = pedido.infoProductos.Split(',');
                foreach (var infoProducto in ArrayInfoProductos)
                {
                    var atributosProducto = infoProducto.Split('-');
                    Producto producto = repositorioProducto.GetProductoWithId(int.Parse(atributosProducto[0]));
                    // Console.WriteLine(producto.ToString());
                    if (producto == null)
                    {
                        producto = new Producto();
                        producto.nombre = atributosProducto[1];
                        producto.cantidad = int.Parse(atributosProducto[3]);
                        producto.precio = int.Parse(atributosProducto[2]);
                    }

                    Productos.Add(producto);
                }
                pedido.productos = Productos;
            }

            return Pedidos;

        }
        public Pedido GetPedidoWithId(int id)
        {
            return _appContext.Pedidos.Where(p => p.id == id).FirstOrDefault();
        }

        public Pedido Update(Pedido pedido)  //Actualiza el pedido
        {
            var PedidoOld = GetPedidoWithId(pedido.id);
            if (PedidoOld != null)
            {
                PedidoOld.id = pedido.id;
                PedidoOld.productos = pedido.productos;
                PedidoOld.totalPagar = pedido.totalPagar;
                PedidoOld.descuento = pedido.descuento;
                PedidoOld.infoProductos = pedido.infoProductos;
            }
            _appContext.SaveChanges();
            return PedidoOld;
        }
        public Pedido Create(Pedido Pedido) // Creamos un Pedido traido de Frontend
        {
            _appContext.Pedidos.Add(Pedido);
            _appContext.SaveChanges();
            return Pedido;
        }
        public Pedido Create(Pedido Pedido,List<Producto> Productos) // Creamos un Pedido traido de Frontend
        {
            foreach (var producto in Productos)
            {
                if (producto.cantidadProvicional > 0)
                {
                    var infoIngredientes = producto.ingredienteString.Split(','); // List product
                    foreach (var infoIngrediente in infoIngredientes)
                    { 
                        Console.WriteLine("=======");
                        Console.WriteLine(infoIngrediente);// Aa llega correcto
                        var atributosIngrediente = infoIngrediente.Split('-'); // [id] [nombre] [cantidadEnProducto]
                        if(atributosIngrediente[2] !="0") 
                        {
                            Ingrediente ingrediente = repositorioIngrediente.DecreaseCantidadwithIdCantidad(int.Parse(atributosIngrediente[0]), int.Parse(atributosIngrediente[2])); //Envio la actualizacion para los ingredientes incluidos en el producto del pedido
                        }
                    }
                }
            }
            _appContext.Pedidos.Add(Pedido);
            _appContext.SaveChanges();
            return Pedido;
        }
        public Pedido Delete(int id)
        {
            var Pedido = GetPedidoWithId(id);
            _appContext.Pedidos.Remove(Pedido);
            _appContext.SaveChanges();
            return Pedido;
        }
        public Pedido GetCantidadIngredientes(Pedido Pedido)
        {
            var ArrayInfoProductos = Pedido.infoProductos.Split(',');   //Separamos los productos
            foreach (var infoProducto in ArrayInfoProductos)
            {
                var atributosProducto = infoProducto.Split('-');     //Separamos los ingredientes de cada producto [id] [nombre] [cantidadEnProducto]
                Producto producto = repositorioProducto.GetProductoWithId(int.Parse(atributosProducto[0]));
                if (producto == null)
                {
                    producto = new Producto();
                    producto.nombre = atributosProducto[1];
                    producto.cantidad = int.Parse(atributosProducto[3]);
                    producto.precio = int.Parse(atributosProducto[2]);
                }
                Pedido.productos.Add(producto);
            }
            return Pedido;
        }

    }
}