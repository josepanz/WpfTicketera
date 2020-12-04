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
            ObtenerDatosTicket();
        }

        private void dgTickets_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem != null)
            {
                Ticket t = (Ticket)dgTickets.SelectedItem;
                txtNroTicket.Text = t.Nro_Ticket.ToString();
                cboCliente.SelectedItem = t.Cliente;
                //intIdCaja = (int)t.Id_Caja;
            }
        }

        private void CargarDatosGrilla()
        {
            try
            {
                dgTickets.ItemsSource = datos.Ticket.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CargarDatosGrilla();

            //Cargamos el combo de Materiales
            cboCliente.ItemsSource = datos.Cliente.ToList();
            cboCliente.DisplayMemberPath = "Nombre" + " " + "Apellido";
            cboCliente.SelectedValuePath = "Id_Cliente";
        }

        public void ObtenerDatosTicket()
        {
            //trae la letra de la primera caja disponible
            var linqLetraTicket = (from p in datos.Caja
                                   where p.Estado == "D"
                                   orderby p.Codigo ascending
                                   select p.Codigo).Take(1);

            letraTicket = linqLetraTicket.ToList()[0].ToString();

            //trae el id de la primera caja disponible
            var linqIdCaja = (from p in datos.Caja
                              where p.Estado == "D"
                              orderby p.Codigo ascending
                              select p.Id_Caja).Take(1);

            string idCaja = linqIdCaja.ToList()[0].ToString();

            intIdCaja = Int32.Parse(idCaja);

            //trae el numero de ticket a asociar a la caja disponible
            int linqNroTicket = (from p in datos.Ticket
                                 where p.Id_Caja == intIdCaja && p.Fecha == DateTime.Now
                                 select p).Count() + 1;
            string nroTicket = linqNroTicket.ToString();

            nroLetraTicket = letraTicket + "" + nroTicket;


        }


        private void btnEliminar_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem != null)
            {
                Ticket t = (Ticket)dgTickets.SelectedItem;
                datos.Ticket.Remove(t);
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar un Ticket de la grilla para eliminar!");
        }

        private void btnModificar_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            if (dgTickets.SelectedItem != null)
            {
                Ticket t = (Ticket)dgTickets.SelectedItem;
                t.Nro_Ticket = txtNroTicket.Text;
                t.Cliente = (Cliente)cboCliente.SelectedItem;

                datos.Entry(t).State = System.Data.Entity.EntityState.Modified;
                datos.SaveChanges();
                CargarDatosGrilla();
            }
            else
                MessageBox.Show("Debe seleccionar una Ticket de la grilla para modificar!");
        }

        private void btnGuardar_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Ticket ticket = new Ticket();
            ticket.Nro_Ticket = txtNroTicket.Text;
            ticket.Fecha = DateTime.Now;
            ticket.Estado = "P";
            ticket.Cliente = (Cliente)cboCliente.SelectedItem;
            ticket.Id_Caja = intIdCaja;

            datos.Ticket.Add(ticket);
            datos.SaveChanges();
        }
    }

}
