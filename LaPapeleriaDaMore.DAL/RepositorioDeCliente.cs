using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaPapeleriaDaMore.DAL
{
    public class RepositorioDeCliente : IRepositorio<Cliente>
    {
        private string DBName = "LaPapeleriaDaMore.DB";
        private string TableName = "Clientes";
        public List<Cliente> Leer {
            get
            {
                List<Cliente> datos = new List<Cliente>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Cliente>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Crear(Cliente entidad)
        {
            try
            {
                entidad.Id = Guid.NewGuid().ToString();
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Cliente>(TableName);
                    colection.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public bool Editar(Cliente entidad)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var colection = db.GetCollection<Cliente>(TableName);
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
                    var colection = db.GetCollection<Cliente>(TableName);
                    r = colection.Delete(e => e.Id == id);
                }
                return r>0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
