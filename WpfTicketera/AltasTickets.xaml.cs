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
    /// Interaction logic for AltasTickets.xaml
    /// </summary>
    public partial class AltasTickets : Window
    {
        string letraTicket = "";
        int intIdCaja = 0;
        string nroLetraTicket = "";

        //Creamos el objeto 
        BD_TicketEntities datos;
        public AltasTickets()
        {
            InitializeComponent();
            datos = new BD_TicketEntities();
        }

        private void dgTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem != null)
            {
                Ticket t = (Ticket)dgTickets.SelectedItem;
                txtNroTicket.Text = t.Nro_Ticket.ToString();
                cboCliente.SelectedItem = t.Cliente;
                intIdCaja = (int)t.Id_Caja;
                nroLetraTicket = t.Nro_Ticket;
            }
        }

        private void CargarDatosGrilla()
        {
            try
            {
                dgTickets.ItemsSource = datos.Tickets.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();
            ObtenerDatosTicket();

            //Cargamos el combo de Clientes
            cboCliente.ItemsSource = datos.Clientes.ToList();
            cboCliente.DisplayMemberPath = "Nombre" + " " + "Apellido";
            cboCliente.SelectedValuePath = "Id_Cliente";
        }

        public void ObtenerDatosTicket()
        {
            //trae la letra de la primera caja disponible
            var linqLetraTicket = (from p in datos.Cajas
                                   where p.Estado == "D"
                                   orderby p.Codigo ascending
                                   select p.Codigo).Take(1);
            letraTicket = linqLetraTicket.ToList()[0].ToString();

            //trae el id de la primera caja disponible
            var linqIdCaja = (from p in datos.Cajas
                              where p.Estado == "D"
                              orderby p.Codigo ascending
                              select p.Id_Caja).Take(1);

            string idCaja = linqIdCaja.ToList()[0].ToString();

            intIdCaja = Int32.Parse(idCaja);

            DateTime startDateTime = DateTime.Today;

            //trae el numero de ticket a asociar a la caja disponible
            int linqNroTicket = (from p in datos.Tickets
                                 where (p.Id_Caja == intIdCaja && p.Fecha == DateTime.Today)
                                 select p).Count();
            string nroTicket = (linqNroTicket + 1).ToString();

            nroLetraTicket = letraTicket + "" + nroTicket;

        }


        private void btnEliminar_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem != null)
            {
                Ticket t = (Ticket)dgTickets.SelectedItem;
                datos.Tickets.Remove(t);
                datos.SaveChanges();
                CargarDatosGrilla();
                limpiarDatos();
                ObtenerDatosTicket();
            }
            else
                MessageBox.Show("Debe seleccionar un Ticket de la grilla para eliminar!");
        }

        private void btnModificar_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem != null)
            {
                cboCliente.IsEnabled = false;
                Ticket t = (Ticket)dgTickets.SelectedItem;
                t.Nro_Ticket = txtNroTicket.Text;
                t.Cliente = (Cliente)cboCliente.SelectedItem;
                t.Estado = txtEstado.Text;

                datos.Entry(t).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
                limpiarDatos();
                ObtenerDatosTicket();
                cboCliente.IsEnabled = true;
            }
            else
                MessageBox.Show("Debe seleccionar una Ticket de la grilla para modificar!");
        }

        private void btnGuardar_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Ticket ticket = new Ticket();
            //ticket.Id_Cliente = (Ticket)cboCliente.SelectedIndex;
            ticket.Nro_Ticket = nroLetraTicket;
            ticket.Id_Caja = intIdCaja;
            ticket.Fecha = DateTime.Now;
            ticket.Estado = "P";
            datos.Tickets.Add(ticket);
            datos.SaveChanges();
            limpiarDatos();
            ObtenerDatosTicket();

        }

        private void limpiarDatos()
        {
            txtNroTicket.Text = "";
            txtEstado.Text = "";
            cboCliente.SelectedIndex = -1;
        }
    }

}
