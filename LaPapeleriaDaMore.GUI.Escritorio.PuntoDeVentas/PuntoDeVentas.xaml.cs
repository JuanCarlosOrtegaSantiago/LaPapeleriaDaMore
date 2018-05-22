using LaPapeleriaDaMore.COMMON.Entidades;
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

namespace LaPapeleriaDaMore.GUI.Escritorio.PuntoDeVentas
{
    /// <summary>
    /// Lógica de interacción para PuntoDeVentas.xaml
    /// </summary>
    public partial class PuntoDeVentas : Window
    {
        Sucursal sucursal;
        Empleado empleado;
        public PuntoDeVentas(Sucursal sucursal,Empleado empleado)
        {
            InitializeComponent();

            this.empleado = empleado;
            this.sucursal = sucursal;
            lblEmpleado.Content = string.Format("Empleado: {0}-{1}",empleado.Nombre,empleado.Cargo);
            ImgFoto.Source = null;
            ImgFoto.Source = ByteToImagen(empleado.Fotografia);
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
            throw new NotImplementedException();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagina = new MainWindow();
            pagina.Show();
            this.Close();
        }

        private void btnNuevaVenta_Click(object sender, RoutedEventArgs e)
        {
            Venta pagina = new Venta(sucursal, empleado);
            pagina.Show();
            this.Close();
        }
    }
}
