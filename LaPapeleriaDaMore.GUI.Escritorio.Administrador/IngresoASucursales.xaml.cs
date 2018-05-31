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
    /// Lógica de interacción para IngresoASucursales.xaml
    /// </summary>
    public partial class IngresoASucursales : Window
    {
        private int oportunidades=3;

        public IngresoASucursales()
        {
            InitializeComponent();

            lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
            lblErrorFaltaDeDatos.Visibility = Visibility.Collapsed;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(pswrContrasena.Password) && !string.IsNullOrWhiteSpace(tbxUsuario.Text))
                MessageBox.Show("Error no se ha podido reconocer los datos", "Error",MessageBoxButton.OK,MessageBoxImage.Hand,MessageBoxResult.None,MessageBoxOptions.ServiceNotification);
        }

        private void ValidacionDeContrasena()
        {
            lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
            lblErrorFaltaDeDatos.Visibility = Visibility.Collapsed;

            if (!string.IsNullOrWhiteSpace(pswrContrasena.Password) && !string.IsNullOrWhiteSpace(tbxUsuario.Text)) 
            {
                if (pswrContrasena.Password== "@-45*|7" && tbxUsuario.Text=="_-_")
                {
                    lblErrorContrasenaIncorrecta.Visibility = Visibility.Collapsed;
                    lblErrorFaltaDeDatos.Visibility = Visibility.Collapsed;
                    Sucursales pagina = new Sucursales();
                    pagina.Show();
                    this.Close();
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
                        lblErrorContrasenaIncorrecta.Content = string.Format("Contraseña o usuario incorrecta.\nTe quedan {0} oportunidades", oportunidades);
                    }

                }
            }
            else
            {
                lblErrorFaltaDeDatos.Visibility = Visibility.Visible;
            }
        }

        private void Entrar(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                 ValidacionDeContrasena();
            }
        }
    }
}
