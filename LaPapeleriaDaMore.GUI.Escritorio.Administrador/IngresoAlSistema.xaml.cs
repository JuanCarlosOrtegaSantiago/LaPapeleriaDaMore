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
        public IngresoAlSistema(Empleado empleado)
        {
            InitializeComponent();
            this.empleado = empleado;
            MessageBox.Show("Bienvenido "+empleado.Nombre,"Bienvenido",MessageBoxButton.OK,MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);

            lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
            lblErrorFaltaContrasena.Visibility = Visibility.Collapsed;
            txtblkUsuario.Content = empleado.Nombre;
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pagina = new MainWindow();
            pagina.Show();
            this.Close();
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
            lblErrorFaltaContrasena.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrWhiteSpace(pswrContrasena.Password))
            {
                if (empleado.Contrasena == pswrContrasena.Password)
                {
                    lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
                    lblErrorFaltaContrasena.Visibility = Visibility.Collapsed;
                    MessageBox.Show("La contraseña es correcta","correcto", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    oportunidades -= 1;
                    if (oportunidades == 0)
                    {
                        MessageBox.Show("Has agotado tus intentos", "Intento fallodo", MessageBoxButton.OK, MessageBoxImage.Asterisk, MessageBoxResult.None, MessageBoxOptions.ServiceNotification);
                        MainWindow pagina = new MainWindow();
                        pagina.Show();
                        this.Close();
                    }
                    else
                    {
                        lblErrorContrasenaIncorrecta.Visibility = Visibility.Visible;
                        lblErrorContrasenaIncorrecta.Content = string.Format("Contraseña incorrecta.\nTe quedan {0} oportunidades",oportunidades);
                    }
                    
                }
            }
            else
            {
                lblErrorFaltaContrasena.Visibility = Visibility.Visible;
            }
        }
    }
}
