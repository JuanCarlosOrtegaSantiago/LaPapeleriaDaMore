using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
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

        public Producto BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Producto entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
