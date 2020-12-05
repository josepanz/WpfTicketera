using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WpfTicketera
{
    public class ClienteAPI
    {
        public int Id_Cliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CI { get; set; }

        public override string ToString()
        {
            return this.Nombre;
        }

        public static async Task<List<ClienteAPI>> ObtenerTodos()
        {
            List<ClienteAPI> lstclienteApi = new List<ClienteAPI>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respuesta = await client.GetAsync("api/ClientesApi");

                if (respuesta.IsSuccessStatusCode)
                {
                    lstclienteApi = await respuesta.Content.ReadAsAsync<List<ClienteAPI>>();
                }
            }

            return lstclienteApi;
        }

        public static async Task<bool> AgregarCliente(ClienteAPI p)
        {
            //Muy parecido con el anterior, varia el metodo "PostAsJsonAsync", ademas de la URI, se le pasa como pareametro el objeto Persona.
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage respuesta = await client.PostAsJsonAsync("api/ClientesApi", p); //Aqui va el Endpoint api/Personas, junto con el Objeto Persona (p), ya que el objeto tiene en los valores de sus atributos, los valores para crear un nuevo recurso Persona.
                return respuesta.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> ModificarCliente(ClienteAPI p)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.PutAsJsonAsync("api/ClientesApi/" + p.Id_Cliente, p); 
                return respuesta.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> EliminarCliente(ClienteAPI p)
        {
            //Muy parecido con el anterior, varia el metodo "DeleteAsync"
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44333/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage respuesta = await client.DeleteAsync("api/ClientesApi/" + p.Id_Cliente); 
                return respuesta.IsSuccessStatusCode;
            }
        }

    }
}
