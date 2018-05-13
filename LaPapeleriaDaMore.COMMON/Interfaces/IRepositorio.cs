using LaPapeleriaDaMore.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Interfaces
{
    public interface IRepositorio<T> where T:Base
    {
        bool Crear(T entidad);
        bool Editar(T entidad);
        bool Eliminar(string id);
        List<T> Leer { get; }
    }
}
