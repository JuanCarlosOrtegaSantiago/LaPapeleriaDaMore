using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.DAL
{
    public class RepositorioDeEmpleado : IRepositorio<Empleado>
    {
        private string DBName = "LaPapeleriaDaMore.DB";
        private string TableName = "Empleados";
        public List<Empleado> Leer
        {
            get
            {
                List<Empleado> datos = new List<Empleado>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Empleado>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Empleado entidad)
        {
            try
            {
                entidad.Id = Guid.NewGuid().ToString();
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Empleado>(TableName);
                    colection.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Editar(Empleado entidad)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Empleado>(TableName);
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
                    var colection = db.GetCollection<Empleado>(TableName);
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
