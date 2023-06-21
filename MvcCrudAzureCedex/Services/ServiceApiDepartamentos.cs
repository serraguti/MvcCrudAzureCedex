using MvcCrudAzureCedex.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MvcCrudAzureCedex.Services
{
    public class ServiceApiDepartamentos
    {
        private string UrlApi;
        //EN SERVIDOR, DEBEMOS INDICAR EL TIPO DE DATOS 
        //QUE ESTAMOS CONSUMIENDO.
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiDepartamentos(IConfiguration configuration)
        {
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiCrudAzureCedex");
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //METODO PARA RECUPERAR TODOS LOS DEPARTAMENTOS
        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //NECESITAMOS LA PETICION
                string request = "/api/departamentos";
                //LA PETICION NECESITA UNA URL DE BASE
                client.BaseAddress = new Uri(this.UrlApi);
                //LIMPIAMOS LAS CABECERAS
                client.DefaultRequestHeaders.Clear();
                //TIPO DE DATOS A CONSUMIR
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //REALIZAMOS LA PETICION
                HttpResponseMessage response =
                    await client.GetAsync(request);
                //COMPROBAMOS SI LA PETICION ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    //AQUI, SI LOS NOMBRES DE LAS PROPIEDADES NO SE LLAMAN
                    //IGUAL, DEBEMOS UTILIZAR NEWTON CON [JsonProperty] EN EL MODEL
                    //string data = await response.Content.ReadAsStringAsync();
                    //JsonConvert.DeserializeObject<>(data);
                    List<Departamento> departamentos =
                        await response.Content.ReadAsAsync<List<Departamento>>();
                    return departamentos;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Departamento> FindDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                //NECESITAMOS LA PETICION
                string request = "/api/departamentos/" + id; 
                //LA PETICION NECESITA UNA URL DE BASE
                client.BaseAddress = new Uri(this.UrlApi);
                //LIMPIAMOS LAS CABECERAS
                client.DefaultRequestHeaders.Clear();
                //TIPO DE DATOS A CONSUMIR
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //REALIZAMOS LA PETICION
                HttpResponseMessage response =
                    await client.GetAsync(request);
                //COMPROBAMOS SI LA PETICION ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    Departamento departamento =
                        await response.Content.ReadAsAsync<Departamento>();
                    return departamento;
                }
                else
                {
                    return null;
                }
            }
        }

        //POST
        public async Task CreateDepartamentoAsync
            (string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                //DEPENDIENDO DE LO QUE ESTEMOS CONSUMIENDO EN CLOUD
                //ES DECIR, DEPENDIENDO DE DONDE ESTE ALOJADA EL API
                //NOS PUEDE DAR ERROR UTILIZAR BaseAddress
                //Ejemplos: Api Management, VM
                string url = this.UrlApi + request;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //DEBEMOS CREAR EL OBJETO MODEL PARA CONVERTIRLO A JSON
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = 0;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                //CONVERTIMOS EL OBJETO A JSON
                string json = JsonConvert.SerializeObject(departamento);
                //PARA ENVIAR LOS DATOS SE UTILIZA LA CLASE StringContent
                StringContent content = new
                    StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(url, content);
                //PODRIAMOS HACER NUESTRO IF PARA DEVOLVER LLAMADAS PERSONALIZADAS
            }
        }

        private StringContent 
            ConvertStringContent(int id, string nombre, string localidad)
        {
            //DEBEMOS CREAR EL OBJETO MODEL PARA CONVERTIRLO A JSON
            Departamento departamento = new Departamento();
            departamento.IdDepartamento = id;
            departamento.Nombre = nombre;
            departamento.Localidad = localidad;
            //CONVERTIMOS EL OBJETO A JSON
            string json = JsonConvert.SerializeObject(departamento);
            //PARA ENVIAR LOS DATOS SE UTILIZA LA CLASE StringContent
            StringContent content = new
                StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task UpdateDepartamentoAsync(int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos";
                string url = this.UrlApi + request;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                StringContent content = this.ConvertStringContent(id, nombre, localidad);
                HttpResponseMessage response = await
                    client.PutAsync(url, content);
            }
        }

        public async Task DeleteDepartamentoAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/departamentos/" + id;
                string url = this.UrlApi + request;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await
                    client.DeleteAsync(url);
            }
        }
    }
}
