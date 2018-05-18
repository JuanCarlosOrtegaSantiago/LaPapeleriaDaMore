using LaPapeleriaDaMore.COMMON.Entidades;
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
    /// Lógica de interacción para IngresoAlSistema.xaml
    /// </summary>
    public partial class IngresoAlSistema : Window
    {
        int oportunidades = 3;
        Empleado empleado;
        Sucursal sucursal;
        public IngresoAlSistema(Empleado empleado, Sucursal sucursal)
        {
            InitializeComponent();
            this.empleado = empleado;
            this.sucursal = sucursal;
            MessageBox.Show("Bienvenido "+empleado.Nombre,"Bienvenido",MessageBoxButton.OK,MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);

            lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
            lblErrorFaltaContrasena.Visibility = Visibility.Collapsed;
            txtblkUsuario.Content = empleado.Nombre;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            ValidacionDeContrasena();
        }

        private void pswrContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ValidacionDeContrasena();
            }
        }

        private void ValidacionDeContrasena()
        {
            //lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
            //lblErrorFaltaContrasena.Visibility = Visibility.Collapsed;

            //if (!string.IsNullOrWhiteSpace(pswrContrasena.Password))
            //{
            //    if (empleado.Contrasena == pswrContrasena.Password)
            //    {
                    lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
                    lblErrorFaltaContrasena.Visibility = Visibility.Collapsed;
                    VentanaReguistros pagina = new VentanaReguistros(sucursal);
                    pagina.Show();
                    this.Close();
        //        }
        //        else
        //        {
        //            oportunidades -= 1;
        //            if (oportunidades == 0)
        //            {
        //                MessageBox.Show("Has agotado tus intentos", "Intento fallodo", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
        //                MainWindow pagina = new MainWindow();
        //                pagina.Show();
        //                this.Close();
        //            }
        //            else
        //            {
        //                lblErrorContrasenaIncorrecta.Visibility = Visibility.Visible;
        //                lblErrorContrasenaIncorrecta.Content = string.Format("Contraseña incorrecta.\nTe quedan {0} oportunidades", oportunidades);
        //            }

        //        }
        //    }
        //    else
        //    {
        //        lblErrorFaltaContrasena.Visibility = Visibility.Visible;
        //    }
        }
    }
}
