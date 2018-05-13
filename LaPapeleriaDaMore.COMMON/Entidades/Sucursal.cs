using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Sucursal:Base
    {
        public string Direccion { get; set; }
        public Empleado Encargado { get; set; }
        public List<Empleado> Empleados { get; set; }
    }
}