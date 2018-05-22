using LaPapeleriaDaMore.BIZ;
using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LaPapeleriaDaMore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LaPapeleriaDaMore.GUI.Escritorio.PuntoDeVentas
{
    /// <summary>
    /// Lógica de interacción para Venta.xaml
    /// </summary>
    public partial class Venta : Window
    {
        Empleado empleado;
        Sucursal sucursal;


        IManejadorDeProducto manejadorDeProducto;
        public Venta(Sucursal sucursal, Empleado empleado)
        {
            InitializeComponent();

            this.empleado = empleado;
            this.sucursal = sucursal;

            manejadorDeProducto = new ManejadorDeProducto(new RepositorioDeProducto());

            lblDeEmpleado.Content = string.Format("{0}", empleado.Nombre);
            lblDeSucursalCliente.Content = string.Format("{0}", sucursal.Nombre);
            lblFecha.Content = DateTime.Now.ToShortDateString();

            cmbClienteNuevo.Visibility = Visibility.Collapsed;
            cmbDeClienteFrecuente.Visibility = Visibility.Collapsed;
            cmbxProducto.ItemsSource = manejadorDeProducto.Listar.Where(e => e.sucursal.Id == sucursal.Id);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PuntoDeVentas pagina = new PuntoDeVentas(sucursal,empleado);
            pagina.Show();
            this.Close();
        }
    }
}
