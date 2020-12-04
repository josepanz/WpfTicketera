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

namespace WpfTicketera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            /*Tickets*/
            AltasTickets win2 = new AltasTickets();
            win2.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*Clientes*/
            AltasClientes c = new AltasClientes();
            c.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*Salir*/
            System.Windows.Application.Current.Shutdown();
        }
    }
}
