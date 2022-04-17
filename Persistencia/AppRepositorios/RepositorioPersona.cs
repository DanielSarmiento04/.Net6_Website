using System.Collections.Generic;
using Dominio;
using System.Linq;
using System;
using Persistencia.AppRepositorios;

namespace Persistencia.AppRepositorios
{
    public class RepositorioPersona
    {

        private readonly AppContext _appContext = new AppContext();
        List<Persona>? Personas;
        public RepositorioPersona()
        {

        }
        public Persona Create(Persona persona) // Creamos una persona 
        {
            var addPersona = _appContext.Personas.Add(persona);
            _appContext.SaveChanges();
            if (addPersona == null)
            {
                return null;
            }

            return addPersona.Entity;
        }

        public IEnumerable<Persona> GetAll()
        {
            return _appContext.Personas;
        }

        public Persona GetPersonaWithId(int id)
        {
            // return Personas.SingleOrDefault(p => p.id == id);
            return _appContext.Personas.Find(id);
        }

        public Persona Update(Persona newPersona)
        {
            var personaOld = GetPersonaWithId(newPersona.id);
            if (personaOld != null)
            {
                personaOld.id = newPersona.id;
                personaOld.nombre = newPersona.nombre;
                personaOld.cargo = newPersona.cargo;
            }

            _appContext.SaveChanges();
            return personaOld;
        }
        public Persona Delete(int id) // Eliminamos la personas dentro de la base de datos
        {
            var Persona = GetPersonaWithId(id);
            _appContext.Personas.Remove(Persona);
            _appContext.SaveChanges();
            return Persona;
        }
    }
}