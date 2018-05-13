using System;
using System.Collections.Generic;
using System.Text;

namespace LaPapeleriaDaMore.COMMON.Entidades
{
    public class Venta:Base
    {
        public List<Producto> productos { get; set; }
        public Cliente cliente { get; set; }
        public Empleado empleado { get; set; }
        public DateTime? FechaDeVenta { get; set; }

    }
}
