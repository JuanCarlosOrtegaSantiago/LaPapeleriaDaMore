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
    /// Lógica de interacción para VentanaReguistros.xaml
    /// </summary>
    public partial class VentanaReguistros : Window
    {
        enum accion
        {
            nuevo,editar
        }
        accion accionDeCliente;
        accion accionDeEmpleado;

        Sucursal Sucursal;

        IMAnejadorDeCliente mAnejadorDeCliente;
        IManejadorDeEmpleado manejadorDeEmpleado;
        public VentanaReguistros(Sucursal sucursal)
        {

            InitializeComponent();

            this.Sucursal = sucursal;
            lblDeSucursalCliente.Content = string.Format("Sucursal {0}",sucursal.Nombre);
            lblDeSucursalEmpleado.Content = string.Format("Sucursal {0}", sucursal.Nombre);
            lblContrasenaDeEmpleado.Visibility = Visibility.Collapsed;
            tbxContrasenaDeEmpleado.Visibility = Visibility.Collapsed;

            mAnejadorDeCliente = new ManejadorDeCliente(new RepositorioDeCliente());
            manejadorDeEmpleado = new ManejadorDeEmpleado(new RepositorioDeEmpleado());

            ActualizarTablaDeEmpleado();
            ActualizarTablaDeCliente();

            LimpiarCamposDeEmpleado(false);
            LimpiarCamposDeCliente(false);

            HabilitarBotonesParaEmpleados(false);
            HabilitarBotonesParaClientes(false);

        }

        private void HabilitarBotonesParaEmpleados(bool v)
        {
            btnCancelarEmpleado.IsEnabled = v;
            btnEditarEmpleado.IsEnabled = !v;
            btnEliminarEmpleado.IsEnabled = !v;
            btnGuardarEmpleado.IsEnabled = v;
            btnNuevoEmpleado.IsEnabled = !v;
        }

        private void LimpiarCamposDeEmpleado(bool v)
        {
            tbxCargoDeEmpleado.Clear();
            tbxContrasenaDeEmpleado.Clear();
            tbxEmailDeEmpleado.Clear();
            tbxNombreDeEmpleado.Clear();
            tbxSueldoDeEmpleado.Clear();
            tbxTelefonoEmpleado.Clear();
            panelDeDatosEmpleado.IsEnabled = v;
            lstvEmpleados.IsEnabled = !v;
        }

        private void ActualizarTablaDeEmpleado()
        {
            lstvEmpleados.ItemsSource = null;
            lstvEmpleados.ItemsSource = manejadorDeEmpleado.Listar;
        }

        private void HabilitarBotonesParaClientes(bool habilitado)
        {
            btnCancelarCliente.IsEnabled = habilitado;
            btnEditarCliente.IsEnabled = !habilitado;
            btnEliminarCliente.IsEnabled = !habilitado;
            btnGuardarCliente.IsEnabled = habilitado;
            btnNuevoCliente.IsEnabled = !habilitado;
        }

        private void LimpiarCamposDeCliente(bool v)
        {
            tbxEmailDeCliente.Clear();
            tbxNombreDeCliente.Clear();
            tbxRFCDeCliente.Clear();
            tbxTelefonoCliente.Clear();
            panelDeDatosCliente.IsEnabled = v;
            lstvClientes.IsEnabled = !v;
        }

        private void ActualizarTablaDeCliente()
        {
            lstvClientes.ItemsSource = null;
            lstvClientes.ItemsSource = mAnejadorDeCliente.Listar;
        }

        private void btnNuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotonesParaClientes(true);
            LimpiarCamposDeCliente(true);
            accionDeCliente = accion.nuevo;
        }

        private void btnCancelarCliente_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotonesParaClientes(false);
            LimpiarCamposDeCliente(false);
        }

        private void btnEditarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (mAnejadorDeCliente.Listar.Count != 0)
            {
                Cliente cliente = lstvClientes.SelectedItem as Cliente;

                if (cliente != null)
                {
                    HabilitarBotonesParaClientes(true);
                    LimpiarCamposDeCliente(true);
                    accionDeCliente = accion.editar;
                    tbxEmailDeCliente.Text = cliente.Email;
                    tbxNombreDeCliente.Text = cliente.Nombre;
                    tbxRFCDeCliente.Text = cliente.RFC;
                    tbxTelefonoCliente.Text = cliente.Telefono;

                }
                else
                {
                    MensajeNoSeleccionadoNada("cliente", "editar", "Error al editar cliente");
                }
            }
            else
            {
                MensajeNoContienes("cliente","editar","Error al editar cliente");
                
            }
            

        }

        private void MensajeNoSeleccionadoNada(string elemento, string accion, string Titulo)
        {
            MessageBox.Show("No has selecionado ningun " + elemento + " para " + accion, "" + Titulo, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
        }

        private void MensajeDeOperacionIncorrecta(string elemento, string accion, string titulo)
        {
            MessageBox.Show("Ocurrio un error al "+accion+" el "+elemento, ""+titulo, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
        }

        private void MensajeDeOperacionCorrecta(string elemento, string accionTerminacionO, string tituloDeAccion)
        {
            MessageBox.Show(""+elemento+" se "+ accionTerminacionO + " correctamente", "Elemento "+ tituloDeAccion+" Correctamente", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
        }

        private void MensajeNoContienes(string elemento, string accion, string Titulo)
        {
            MessageBox.Show("No tienes ningun " + elemento + " para " + accion, "" + Titulo, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
        }

        private void btnEliminarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (mAnejadorDeCliente.Listar.Count != 0)
            {
                Cliente cliente = lstvClientes.SelectedItem as Cliente;

                if (cliente != null)
                {
                    if (MessageBox.Show("Realmente deseas eliminar el cliente''" + cliente.Nombre + "''", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None, MessageBoxOptions.ServiceNotification) == MessageBoxResult.Yes)
                    
                        if (mAnejadorDeCliente.Eliminar(cliente.Id))
                        {
                            MensajeDeOperacionCorrecta("cliente", "elimino", "eliminado");
                            ActualizarTablaDeCliente();
                        }
                        else
                        {
                            MensajeDeOperacionIncorrecta("cliente", "eliminar", "Error al eliminar cliente");
                        }
                }
                else
                {
                    MensajeNoSeleccionadoNada("cliente", "eliminar", "Error se seleccion");
                }
            }
            else
            {
                MensajeNoContienes("cliente", "eliminar", "Error de elementos");

            }
        }

        private void btnGuardarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbxEmailDeCliente.Text) && !string.IsNullOrWhiteSpace(tbxNombreDeCliente.Text) && !string.IsNullOrWhiteSpace(tbxRFCDeCliente.Text) && !string.IsNullOrWhiteSpace(tbxTelefonoCliente.Text)) 
                {
                    if (accionDeCliente == accion.nuevo)
                    {
                        Cliente cliente = new Cliente()
                        {
                            Email = tbxEmailDeCliente.Text,
                            Nombre = tbxNombreDeCliente.Text,
                            RFC = tbxRFCDeCliente.Text,
                            Telefono = tbxTelefonoCliente.Text
                        };
                        if (mAnejadorDeCliente.Agregar(cliente))
                        {
                            MensajeDeOperacionCorrecta("cliente", "agrego", "agregado");
                            ActualizarTablaDeCliente();
                            HabilitarBotonesParaClientes(false);
                            LimpiarCamposDeCliente(false);
                        }
                        else
                        {
                            MensajeDeOperacionIncorrecta("cliente", "guardar", "Error al guardar cliente");
                        }
                    }
                    else
                    {
                        Cliente cliente = lstvClientes.SelectedItem as Cliente;
                        cliente.Email = tbxEmailDeCliente.Text;
                        cliente.Nombre = tbxNombreDeCliente.Text;
                        cliente.RFC = tbxRFCDeCliente.Text;
                        cliente.Telefono = tbxTelefonoCliente.Text;
                        if (mAnejadorDeCliente.Modificar(cliente))
                        {
                            MensajeDeOperacionCorrecta("cliente", "modifico", "modificado");
                            ActualizarTablaDeCliente();
                            HabilitarBotonesParaClientes(false);
                            LimpiarCamposDeCliente(false);
                        }
                        else
                        {
                            MensajeDeOperacionIncorrecta("cliente", "modificar", "Error al modificar cliente");
                        }
                    }
                }
                else
                {
                    MensajeFaltaDeDatos();
                }
            }
            catch (Exception ex)
            {

                MensajeDeError(ex);
            }
        }

        private void MensajeFaltaDeDatos()
        {
            MessageBox.Show("Faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
        }

        private void MensajeDeError(Exception ex)
        {
            MessageBox.Show(""+ex.Message);
        }

        private void btnNuevoEmpleado_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotonesParaEmpleados(true);
            LimpiarCamposDeEmpleado(true);
            tbxCargoDeEmpleado.IsEnabled = true;
            accionDeEmpleado = accion.nuevo;
        }

        private void btnCancelarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotonesParaEmpleados(false);
            LimpiarCamposDeEmpleado(false);
            lblContrasenaDeEmpleado.Visibility = Visibility.Collapsed;
            tbxContrasenaDeEmpleado.Visibility = Visibility.Collapsed;
        }

        private void btnEditarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (manejadorDeEmpleado.Listar.Count != 0)
            {
                Empleado empleado = lstvEmpleados.SelectedItem as Empleado;
                if (empleado != null)
                {
                    LimpiarCamposDeEmpleado(true);
                    HabilitarBotonesParaEmpleados(true);
                    accionDeEmpleado = accion.editar;

                    if (empleado.Cargo == "Gerente")
                    {
                        tbxCargoDeEmpleado.Text = empleado.Cargo;
                        tbxCargoDeEmpleado.IsEnabled = false;

                        lblContrasenaDeEmpleado.Visibility = Visibility.Visible;
                        tbxContrasenaDeEmpleado.Visibility = Visibility.Visible;
                        tbxContrasenaDeEmpleado.Text = empleado.Contrasena;

                        tbxEmailDeEmpleado.Text = empleado.Email;
                        tbxNombreDeEmpleado.Text = empleado.Nombre;
                        tbxSueldoDeEmpleado.Text = empleado.Sueldo.ToString();
                        tbxTelefonoEmpleado.Text = empleado.Telefono;
                    }
                    else
                    {
                        tbxCargoDeEmpleado.Text = empleado.Cargo;
                        tbxEmailDeEmpleado.Text = empleado.Email;
                        tbxNombreDeEmpleado.Text = empleado.Nombre;
                        tbxSueldoDeEmpleado.Text = empleado.Sueldo.ToString();
                        tbxTelefonoEmpleado.Text = empleado.Telefono;
                    }
                    
                }
                else
                {
                    MensajeNoSeleccionadoNada("empleado", "editar", "Error en selección");
                }
            }
            else
            {
                MensajeNoContienes("empleado", "editar", "Falta de datos");
            }
        }

        private void btnEliminarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (manejadorDeEmpleado.Listar.Count != 0)
            {
                Empleado empleado = lstvEmpleados.SelectedItem as Empleado;
                if (empleado != null)
                {
                    if (empleado.Cargo == "Gerente")
                    {
                        MessageBox.Show("Este empleado no se puede eliminar solo modificar", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                    }
                    else
                    {
                        if (MessageBox.Show("Realmente deseas eliminar el empleado''" + empleado.Nombre + "''", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None, MessageBoxOptions.ServiceNotification) == MessageBoxResult.Yes)
                        {
                            if (manejadorDeEmpleado.Eliminar(empleado.Id))
                            {
                                MensajeDeOperacionCorrecta("empleado", "eliminado", "eliminado");
                                ActualizarTablaDeEmpleado();
                            }
                            else
                            {
                                MensajeDeOperacionIncorrecta("empleado", "eliminar", "Error al eliminar");
                            }
                        }
                    }

                }
                else
                {
                    MensajeNoSeleccionadoNada("empleado", "eliminar", "Error en selección");
                }
            }
            else
            {
                MensajeNoContienes("empleado", "eliminar", "Falta de datos");
            }
        }

        private void btnGuardarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbxCargoDeEmpleado.Text))
                {
                    if (accionDeEmpleado==accion.nuevo && tbxCargoDeEmpleado.Text!= "Gerente")
                    {
                        if (!string.IsNullOrWhiteSpace(tbxEmailDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxNombreDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxSueldoDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxTelefonoEmpleado.Text))
                        {
                            Empleado empleado = new Empleado()
                            {
                                Cargo = tbxCargoDeEmpleado.Text,
                                Contrasena = null,
                                Email = tbxEmailDeEmpleado.Text,
                                Nombre = tbxNombreDeEmpleado.Text,
                                Sueldo = float.Parse(tbxSueldoDeEmpleado.Text),
                                Telefono = tbxTelefonoEmpleado.Text
                            };
                            if (manejadorDeEmpleado.Agregar(empleado))
                            {
                                MensajeDeOperacionCorrecta("empleado", "agrego", "agregado");
                                ActualizarTablaDeEmpleado();
                                HabilitarBotonesParaEmpleados(false);
                                LimpiarCamposDeEmpleado(false);
                            }
                            else
                            {
                                MensajeDeOperacionIncorrecta("empleado", "agregar", "Error al agregar el empleado");
                            }

                        }
                        else
                        {
                            MensajeFaltaDeDatos();
                        }
                    }
                    
                    if (accionDeEmpleado == accion.editar && tbxCargoDeEmpleado.Text == "Gerente")
                    {
                        if (!string.IsNullOrWhiteSpace(tbxContrasenaDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxEmailDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxNombreDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxSueldoDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxTelefonoEmpleado.Text))
                        {
                            Empleado empleado = lstvEmpleados.SelectedItem as Empleado;
                            empleado.Contrasena = tbxContrasenaDeEmpleado.Text;
                            empleado.Email = tbxEmailDeEmpleado.Text;
                            empleado.Nombre = tbxNombreDeEmpleado.Text;
                            empleado.Sueldo = float.Parse(tbxSueldoDeEmpleado.Text);
                            empleado.Telefono = tbxTelefonoEmpleado.Text;
                            if (manejadorDeEmpleado.Modificar(empleado))
                            {
                                MensajeDeOperacionCorrecta("gerente", "modifico", "modificado");
                                ActualizarTablaDeEmpleado();
                                HabilitarBotonesParaEmpleados(false);
                                LimpiarCamposDeEmpleado(false);
                            }
                            else
                            {
                                MensajeDeOperacionIncorrecta("gerente", "modifico", "Error al modificar el gerente");
                            }
                        }
                        else
                        {
                            MensajeFaltaDeDatos();
                        }
                            
                    }
                    if (accionDeEmpleado == accion.editar && tbxCargoDeEmpleado.Text != "Gerente")
                    {
                        if (!string.IsNullOrWhiteSpace(tbxCargoDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxEmailDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxNombreDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxSueldoDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxTelefonoEmpleado.Text))
                        {
                            Empleado empleado = lstvEmpleados.SelectedItem as Empleado;

                            empleado.Cargo = tbxCargoDeEmpleado.Text;
                            empleado.Email = tbxEmailDeEmpleado.Text;
                            empleado.Nombre = tbxNombreDeEmpleado.Text;
                            empleado.Sueldo = float.Parse(tbxSueldoDeEmpleado.Text);
                            empleado.Telefono = tbxTelefonoEmpleado.Text;

                            if (manejadorDeEmpleado.Modificar(empleado))
                            {
                                MensajeDeOperacionCorrecta("empleado", "modifico", "modificado");
                                ActualizarTablaDeEmpleado();
                                HabilitarBotonesParaEmpleados(false);
                                LimpiarCamposDeEmpleado(false);
                            }
                            else
                            {
                                MensajeDeOperacionIncorrecta("empleado", "modifico", "Error al modificar el empleado");
                            }
                        }
                        else
                        {
                            MensajeFaltaDeDatos();
                        }
                    }
                }
                else
                {
                    MensajeFaltaDeDatos();
                }
            }
            catch (Exception ex)
            {

                MensajeDeError(ex);
            }
        }

    }
}
