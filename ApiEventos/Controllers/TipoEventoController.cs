using BDEventos.Models;
using Entidades.Clases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio;

namespace ApiEventos.Controllers
{
    [Authorize]
    [Route("api/tipo-evento")]
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<TipoEvento,TipoEventoDTO> _repositorio;
        public TipoEventoController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new TipoEventoRepositorio(cadena_conexion);
        }


        [HttpGet]
        [Route("lista")]
        public RespuestaAPI<List<TipoEventoDTO>> Listar()
        {

            RespuestaAPI<List<TipoEventoDTO>> datos = new RespuestaAPI<List<TipoEventoDTO>>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                datos.data = _repositorio.Listar();
                return datos;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
                return datos;
            }
        }

        [HttpPost]
        [Route("crear")]
        public RespuestaAPI<TipoEvento> Crear(TipoEvento entidad)
        {
            RespuestaAPI<TipoEvento> datos = new RespuestaAPI<TipoEvento>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                datos.data = _repositorio.Crear(entidad);
                return datos;
            }
            catch (DbUpdateException ex)
            {
                datos.success = false;
                datos.message = ex.InnerException.Message;
                return datos;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
                return datos;
            }
        }

        [HttpPost]
        [Route("actualizar")]
        public RespuestaAPI<TipoEvento> Actualizar(TipoEvento entidad)
        {
            RespuestaAPI<TipoEvento> datos = new RespuestaAPI<TipoEvento>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                datos.data = _repositorio.Actualizar(entidad);
                return datos;
            }
            catch (DbUpdateException ex)
            {
                datos.success = false;
                datos.message = ex.InnerException.Message;
                return datos;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
                return datos;
            }
        }

        [HttpPost]
        [Route("eliminar")]
        public RespuestaAPI<TipoEvento> Eliminar(TipoEvento entidad)
        {
            RespuestaAPI<TipoEvento> datos = new RespuestaAPI<TipoEvento>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                datos.data = _repositorio.Eliminar(entidad);
                return datos;
            }
            catch (DbUpdateException ex)
            {
                datos.success = false;
                datos.message = ex.InnerException.Message;
                return datos;
            }
            catch (Exception ex)
            {
                datos.success = false;
                datos.message = ex.Message;
                return datos;
            }
        }
    }
}
