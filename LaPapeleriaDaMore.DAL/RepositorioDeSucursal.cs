using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.DAL
{
    public class RepositorioDeSucursal : IRepositorio<Sucursal>
    {
        private string DBName = @"C:\Bd\LaPapeleriaDaMore.DB";
        private string TableName = "Sucursales";
        public List<Sucursal> Leer
        {
            get
            {
                List<Sucursal> datos = new List<Sucursal>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Sucursal>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Sucursal entidad)
        {
            try
            {
                entidad.Id = Guid.NewGuid().ToString();
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Sucursal>(TableName);
                    colection.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Editar(Sucursal entidad)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Sucursal>(TableName);
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
                    var colection = db.GetCollection<Sucursal>(TableName);
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
