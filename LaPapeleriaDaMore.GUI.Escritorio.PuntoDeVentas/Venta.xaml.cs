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
        float Total=0;
        int NumVenta=0;
        enum TipoDeCliente
        {
            nuevo,
            frecuente
        }
        TipoDeCliente clienteTipo;

        Empleado empleado;
        Sucursal sucursal;
        Ventas ventas;

        IManejadorDeVenta manejadorDeVenta;
        IManejadorDeProducto manejadorDeProducto;
        IMAnejadorDeCliente mAnejadorDeCliente;
        public Venta(Sucursal sucursal, Empleado empleado)
        {
            InitializeComponent();

            this.empleado = empleado;
            this.sucursal = sucursal;

            manejadorDeProducto = new ManejadorDeProducto(new RepositorioDeProducto());
            mAnejadorDeCliente = new ManejadorDeCliente(new RepositorioDeCliente());
            manejadorDeVenta = new ManejadorDeVenta(new RepositorioDeVenta());

            lblDeEmpleado.Content = string.Format("{0}", empleado.Nombre);
            lblDeSucursalCliente.Content = string.Format("{0}", sucursal.Nombre);
            lblFecha.Content = "";

            cmbClienteNuevo.Visibility = Visibility.Collapsed;
            cmbDeClienteFrecuente.Visibility = Visibility.Collapsed;

            ActualizarCombos();

            HabilitarCampos(false);
            HabilitarBotones(true);
        }

        private void ActualizarCombos()
        {
            cmbxProducto.ItemsSource = null;
            cmbxProducto.ItemsSource = manejadorDeProducto.Listar.Where(e => e.sucursal.Id == sucursal.Id);

            cmbxDeClienteFrecuente.ItemsSource = null;
            cmbxDeClienteFrecuente.ItemsSource = mAnejadorDeCliente.Listar.Where(e => e.sucursal.Id == sucursal.Id);
        }

        private void HabilitarBotones(bool Habilitado)
        {
            btnCancelarVenta.IsEnabled = !Habilitado;
            btnGuardarVender.IsEnabled = !Habilitado;
            btnNuevaVenta.IsEnabled = Habilitado;
            btnRegresar.IsEnabled = Habilitado;
        }

        private void HabilitarCampos(bool Habilitado)
        {
            panelDeDatosVenta.IsEnabled = Habilitado;
            lstvMaterialesEnLista.ItemsSource = null;
            tbxNombreDeCliente.Clear();
            tbxTelefonoDeCliente.Clear();
            tbxCantidad.Clear();
            lblFecha.Content = "";
            lblTotal.Content = "";

            ActualizarCombos();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PuntoDeVentas pagina = new PuntoDeVentas(sucursal,empleado);
            pagina.Show();
            this.Close();
        }

        private void btnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            HabilitarCampos(true);
            lblFecha.Content = DateTime.Now.ToShortDateString();
            HabilitarBotones(false);
            ventas = new Ventas();
            ventas.productos = new List<Producto>();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Producto producto = cmbxProducto.SelectedItem as Producto;
                if (producto != null && !string.IsNullOrWhiteSpace(tbxCantidad.Text))
                {
                    int Cantidad = int.Parse(tbxCantidad.Text);

                    if (Cantidad < producto.Cantidad)
                    {
                        int cantidadOriginal = producto.Cantidad-Cantidad;

                        producto.Cantidad = cantidadOriginal;
                        manejadorDeProducto.Modificar(producto);

                        Total += Cantidad * producto.PresioVenta;

                        Producto producto1 = new Producto();
                        producto1.Nombre = producto.Nombre;
                        producto1.Cantidad = Cantidad;
                        producto1.PresioVenta = producto.PresioVenta;
                        producto1.Id = producto.Id;
                        producto1.Codigo = producto.Codigo;
                        producto1.sucursal = producto.sucursal;
                        producto1.NombreProveedor = producto.NombreProveedor;

                        ventas.productos.Add(producto1);

                        ActualizaTablaDeProductos();

                        lblTotal.Content = string.Format("$ {0}", Total.ToString());
                    }
                    else
                    {
                        MessageBox.Show("La cantidad que solicitas es mayor la la existente\n(" + producto.Cantidad + ") atriculo en existencia" , "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                    }

                }
                else
                {
                    MessageBox.Show("Te faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(""+ex.Message);
            }
            
        }

        private void ActualizaTablaDeProductos()
        {
            lstvMaterialesEnLista.ItemsSource = null;
            lstvMaterialesEnLista.ItemsSource = ventas.productos;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Producto producto = lstvMaterialesEnLista.SelectedItem as Producto;
                if (producto != null)
                {
                    int Cantidad = producto.Cantidad;

                    Producto producto1 = manejadorDeProducto.BuscarPorId(producto.Id);
                    //Producto producto2 = manejadorDeProducto.BuscarPorId(producto.Id) as Producto;

                    int cantidadOriginal = producto1.Cantidad;
                    int CantidadTotal = cantidadOriginal + Cantidad;

                    producto1.Cantidad = CantidadTotal;

                    manejadorDeProducto.Modificar(producto1);

                    Total -= Cantidad * producto.PresioVenta;

                    ventas.productos.Remove(producto);
                    ActualizaTablaDeProductos();

                    lblTotal.Content = string.Format("$ {0}", Total.ToString());

                }
                else
                {
                    MessageBox.Show("Te faltan datos por llenar", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("" + ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
            }
        }

        private void btnClienteNuevo_Click(object sender, RoutedEventArgs e)
        {
            clienteTipo = TipoDeCliente.nuevo;
            cmbClienteNuevo.Visibility = Visibility.Visible;
            cmbDeClienteFrecuente.Visibility = Visibility.Collapsed;
        }

        private void btnClienteFrecuente_Click(object sender, RoutedEventArgs e)
        {
            clienteTipo = TipoDeCliente.frecuente;
            cmbClienteNuevo.Visibility = Visibility.Collapsed;
            cmbDeClienteFrecuente.Visibility = Visibility.Visible;
        }

        private void btnCancelarVenta_Click(object sender, RoutedEventArgs e)
        {
            if (ventas.productos.Count == 0)
            {
                HabilitarCampos(false);
                HabilitarBotones(true);
                cmbClienteNuevo.Visibility = Visibility.Collapsed;
                cmbDeClienteFrecuente.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Te faltan "+ventas.productos.Count+" elementos\npor eliminar de la lista","Advertencia",MessageBoxButton.OK,MessageBoxImage.Stop,MessageBoxResult.None,MessageBoxOptions.ServiceNotification);
            }
            
        }

        private void btnGuardarVender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ventas.empleado = empleado;
                ventas.FechaDeVenta = DateTime.Now;
                ventas.PresioVenta = Total;
                ventas.sucursal = sucursal;

                if (clienteTipo == TipoDeCliente.nuevo) 
                {
                    Cliente cliente = new Cliente();
                    cliente.Nombre = tbxNombreDeCliente.Text;
                    cliente.Telefono = tbxTelefonoDeCliente.Text;
                    cliente.sucursal = sucursal;
                    ventas.cliente = cliente;
                }
                else
                {
                    Cliente cliente = cmbxDeClienteFrecuente.SelectedItem as Cliente;
                    ventas.cliente = cliente;
                }
                NumVenta += 1;
                ventas.Nombre = string.Format("V-0{0}",NumVenta);
                if (manejadorDeVenta.Agregar(ventas))
                {
                    MessageBox.Show("Gracias por su compra", "Bien", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                    HabilitarCampos(false);
                    HabilitarBotones(true);
                    cmbClienteNuevo.Visibility = Visibility.Collapsed;
                    cmbDeClienteFrecuente.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("La compra no se pudo guardar", "error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                    HabilitarCampos(false);
                    HabilitarBotones(true);
                    cmbClienteNuevo.Visibility = Visibility.Collapsed;
                    cmbDeClienteFrecuente.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(""+ex.Message,"error",MessageBoxButton.OK,MessageBoxImage.Exclamation,MessageBoxResult.None,MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
