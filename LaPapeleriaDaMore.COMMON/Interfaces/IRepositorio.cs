using LaPapeleriaDaMore.COMMON.Entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Interfaces
{
    public interface IRepositorio<T> where T:Base
    {
        bool Crear(T entidad);
        bool Editar(T entidad);
        bool Eliminar(ObjectId id);
        List<T> Leer { get; }
    }
}
