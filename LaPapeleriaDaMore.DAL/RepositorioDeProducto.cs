using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.DAL
{
    public class RepositorioDeProducto : IRepositorio<Producto>
    {
        private string DBName = @"C:\Bd\LaPapeleriaDaMore.DB";
        private string TableName = "Productos";
        public List<Producto> Leer
        {
            get
            {
                List<Producto> datos = new List<Producto>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Producto>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Producto entidad)
        {
            try
            {
                entidad.Id = Guid.NewGuid().ToString();
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Producto>(TableName);
                    colection.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Editar(Producto entidad)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Producto>(TableName);
                    colection.Update(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                int r;
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Producto>(TableName);
                    r = colection.Delete(e => e.Id == id);
                }
                return r > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
