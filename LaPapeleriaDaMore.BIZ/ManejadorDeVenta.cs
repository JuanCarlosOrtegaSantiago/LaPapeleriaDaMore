using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.BIZ
{
    public class ManejadorDeVenta : IManejadorDeVenta
    {
        IRepositorio<Venta> repositorio;
        public ManejadorDeVenta(IRepositorio<Venta> repositorio)
        {
            this.repositorio = repositorio;
        }
        public List<Venta> Listar => repositorio.Leer;

        public bool Agregar(Venta entidad)
        {
            return repositorio.Crear(entidad);
        }

        public Venta BuscarPorId(string id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(string id)
        {
            return repositorio.Eliminar(id);
        }

        public bool Modificar(Venta entidad)
        {
            return repositorio.Editar(entidad);
        }
    }
}
