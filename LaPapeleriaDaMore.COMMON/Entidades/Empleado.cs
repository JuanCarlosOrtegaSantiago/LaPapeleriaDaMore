using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Empleado:Persona
    {
        public string Cargo { get; set; }
        public float Sueldo { get; set; }
        public string Contrasena { get; set; }
        public byte[] Fotografia{ get; set; }
    }
}
