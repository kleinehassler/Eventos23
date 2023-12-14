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
    [Route("api/grupo")]
    [ApiController]
    public class GrupoController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<Grupo,GrupoDTO> _repositorio;
        public GrupoController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new GrupoRepositorio(cadena_conexion);
        }


        [HttpGet]
        [Route("lista")]
        public RespuestaAPI<List<GrupoDTO>> Listar(Dictionary<string, object> parametros)
        {
            RespuestaAPI<List<GrupoDTO>> datos = new RespuestaAPI<List<GrupoDTO>>();

            datos.success = true;
            datos.message = "OK";
            datos.data = _repositorio.Listar(parametros);

            return datos;
        }

        [HttpPost]
        [Route("crear")]
        public RespuestaAPI<Grupo> Crear(Grupo entidad)
        {
            RespuestaAPI<Grupo> datos = new RespuestaAPI<Grupo>();
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
        public RespuestaAPI<Grupo> Actualizar(Grupo entidad)
        {
            RespuestaAPI<Grupo> datos = new RespuestaAPI<Grupo>();
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
        public RespuestaAPI<Grupo> Eliminar(Grupo entidad)
        {
            RespuestaAPI<Grupo> datos = new RespuestaAPI<Grupo>();
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
