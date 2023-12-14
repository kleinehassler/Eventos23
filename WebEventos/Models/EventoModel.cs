using BDEventos.Models;
using Entidades;
using Entidades.Clases;

namespace WebEventos.Models
{
    public class EventoModel
    {
        public string titulo { get; set; }
        public List<EventoDTO> listaEventos {  get; set; }
        public List<Dictionary<string,object>> listaEventosDic { get; set; }
        public List<Tuple<int, string, string, DateTime>> listaEventosTupla { get; set; }

        public RespuestaAPI<EventoDTO>? evento { get; set; }
        public RespuestaAPI<List<TipoEventoDTO>>? lista_tipo_evento { get; set; }

        public RespuestaAPI<InvitadoDTO>? invitado { get; set; }
        public RespuestaAPI<MenuDTO>? menu { get; set; }
        public RespuestaAPI<GrupoDTO>? grupo { get; set; }
        public RespuestaAPI<MesaDTO>? mesa { get; set; }
    }
}
