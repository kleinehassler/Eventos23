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
    [Route("api/invitado")]
    [ApiController]
    public class InvitadoController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<Invitado,InvitadoDTO> _repositorio;
        public InvitadoController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new InvitadoRepositorio(cadena_conexion);
        }


        [HttpGet]
        [Route("lista")]
        public RespuestaAPI<List<InvitadoDTO>> Listar(Dictionary<string, object> parametros)
        {
            RespuestaAPI<List<InvitadoDTO>> datos = new RespuestaAPI<List<InvitadoDTO>>();

            datos.success = true;
            datos.message = "OK";
            datos.data = _repositorio.Listar(parametros);

            return datos;
        }

        [HttpPost]
        [Route("crear")]
        public RespuestaAPI<Invitado> Crear(Invitado entidad)
        {
            RespuestaAPI<Invitado> datos = new RespuestaAPI<Invitado>();
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
        public RespuestaAPI<Invitado> Actualizar(Invitado entidad)
        {
            RespuestaAPI<Invitado> datos = new RespuestaAPI<Invitado>();
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
        public RespuestaAPI<Invitado> Eliminar(Invitado entidad)
        {
            RespuestaAPI<Invitado> datos = new RespuestaAPI<Invitado>();
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
