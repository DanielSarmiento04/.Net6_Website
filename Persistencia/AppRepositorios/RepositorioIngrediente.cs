using System.Collections.Generic;
using Dominio;
using System.Linq;
using System;

namespace Persistencia.AppRepositorios
{
    public class RepositorioIngrediente
    {
        private readonly AppContext _appContext = new AppContext();
        List<Ingrediente> Ingredientes;

        public RepositorioIngrediente()
        {
            
        }
        public IEnumerable<Ingrediente> GetAll()  // Obtener todos los Pngredientes
        {
            return _appContext.Ingredientes;
        }

        public Ingrediente GetIngredienteWithId(int id) // Obtener el pngrediente con el id
        { 
            try
            {
                return _appContext.Ingredientes.Find(id);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
        public Ingrediente Update(Ingrediente ingrediente)    //Actualiza el Pngrediente 
        { 
            var ingredienteDataBase = GetIngredienteWithId(ingrediente.id); // Buscamos el ingrediente en la base de datos
            ingredienteDataBase.precio = ingrediente.precio;
            ingredienteDataBase.imagen = ingrediente.imagen;
            ingredienteDataBase.nombre = ingrediente.nombre;
            ingredienteDataBase.cantidad = ingrediente.cantidad;
            ingredienteDataBase.descripcion = ingrediente.descripcion;
            ingredienteDataBase.cantidadProvicional = ingrediente.cantidadProvicional;
            _appContext.SaveChanges();
            return ingredienteDataBase;
        }
        public Ingrediente Create(Ingrediente ingrediente) // Creamos un ingrediente traido de Frontend
        {  
            var addIngrediente = _appContext.Ingredientes.Add(ingrediente);
            _appContext.SaveChanges();
            if (addIngrediente == null)
            {
                return null  ;              
            }
            return addIngrediente.Entity;
        }
        public Ingrediente Delete(int id)
        {
            var Ingrediente = GetIngredienteWithId(id);
            _appContext.Ingredientes.Remove(Ingrediente);
            _appContext.SaveChanges();
            return Ingrediente;
        }
        public int LengthIngredients()
        {  // Obtenesmos la cantidad de pedidos totales
            return  GetAll().Count();
        }

        public List<int> GetAllId()
        {
            return GetAll().Select(p => p.id).ToList();
        }
        public Ingrediente UpdateIngredienteWithCantidad(int id,int cantidad) // Axtualiza la cantidad del pngrediente incrementando la cantidad del produccto con cantidad
        {
            var ingrediente = GetIngredienteWithId(id);
            ingrediente.cantidad += cantidad;
            _appContext.SaveChanges();
            return ingrediente;
        }  
        public IEnumerable<Ingrediente> UpdateCatidadIngredienteWithZipList(IEnumerable<int> Cantidades,IEnumerable<Ingrediente> Ingredientes) 
        {
            //This methos has a list with Ingredientes and other with a number to add in cantidad from Ingrediente
            //Use method Zip method for combinate the both and with a foreach increase in the initial cantidad
            var combinateList = Ingredientes.Zip(Cantidades, (Ingrediente, cantidad) => new { Ingrediente, cantidad });
            foreach (var item in combinateList)
            {
                var ingrediente = GetIngredienteWithId(item.Ingrediente.id);
                ingrediente.cantidad += item.cantidad;
            }
            _appContext.SaveChanges();
            return Ingredientes;
        }     
        public Ingrediente DecreaseCantidadwithIdCantidad(int id,int cantidad) // Axtualiza la cantidad del pngrediente incrementando la cantidad del produccto con cantidad
        {
            var ingrediente = GetIngredienteWithId(id);
            if (ingrediente!=null)
            {
                ingrediente.cantidad -= cantidad;
                _appContext.SaveChanges();
                return ingrediente;
            }
            return null;
        }  

    }
}
