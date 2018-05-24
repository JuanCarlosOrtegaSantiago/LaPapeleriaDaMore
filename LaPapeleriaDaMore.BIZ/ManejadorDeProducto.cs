using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.BIZ
{
    public class ManejadorDeProducto : IManejadorDeProducto
    {
        IRepositorio<Producto> repositorio;
        public ManejadorDeProducto(IRepositorio<Producto> repositorio)
        {
            this.repositorio = repositorio;
        }

        public List<Producto> Listar => repositorio.Leer;

        public bool Agregar(Producto entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Producto BuscarPorCodigo(string codigo)
        {
            return Listar.Where(e => e.Codigo == codigo).SingleOrDefault();
        }

        public Producto BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Producto entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
