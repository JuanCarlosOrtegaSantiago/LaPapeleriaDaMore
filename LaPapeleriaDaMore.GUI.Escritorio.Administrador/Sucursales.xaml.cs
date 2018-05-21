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

namespace LaPapeleriaDaMore.GUI.Escritorio.Administrador
{
    /// <summary>
    /// Lógica de interacción para Sucursales.xaml
    /// </summary>
    public partial class Sucursales : Window
    {
        enum accion
        {
            nuevo,
            editar
        }
        accion accionDeSucursal;

        IManejadorDeSucursal manejadorDeSucursal;
        IManejadorDeEmpleado manejadorDeEmpleado;
        public Sucursales()
        {
            InitializeComponent();

            manejadorDeSucursal = new ManejadorDeSucursal(new RepositorioDeSucursal());
            manejadorDeEmpleado = new ManejadorDeEmpleado(new RepositorioDeEmpleado());


            PanelDeDatos.IsEnabled = false;
            BotonesHabilitados(false);
            ActualizarTablaSucursales();
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            tbxDireccion.Clear();
            tbxEmailDeEncargado.Clear();
            tbxNombreEncargado.Clear();
            tbxNombreSucursal.Clear();
            tbxTelefonoEncargado.Clear();
            tbxContrasenaEncargado.Clear();
        }

        private void ActualizarTablaSucursales()
        {
            lstvSucursales.ItemsSource = null;
            lstvSucursales.ItemsSource = manejadorDeSucursal.Listar;
        }

        private void BotonesHabilitados(bool Habilitados)
        {
            btnEditar.IsEnabled = !Habilitados;
            btnEliminar.IsEnabled = !Habilitados;
            btnGuardar.IsEnabled = Habilitados;
            btnNuevo.IsEnabled = !Habilitados;
            btnRegresar.IsEnabled = !Habilitados;
            btnCancelar.IsEnabled = Habilitados;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagina = new MainWindow();
            pagina.Show();
            this.Close();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            PanelDeDatos.IsEnabled = true;
            BotonesHabilitados(true);
            accionDeSucursal = accion.nuevo;
            lstvSucursales.IsEnabled = false;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            PanelDeDatos.IsEnabled = false;
            BotonesHabilitados(false);
            lstvSucursales.IsEnabled = true;
            LimpiarCampos();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            Sucursal sucursal = lstvSucursales.SelectedItem as Sucursal;
            if (manejadorDeSucursal.Listar.Count > 0) 
            {
                if (sucursal != null) 
                {

                    tbxDireccion.Text = sucursal.Direccion;
                    tbxNombreSucursal.Text = sucursal.Nombre;

                    tbxTelefonoEncargado.Text = sucursal.Encargado.Telefono;
                    tbxNombreEncargado.Text = sucursal.Encargado.Nombre;
                    tbxEmailDeEncargado.Text = sucursal.Encargado.Email;
                    tbxContrasenaEncargado.Text = sucursal.Encargado.Contrasena;

                    BotonesHabilitados(true);
                    PanelDeDatos.IsEnabled = true;
                    accionDeSucursal = accion.editar;
                    lstvSucursales.IsEnabled = false;
                }
                else
                {
                    MessageBox.Show("No has seleccionado nada", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }

            }
            else
            {
                MessageBox.Show("No tienes nada que editar", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Sucursal sucursal = lstvSucursales.SelectedItem as Sucursal;
            if (manejadorDeSucursal.Listar.Count > 0) 
            {
                if (sucursal != null) 
                {
                    if (MessageBox.Show("Realmente deseas eliminar la sucursal ''"+sucursal.Nombre+"''", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None, MessageBoxOptions.ServiceNotification) == MessageBoxResult.Yes)
                    {
                        Empleado empleado = sucursal.Encargado as Empleado;
                        manejadorDeEmpleado.Eliminar(empleado.Id);

                        if (manejadorDeSucursal.Eliminar(sucursal.Id))
                        {
                            MessageBox.Show("Sucursal Eliminada", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                            ActualizarTablaSucursales();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error al eliminar", "Eliminar", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No has seleccionado nada", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }
            }
            else
            {
                MessageBox.Show("No tienes nada que eliminar", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbxDireccion.Text) || !string.IsNullOrWhiteSpace(tbxEmailDeEncargado.Text) || !string.IsNullOrWhiteSpace(tbxNombreEncargado.Text) || !string.IsNullOrWhiteSpace(tbxNombreSucursal.Text) || !string.IsNullOrWhiteSpace(tbxTelefonoEncargado.Text))
                {
                    if (accionDeSucursal == accion.nuevo)
                    {
                        Empleado empleado = new Empleado()
                        {
                            Nombre = tbxNombreEncargado.Text,
                            Cargo = "Gerente",
                            Telefono = tbxTelefonoEncargado.Text,
                            Sueldo = 823,
                            Email = tbxEmailDeEncargado.Text,
                            Contrasena = tbxContrasenaEncargado.Text,
                            
                        };
                        manejadorDeEmpleado.Agregar(empleado);

                        Sucursal sucursal = new Sucursal()
                        {
                            Direccion = tbxDireccion.Text,
                            Empleados = null,
                            Nombre = tbxNombreSucursal.Text,
                            Encargado = empleado
                        };
                        if (manejadorDeSucursal.Agregar(sucursal))
                        {
                            empleado.sucursal = sucursal;
                            manejadorDeEmpleado.Modificar(empleado);

                            MessageBox.Show("Se agrego correctamente la sucursal ''" + sucursal.Nombre + "''", "Agregar", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                            ActualizarTablaSucursales();
                            PanelDeDatos.IsEnabled = false;
                            BotonesHabilitados(false);
                            lstvSucursales.IsEnabled = true;
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error en la operaciòn", "Agregar", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                        }
                    }
                    else
                    {
                        Sucursal sucursal = lstvSucursales.SelectedItem as Sucursal;

                        Empleado empleado = sucursal.Encargado as Empleado;

                        sucursal.Direccion = tbxDireccion.Text;
                        sucursal.Nombre = tbxNombreSucursal.Text;
                        empleado.Email = tbxEmailDeEncargado.Text;
                        empleado.Nombre = tbxNombreEncargado.Text;
                        empleado.Telefono = tbxTelefonoEncargado.Text;
                        empleado.Contrasena = tbxContrasenaEncargado.Text;

                        manejadorDeEmpleado.Modificar(empleado);

                        sucursal.Encargado.Nombre = empleado.Nombre;
                        sucursal.Encargado.Telefono = empleado.Telefono;
                        sucursal.Encargado.Email = empleado.Email;
                        sucursal.Encargado.Contrasena = empleado.Contrasena;

                        

                        if (manejadorDeSucursal.Modificar(sucursal))
                        {
                            MessageBox.Show("Se modifico correctamente la sucursal ''" + sucursal.Nombre + "''", "Agregar", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                            ActualizarTablaSucursales();
                            PanelDeDatos.IsEnabled = false;
                            BotonesHabilitados(false);
                            lstvSucursales.IsEnabled = true;
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("Ocurrio un error en la operaciòn", "Agregar", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex.Message);
            }
        }
    }
}
