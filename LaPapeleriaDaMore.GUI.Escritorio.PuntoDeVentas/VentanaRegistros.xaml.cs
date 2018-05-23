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
    /// Lógica de interacción para VentanaRegistros.xaml
    /// </summary>
    public partial class VentanaRegistros : Window
    {
        Sucursal sucursal;
        Empleado empleado;
        IManejadorDeVenta manejadorDeVenta;
        public VentanaRegistros(Sucursal sucursal, Empleado empleado)
        {
            InitializeComponent();

            this.empleado = empleado;
            this.sucursal = sucursal;
            manejadorDeVenta = new ManejadorDeVenta(new RepositorioDeVenta());
            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            lstvVentas.ItemsSource = null;
            lstvVentas.ItemsSource = manejadorDeVenta.Listar.Where(e=>e.sucursal.Id==sucursal.Id).ToList();
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagina = new MainWindow();
            pagina.Show();
            this.Close();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventas = lstvVentas.SelectedItem as Ventas;
            if (manejadorDeVenta.Eliminar(ventas.Id))
            {
                MessageBox.Show("Venta eliminada correctamente","Elemento eliminado",MessageBoxButton.OK,MessageBoxImage.Information,MessageBoxResult.None,MessageBoxOptions.ServiceNotification);
                ActualizarTabla();
            }
            else
            {
                MessageBox.Show("La Venta no se elimino", "Error al eliminar", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
            }

        }
    }
}
