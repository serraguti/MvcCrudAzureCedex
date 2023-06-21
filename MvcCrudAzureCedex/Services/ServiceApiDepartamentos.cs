using MvcCrudAzureCedex.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
    }
}
