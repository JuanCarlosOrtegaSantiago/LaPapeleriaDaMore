using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.BIZ
{
    public class ManejadorDeCliente : IMAnejadorDeCliente
    {
        IRepositorio<Cliente> repositorio;
        public ManejadorDeCliente(IRepositorio<Cliente> repo)
        {
            repositorio = repo;
        }
        public List<Cliente> Listar => repositorio.Leer;

        public bool Agregar(Cliente entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Cliente BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Cliente entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
