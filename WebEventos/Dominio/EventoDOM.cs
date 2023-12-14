using BDEventos.Models;
using Entidades;
using Entidades.Clases;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace WebEventos.Dominio
{
    public class EventoDOM:BaseDOM
    {
        public EventoDOM(string url) : base(url)
        {
        }
        public EventoDOM(string urlApi, string token):base(urlApi, token)
        {

        }
        public async Task<List<EventoDTO>> listar(Dictionary<string, object> parametros) {
            

            var response = await this.PostAsJsonAsync("/api/evento/listar", parametros);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<List<EventoDTO>>>();
            return result.data;
        }
        public async Task<List<Dictionary<string,object>>> listarDiccionario()
        {
            return await this.GetFromJsonAsync<List<Dictionary<string, object>>>("api/evento/listar");
        }
        public async Task<List<Tuple<int, string, string, DateTime>>> listarTupla()
        {
            var listaTuplas = new List<Tuple<int, string, string, DateTime>>();

            listaTuplas =  await this.GetFromJsonAsync<List<Tuple<int , string , string , DateTime>>>("api/evento/listar");

            return listaTuplas;
        }

        public async Task<RespuestaAPI<EventoDTO>> crearEvento(EventoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/evento/crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<EventoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<EventoDTO>> actualizarEvento(EventoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/evento/actualizar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<EventoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<EventoDTO>> eliminarEvento(EventoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/evento/eliminar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<EventoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<List<TipoEventoDTO>>> listarTipoEventos()
        {
            return await this.GetFromJsonAsync<RespuestaAPI<List<TipoEventoDTO>>>("/api/tipo-evento/lista"); ;
        }

        public async Task<RespuestaAPI<List<InvitadoDTO>>> listarInvitados(Dictionary<string, object> parametros)
        {
            var response = await this.PostAsJsonAsync("/api/invitado/lista", parametros);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<List<InvitadoDTO>>>();
            return result!;
        }

        public async Task<RespuestaAPI<InvitadoDTO>> crearInvitado(InvitadoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/invitado/crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<InvitadoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<InvitadoDTO>> actualizarInvitado(InvitadoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/invitado/actualizar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<InvitadoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<InvitadoDTO>> eliminarInvitado(InvitadoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/invitado/eliminar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<InvitadoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<MenuDTO>> crearMenu(MenuDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/menu/crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<MenuDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<MenuDTO>> actualizarMenu(MenuDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/menu/actualizar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<MenuDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<MenuDTO>> eliminarMenu(MenuDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/menu/eliminar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<MenuDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<GrupoDTO>> crearGrupo(GrupoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/grupo/crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<GrupoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<GrupoDTO>> actualizarGrupo(GrupoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/grupo/actualizar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<GrupoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<GrupoDTO>> eliminarGrupo(GrupoDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/grupo/eliminar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<GrupoDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<MesaDTO>> crearMesa(MesaDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/mesa/crear", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<MesaDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<MesaDTO>> actualizarMesa(MesaDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/mesa/actualizar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<MesaDTO>>();
            return result!;
        }

        public async Task<RespuestaAPI<MesaDTO>> eliminarMesa(MesaDTO modelo)
        {
            var response = await this.PostAsJsonAsync("/api/mesa/eliminar", modelo);
            var result = await response.Content.ReadFromJsonAsync<RespuestaAPI<MesaDTO>>();
            return result!;
        }
    }
}
