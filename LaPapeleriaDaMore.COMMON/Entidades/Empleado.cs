using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Empleado:Persona
    {
        public string Cargo { get; set; }
        public string Bonificaciones { get; set; }
        public float Sueldo { get; set; }
        public Sucursal sucursal { get; set; }
    }
}
