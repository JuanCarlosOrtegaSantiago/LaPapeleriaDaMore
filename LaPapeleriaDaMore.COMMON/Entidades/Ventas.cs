using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Ventas:Base
    {
        public List<Producto> productos { get; set; }
        public Cliente cliente { get; set; }
        public Empleado empleado { get; set; }
        public float PresioVenta { get; set; }
        public DateTime? FechaDeVenta { get; set; }
        public Sucursal sucursal { get; set; }
    }
}
