using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Cliente:Persona
    {
        public List<Producto> productos{ get; set; }
        public string RFC { get; set; }
    }
}
