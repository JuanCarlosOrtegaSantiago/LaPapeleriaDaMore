using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.BIZ
{
    public class ManejadorDeSucursal : IManejadorDeSucursal
    {
        IRepositorio<Sucursal> repositorio;
        public ManejadorDeSucursal(IRepositorio<Sucursal> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Sucursal> Listar => repositorio.Leer;

        public bool Agregar(Sucursal entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Sucursal BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Sucursal entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
