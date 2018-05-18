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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaPapeleriaDaMore.GUI.Escritorio.Administrador
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IManejadorDeSucursal manejadorDeSucursal;
        IManejadorDeEmpleado manejadorDeEmpleado;
        public MainWindow()
        {
            InitializeComponent();

            manejadorDeSucursal = new ManejadorDeSucursal(new RepositorioDeSucursal());
            manejadorDeEmpleado = new ManejadorDeEmpleado(new RepositorioDeEmpleado());


            lblError.Visibility = Visibility.Collapsed;

            cmbxSucursal.ItemsSource = null;
            cmbxSucursal.ItemsSource = manejadorDeSucursal.Listar;

            cmbxEncargado.ItemsSource = null;
            cmbxEncargado.ItemsSource = manejadorDeEmpleado.Listar;
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Realmente deseas salir", "Salir", MessageBoxButton.YesNo, MessageBoxImage.Exclamation,MessageBoxResult.None,MessageBoxOptions.ServiceNotification) == MessageBoxResult.Yes)
                this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if(cmbxEncargado.SelectedItem!=null && cmbxSucursal.SelectedItem != null)
            {
                Empleado empleado = cmbxEncargado.SelectedItem as Empleado;
                Sucursal sucursal = cmbxSucursal.SelectedItem as Sucursal;
                if (sucursal.Encargado.Id == empleado.Id)
                {
                    IngresoAlSistema pagina = new IngresoAlSistema(empleado, sucursal);
                    this.Close();
                    pagina.Show();
                }
                else
                {
                    MessageBox.Show("Tu no eres gerente de "+sucursal.Nombre, "Error", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }
                
            }
            else
            {
                lblError.Visibility = Visibility.Visible;
            }
            
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftCtrl)
            {
                Sucursales pagina = new Sucursales();
                pagina.Show();
                this.Close();
            }
        }
    }
}
