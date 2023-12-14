using BDEventos.Models;
using Entidades.Clases;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebEventos.Dominio;
using WebEventos.Models;

namespace WebEventos.Controllers
{
    public class EventoController : Controller
    {
        private IConfiguration _configuration;
        IHttpContextAccessor _httpContextAccessor;
        EventoDOM dominio;
        int idusuario;
        public EventoController(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = contextAccessor;

            string api = _configuration["Apis:ApiEvento"];

            string token = _httpContextAccessor.HttpContext.User.FindFirst("token").Value;

            dominio = new EventoDOM(api, token);

            idusuario = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("idusuario").Value);
        }
        public async Task<IActionResult> Index()
        {
            ViewData["HOST_URL"] = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value;

            EventoModel modelo = new EventoModel();
            modelo.titulo = "Mi lista de eventos";

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("pagina", 1);
            parametros.Add("registros", 20);
            parametros.Add("orden", "fecha");
            parametros.Add("idusuario", idusuario);
            modelo.listaEventos = await dominio.listar(parametros);

            return View(modelo);
        }

        public async Task<IActionResult> Registro(int idevento)
        {
            ViewData["HOST_URL"] = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            EventoModel modelo = new EventoModel();
            try
            {
                if (idevento > 0)
                {
                    var filtroEventos = new Dictionary<string, object>()
                    {
                        { "idevento",idevento},
                        { "pagina",1},
                        { "registros",1},
                        { "orden","nombre"},
                        { "direccion","ASC"}
                    };
                    var datos = await dominio.listar(filtroEventos);
                    modelo.evento = new RespuestaAPI<EventoDTO>();
                    modelo.evento.data = datos.First(); ;

                }
                else
                {
                    modelo.evento = new RespuestaAPI<EventoDTO>();
                    modelo.evento.data = new EventoDTO();
                    modelo.evento.data.Idevento = 0;
                }
                modelo.lista_tipo_evento = await dominio.listarTipoEventos();
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }
            return View(modelo);

        }

        public async Task<IActionResult> Ver(int idevento)
        {
            //ViewData["usuario"] = _httpContextAccessor.HttpContext.User.FindFirst("usuario").Value;
            ViewData["HOST_URL"] = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host.Value;
            EventoModel modelo = new EventoModel();
            try
            {
                if (idevento > 0)
                {
                    var filtroEventos = new Dictionary<string, object>()
                    {
                        { "filtro",""},
                        { "idevento",idevento},
                        { "pagina",1},
                        { "registros",1},
                        { "orden","nombre"},
                        { "direccion","ASC"},
                        { "idusuario", idusuario},
                        {"detalle","SI" }
                    };
                    var datos = await dominio.listar(filtroEventos);
                    modelo.evento = new RespuestaAPI<EventoDTO>();
                    modelo.evento.data = datos.First(); ;

                }
                else
                {
                    modelo.evento = new RespuestaAPI<EventoDTO>();
                    modelo.evento.data = new EventoDTO();
                    modelo.evento.data.Idevento = 0;
                }
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }
            return View(modelo);
        }

        public async Task<IActionResult> Crear([FromBody] EventoDTO entidad)
        {
            try
            {
                var idusuario = _httpContextAccessor.HttpContext.User.FindFirst("idusuario").Value;
                entidad.Idusuario = int.Parse(idusuario);
                EventoModel modelo = new EventoModel();
                modelo.evento = await dominio.crearEvento(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.evento));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }
        }

        public async Task<IActionResult> Actualizar([FromBody] EventoDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.evento = await dominio.actualizarEvento(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.evento));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> Eliminar([FromBody] EventoDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.evento = await dominio.eliminarEvento(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.evento));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> CrearInvitado([FromBody] InvitadoDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.invitado = await dominio.crearInvitado(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.invitado));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }
        }
        public async Task<IActionResult> EditarInvitado([FromBody] InvitadoDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.invitado = await dominio.actualizarInvitado(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.invitado));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> EliminarInvitado([FromBody] InvitadoDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.invitado = await dominio.eliminarInvitado(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.invitado));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> CrearMesa([FromBody] MesaDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.mesa = await dominio.crearMesa(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.mesa));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }
        public async Task<IActionResult> EditarMesa([FromBody] MesaDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.mesa = await dominio.actualizarMesa(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.mesa));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> EliminarMesa([FromBody] MesaDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.mesa = await dominio.eliminarMesa(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.mesa));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> CrearGrupo([FromBody] GrupoDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.grupo = await dominio.crearGrupo(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.grupo));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }
        public async Task<IActionResult> EditarGrupo([FromBody] GrupoDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.grupo = await dominio.actualizarGrupo(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.grupo));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> EliminarGrupo([FromBody] GrupoDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.grupo = await dominio.eliminarGrupo(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.grupo));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> CrearMenu([FromBody] MenuDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.menu = await dominio.crearMenu(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.menu));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }
        public async Task<IActionResult> EditarMenu([FromBody] MenuDTO entidad)
        {
            try
            {

                EventoModel modelo = new EventoModel();
                modelo.menu = await dominio.actualizarMenu(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.menu));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }

        public async Task<IActionResult> EliminarMenu([FromBody] MenuDTO entidad)
        {
            try
            {
                EventoModel modelo = new EventoModel();
                modelo.menu = await dominio.eliminarMenu(entidad);
                return Ok(JsonConvert.SerializeObject(modelo.menu));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }

        }
    }
}
