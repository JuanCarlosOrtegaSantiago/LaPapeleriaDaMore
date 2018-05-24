using LaPapeleriaDaMore.BIZ;
using LaPapeleriaDaMore.COMMON.Entidades;
using LaPapeleriaDaMore.COMMON.Interfaces;
using LaPapeleriaDaMore.DAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        accion accionDeProducto;

        Sucursal Sucursal;

        IMAnejadorDeCliente mAnejadorDeCliente;
        IManejadorDeEmpleado manejadorDeEmpleado;
        IManejadorDeProducto manejadorDeProducto;
        IManejadorDeSucursal ManejadorDeSucursal;
        public VentanaReguistros(Sucursal sucursal)
        {

            InitializeComponent();


            this.Sucursal = sucursal;
            if (sucursal.Empleados == null)
            {
                sucursal.Empleados = new List<Empleado>();
            }
            lblDeSucursalCliente.Content = string.Format("Sucursal {0}",sucursal.Nombre);
            lblDeSucursalEmpleado.Content = string.Format("Sucursal {0}", sucursal.Nombre);
            lblDeSucursalProducto.Content = string.Format("Sucursal {0}", sucursal.Nombre);

            lblContrasenaDeEmpleado.Visibility = Visibility.Collapsed;
            tbxContrasenaDeEmpleado.Visibility = Visibility.Collapsed;

            ManejadorDeSucursal = new ManejadorDeSucursal(new RepositorioGenerico<Sucursal>());
            mAnejadorDeCliente = new ManejadorDeCliente(new RepositorioGenerico<Cliente>());
            manejadorDeEmpleado = new ManejadorDeEmpleado(new RepositorioGenerico<Empleado>());
            manejadorDeProducto = new ManejadorDeProducto(new RepositorioGenerico<Producto>());

            ActualizarTeblaDeProducto();
            ActualizarTablaDeEmpleado();
            ActualizarTablaDeCliente();

            LimpiarCamposDeEmpleado(false);
            LimpiarCamposDeCliente(false);
            LimpiarCamposDeProducto(false);

            HabilitarBotonesParaProductos(false);
            HabilitarBotonesParaEmpleados(false);
            HabilitarBotonesParaClientes(false);

        }

        private void HabilitarBotonesParaProductos(bool v)
        {
            btnCancelarProducto.IsEnabled = v;
            btnEditarProducto.IsEnabled = !v;
            btnEliminarProducto.IsEnabled = !v;
            btnGuardarProducto.IsEnabled = v;
            btnNuevoProducto.IsEnabled = !v;
        }

        private void LimpiarCamposDeProducto(bool v)
        {
            tbxCantidadDeProducto.Clear();
            tbxCodigoProducto.Clear();
            tbxNombreDelProveedorDeProducto.Clear();
            tbxNombreDeProducto.Clear();
            tbxPresioDeVentaDeProducto.Clear();
            panelDeDatosProducto.IsEnabled = v;
            lstvEmpleados.IsEnabled = !v;
        }

        private void ActualizarTeblaDeProducto()
        {
            lstProducto.ItemsSource = null;
            lstProducto.ItemsSource = manejadorDeProducto.Listar.Where(e => e.sucursal.Id == Sucursal.Id);
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
            panelDeDatosEmpleadoConFoto.IsEnabled = v;
            lstvEmpleados.IsEnabled = !v;
            ImgFoto.Source = null;
        }

        private void ActualizarTablaDeEmpleado()
        {
            lstvEmpleados.ItemsSource = null;
            lstvEmpleados.ItemsSource = Sucursal.Empleados;
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
            lstvClientes.ItemsSource = mAnejadorDeCliente.Listar.Where(e => e.sucursal.Id == Sucursal.Id);
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
                            Telefono = tbxTelefonoCliente.Text,
                            sucursal = Sucursal 
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
                        ImgFoto.Source = ByteToImagen(empleado.Fotografia);
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

        private ImageSource ByteToImagen(byte[] imageData)
        {
            if (imageData == null)
            {
                return null;
            }
            else
            {
                BitmapImage bitimg = new BitmapImage();
                MemoryStream las = new MemoryStream(imageData);
                bitimg.BeginInit();
                bitimg.StreamSource = las;
                bitimg.EndInit();
                ImageSource imgSrc = bitimg as ImageSource;
                return imgSrc;
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
                                Telefono = tbxTelefonoEmpleado.Text,
                                Fotografia = ImageToByte(ImgFoto.Source)
                            };

                            manejadorDeEmpleado.Agregar(empleado);
                            Sucursal.Empleados.Add(empleado);
                            if (ManejadorDeSucursal.Modificar(Sucursal))
                            {
                                MensajeDeOperacionCorrecta("empleado", "agrego", "agregado");
                                ActualizarTablaDeEmpleado();
                                HabilitarBotonesParaEmpleados(false);
                                LimpiarCamposDeEmpleado(false);
                            }
                            else
                            {
                                MensajeDeOperacionIncorrecta("empleado", "agregar", "Error al agregar el empleado");
                                throw new Exception("error");

                            }

                        }
                        else
                        {
                            MensajeFaltaDeDatos();
                        }
                    }
                    
                    else if (accionDeEmpleado == accion.editar && tbxCargoDeEmpleado.Text == "Gerente")
                    {
                        if (!string.IsNullOrWhiteSpace(tbxContrasenaDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxEmailDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxNombreDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxSueldoDeEmpleado.Text) && !string.IsNullOrWhiteSpace(tbxTelefonoEmpleado.Text))
                        {
                            Empleado empleado = lstvEmpleados.SelectedItem as Empleado;
                            Sucursal.Empleados.Remove(empleado);

                            Empleado empleado1 = new Empleado();
                            empleado1.Contrasena = tbxContrasenaDeEmpleado.Text;
                            empleado1.Email = tbxEmailDeEmpleado.Text;
                            empleado1.Nombre = tbxNombreDeEmpleado.Text;
                            empleado1.Sueldo = float.Parse(tbxSueldoDeEmpleado.Text);
                            empleado1.Telefono = tbxTelefonoEmpleado.Text;
                            empleado1.Fotografia = ImageToByte(ImgFoto.Source);

                            manejadorDeEmpleado.Agregar(empleado);
                            Sucursal.Empleados.Add(empleado1);
                            if (ManejadorDeSucursal.Modificar(Sucursal))
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
                            Sucursal.Empleados.Remove(empleado);

                            Empleado empleado1 = new Empleado()
                            {
                                Cargo = tbxCargoDeEmpleado.Text,
                                Contrasena = null,
                                Email = tbxEmailDeEmpleado.Text,
                                Nombre = tbxNombreDeEmpleado.Text,
                                Sueldo = float.Parse(tbxSueldoDeEmpleado.Text),
                                Telefono = tbxTelefonoEmpleado.Text,
                                Fotografia = ImageToByte(ImgFoto.Source)
                            };
                            manejadorDeEmpleado.Agregar(empleado);
                            Sucursal.Empleados.Add(empleado1);
                            if (ManejadorDeSucursal.Modificar(Sucursal))
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

        public byte[] ImageToByte(ImageSource image)
        {
            if (image != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image as BitmapSource));
                encoder.Save(memoryStream);
                return memoryStream.ToArray();
            }
            else
            {
                return null;
            }
        }

        private void btnNuevoProducto_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotonesParaProductos(true);
            LimpiarCamposDeProducto(true);
            accionDeProducto = accion.nuevo;
        }

        private void btnCancelarProducto_Click(object sender, RoutedEventArgs e)
        {
            HabilitarBotonesParaProductos(false);
            LimpiarCamposDeProducto(false);
        }

        private void btnEditarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (manejadorDeProducto.Listar.Count != 0)
            {
                Producto producto = lstProducto.SelectedItem as Producto;
                if (producto != null)
                {
                    LimpiarCamposDeProducto(true);
                    HabilitarBotonesParaProductos(true);
                    accionDeProducto = accion.editar;

                    tbxCantidadDeProducto.Text = producto.Cantidad.ToString();
                    tbxCodigoProducto.Text = producto.Codigo;
                    tbxNombreDelProveedorDeProducto.Text = producto.NombreProveedor;
                    tbxNombreDeProducto.Text = producto.Nombre;
                    tbxPresioDeVentaDeProducto.Text = producto.PresioVenta.ToString();
                }
                else
                {
                    MensajeNoSeleccionadoNada("producto", "editar", "Error en selección");
                }
            }
            else
            {
                MensajeNoContienes("producto", "editar", "Falta de datos");
            }
        }

        private void btnEliminarProducto_Click(object sender, RoutedEventArgs e)
        {
            if (manejadorDeProducto.Listar.Count != 0)
            {
                Producto producto = lstProducto.SelectedItem as Producto;
                if (producto != null)
                {
                    if (MessageBox.Show("Realmente deseas eliminar el producto''" + producto.Nombre + "''", "Eliminar", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.None, MessageBoxOptions.ServiceNotification) == MessageBoxResult.Yes)
                    {
                        if (manejadorDeProducto.Eliminar(producto.Id))
                        {
                            MensajeDeOperacionCorrecta("producto", "eliminado", "eliminado");
                            ActualizarTeblaDeProducto();
                        }
                        else
                        {
                            MensajeDeOperacionIncorrecta("producto", "eliminar", "Error al eliminar");
                        }
                    }
                }
                else
                {
                    MensajeNoSeleccionadoNada("producto", "eliminar", "Error en selección");
                }
            }
            else
            {
                MensajeNoContienes("producto", "eliminar", "Falta de datos");
            }
        }

        private void btnGuardarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(tbxCantidadDeProducto.Text) && !string.IsNullOrWhiteSpace(tbxCodigoProducto.Text) && !string.IsNullOrWhiteSpace(tbxNombreDelProveedorDeProducto.Text) && !string.IsNullOrWhiteSpace(tbxNombreDeProducto.Text) && !string.IsNullOrWhiteSpace(tbxPresioDeVentaDeProducto.Text))
                {
                    if (accionDeProducto == accion.nuevo)
                    {
                        Producto producto = new Producto()
                        {

                            Cantidad = int.Parse(tbxCantidadDeProducto.Text),
                            Nombre = tbxNombreDeProducto.Text,
                            Codigo = tbxCodigoProducto.Text,
                            NombreProveedor = tbxNombreDelProveedorDeProducto.Text,
                            PresioVenta = float.Parse(tbxPresioDeVentaDeProducto.Text),
                            sucursal = Sucursal
                        };
                        if (manejadorDeProducto.Agregar(producto))
                        {
                            MensajeDeOperacionCorrecta("producto", "agrego", "agregado");
                            ActualizarTeblaDeProducto();
                            HabilitarBotonesParaProductos(false);
                            LimpiarCamposDeProducto(false);
                        }
                        else
                        {
                            MensajeDeOperacionIncorrecta("producto", "guardar", "Error al guardar producto");
                        }
                    }
                    else
                    {
                        Producto producto = lstProducto.SelectedItem as Producto;
                        producto.Cantidad = int.Parse(tbxCantidadDeProducto.Text);
                        producto.Nombre = tbxNombreDeProducto.Text;
                        producto.Codigo = tbxCodigoProducto.Text;
                        producto.NombreProveedor = tbxNombreDelProveedorDeProducto.Text;
                        producto.PresioVenta = float.Parse(tbxPresioDeVentaDeProducto.Text);
                        producto.sucursal = Sucursal;
                        if (manejadorDeProducto.Modificar(producto))
                        {
                            MensajeDeOperacionCorrecta("producto", "modifico", "modificado");
                            ActualizarTeblaDeProducto();
                            HabilitarBotonesParaProductos(false);
                            LimpiarCamposDeProducto(false);
                        }
                        else
                        {
                            MensajeDeOperacionIncorrecta("producto", "modificar", "Error al modificar cliente");
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

        private void regresar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MainWindow pagina = new MainWindow();
                pagina.Show();
                this.Close();
            }
        }

        private void btnFoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Selecciona la fotografia";
            dialog.Filter = "Formato de imagen|*.jpg; *.png";
            if (dialog.ShowDialog().Value){
                ImgFoto.Source = new BitmapImage(new Uri(dialog.FileName));
            }
        }
    }
}
