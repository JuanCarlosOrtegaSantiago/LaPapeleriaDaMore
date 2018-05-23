using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.DAL
{
    public class RepositorioDeVenta : IRepositorio<Ventas>
    {
        private string DBName = @"C:\Bd\LaPapeleriaDaMore.DB";
        private string TableName = "Ventas";
        public List<Ventas> Leer
        {
            get
            {
                List<Ventas> datos = new List<Ventas>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Ventas>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Ventas entidad)
        {
            try
            {
                entidad.Id = Guid.NewGuid().ToString();
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Ventas>(TableName);
                    colection.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Editar(Ventas entidad)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Ventas>(TableName);
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
                    var colection = db.GetCollection<Ventas>(TableName);
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
