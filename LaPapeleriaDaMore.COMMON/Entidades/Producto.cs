using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Producto:Base
    {
        public string Codigo { get; set; }
        public float PresioVenta { get; set; }
        public int Cantidad { get; set; }


    }
}