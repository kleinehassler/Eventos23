using BDEventos.Models;
using Entidades;
using Entidades.Clases;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebEventos.Dominio
{
    public class SesionDOM : BaseDOM
    {
        public SesionDOM(string urlApi):base(urlApi)
        {

        }
        public async Task<RespuestaAPI<UsuarioDTO>> autenticar(Usuario datos) {
            var response = await this.PostAsJsonAsync("/api/sesion/autenticar", datos);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<UsuarioDTO>>();
            return result!;
        }

        public Dictionary<string, object> Cast(dynamic dato)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(dato.ToString());
        }
    }
}
