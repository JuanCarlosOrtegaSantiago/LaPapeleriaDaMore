using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.DAL
{
    public class RepositorioGenerico<T> : IRepositorio<T> where T : Base
    {
        private MongoClient client;
        private IMongoDatabase db;

        public RepositorioGenerico()
        {
            client = new MongoClient(new MongoUrl("mongodb://JuanCarlosO:17021020@ds133360.mlab.com:33360/lapapeleriadamore"));
            db = client.GetDatabase("lapapeleriadamore");
        }
        private IMongoCollection<T> Collection()
        {
            return db.GetCollection<T>(typeof(T).Name);
        }
        public List<T> Leer => Collection().AsQueryable().ToList();

        public bool Crear(T entidad)
        {
            try
            {
                Collection().InsertOne(entidad);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Editar(T entidad)
        {
            try
            {
                return Collection().ReplaceOne(p => p.Id == entidad.Id, entidad).ModifiedCount == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(ObjectId id)
        {
            try
            {
                return Collection().DeleteOne(p => p.Id == id).DeletedCount == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
