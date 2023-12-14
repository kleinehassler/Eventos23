using BDEventos.Models;
using Entidades.Clases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negocio;

namespace ApiUsuarios.Controllers
{
    [Authorize]
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<Usuario,UsuarioDTO> _repositorio;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new UsuarioRepositorio(cadena_conexion);
        }


        [HttpPost]
        [Route("listar")]
        public RespuestaAPI<List<UsuarioDTO>> Listar(Dictionary<string,object> parametros)
        {
            RespuestaAPI<List<UsuarioDTO>> respuesta = new RespuestaAPI<List<UsuarioDTO>>();
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
        public RespuestaAPI<Usuario> Crear(Usuario entidad)
        {
            RespuestaAPI<Usuario> datos = new RespuestaAPI<Usuario>();
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
        public RespuestaAPI<Usuario> Actualizar(Usuario entidad)
        {
            RespuestaAPI<Usuario> datos = new RespuestaAPI<Usuario>();
            try
            {
                datos.success = true;
                datos.message = "OK";
                _repositorio.Actualizar(entidad);
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
        public RespuestaAPI<Usuario> Eliminar(Usuario entidad)
        {
            RespuestaAPI<Usuario> datos = new RespuestaAPI<Usuario>();
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
