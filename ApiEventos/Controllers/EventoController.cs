using BDEventos.Models;
using Entidades.Clases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio;

namespace ApiEventos.Controllers
{

    /// <summary>
    /// Controlador para manejo de la tabla eventos
    /// </summary>
    [Authorize]
    [Route("api/evento")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<Evento,EventoDTO> _repositorio;
        public EventoController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new EventoRepositorio(cadena_conexion);
        }


        /// <summary>
        /// Permite listar la informacion de ventos
        /// </summary>
        /// <param name="parametros"></param>
        /// <returns>Datos del evento</returns>
        [HttpPost]
        [Route("listar")]
        public RespuestaAPI<List<EventoDTO>> Listar(Dictionary<string,object> parametros)
        {
            RespuestaAPI<List<EventoDTO>> respuesta = new RespuestaAPI<List<EventoDTO>>();
            try
            {                
                respuesta.success = true;
                respuesta.message = "Ok";
                respuesta.data = _repositorio.Listar(parametros);

            }
            catch (Exception ex)
            {
                respuesta.success = false;
                respuesta.message = ex.Message;
            }
            return respuesta;
        }

        [HttpPost]
        [Route("crear")]
        public RespuestaAPI<Evento> Crear(Evento entidad)
        {
            RespuestaAPI<Evento> datos = new RespuestaAPI<Evento>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                datos.data = _repositorio.Crear(entidad);
            }
            catch (DbUpdateException ex)
            {
                datos.success = false;
                datos.message = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
            }

            return datos;
        }

        [HttpPost]
        [Route("actualizar")]
        public RespuestaAPI<Evento> Actualizar(Evento entidad)
        {
            RespuestaAPI<Evento> datos = new RespuestaAPI<Evento>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                datos.data = _repositorio.Actualizar(entidad);
            }
            catch (DbUpdateException ex)
            {
                datos.success = false;
                datos.message = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
            }

            return datos;
        }

        [HttpPost]
        [Route("eliminar")]
        public RespuestaAPI<Evento> Eliminar(Evento entidad)
        {
            RespuestaAPI<Evento> datos = new RespuestaAPI<Evento>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                _repositorio.Eliminar(entidad);
            }
            catch (DbUpdateException ex)
            {
                datos.success = false;
                datos.message = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
            }

            return datos;
        }
    }
}
