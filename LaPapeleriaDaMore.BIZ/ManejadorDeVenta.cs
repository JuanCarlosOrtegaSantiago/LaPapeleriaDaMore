using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.BIZ
{
    public class ManejadorDeVenta : IManejadorDeVenta
    {
        IRepositorio<Ventas> repositorio;
        public ManejadorDeVenta(IRepositorio<Ventas> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Ventas> Listar => repositorio.Leer;

        public bool Agregar(Ventas entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Ventas BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Ventas entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
