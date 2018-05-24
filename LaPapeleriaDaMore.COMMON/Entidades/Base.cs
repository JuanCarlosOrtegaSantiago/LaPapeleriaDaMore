using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public abstract class Base
    {
        public ObjectId Id { get; set; }
        public string Nombre { get; set; }
        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
