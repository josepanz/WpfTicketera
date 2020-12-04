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

namespace WpfTicketera
{
    /// <summary>
    /// Interaction logic for AltasClientes.xaml
    /// </summary>
    public partial class AltasClientes : Window
    {
        public AltasClientes()
        {
            InitializeComponent();
        }

        private async void ObtenerDatos()
        {
            List<Cliente> lista = await Cliente.ObtenerTodos();
            lstClientes.ItemsSource = lista;
        }

        private void lstClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstClientes.SelectedItem != null)
            {
                Cliente clienteSeleccionado = (Cliente)lstClientes.SelectedItem;
                txtId.Text = clienteSeleccionado.Id_Cliente.ToString();
                txtNombre.Text = clienteSeleccionado.Nombre;
                txtApellido.Text = clienteSeleccionado.Apellido;
                txtNroDoc.Text = clienteSeleccionado.CI;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ObtenerDatos();
        }

        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (validarDatos() is true)
            {
                Cliente c = new Cliente();
                c.Nombre = txtNombre.Text;
                c.Apellido = txtApellido.Text;
                c.CI = txtNroDoc.Text;

                await Cliente.AgregarCliente(c);
                ObtenerDatos();
                Limpiar();
            }
        }

        private async void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (lstClientes.SelectedItem != null)
            {
                if (validarDatos()== true)
                {
                    Cliente clienteSeleccionado = (Cliente)lstClientes.SelectedItem;
                    clienteSeleccionado.Nombre = txtNombre.Text;
                    clienteSeleccionado.Apellido = txtApellido.Text;
                    clienteSeleccionado.CI = txtNroDoc.Text;
                    await Cliente.ModificarCliente(clienteSeleccionado);
                    ObtenerDatos();
                    Limpiar();
                }
            }
            else
                MessageBox.Show("Debe seleccionar primeramente el cliente a modificar ");
        }

        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lstClientes.SelectedItem != null)
            {
                Cliente clienteSeleccionada = (Cliente)lstClientes.SelectedItem;
                await Cliente.EliminarCliente(clienteSeleccionada);
                ObtenerDatos();
                Limpiar();
            }
            else
                MessageBox.Show("Debe seleccionar primeramente el cliente a eliminar ");
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool validarDatos()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar el Nombre!");
                return false;
            }

            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe ingresar el Apellido!");
                return false;
            }

            if (txtNroDoc.Text == "")
            {
                MessageBox.Show("Debe ingresar el Nro. de Documento!");
                return false;
            }

            return true;
        }

        private void Limpiar()
        {
            txtId.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtNroDoc.Text = string.Empty;
        }
    }
}
