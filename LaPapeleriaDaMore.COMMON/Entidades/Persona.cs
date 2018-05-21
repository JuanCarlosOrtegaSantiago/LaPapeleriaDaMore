using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public abstract class Persona:Base
    {
        public string Telefono { get; set; }
        public string Email { get; set; }
        public Sucursal sucursal { get; set; }
    }
}
