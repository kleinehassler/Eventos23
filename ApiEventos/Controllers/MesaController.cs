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
    [Route("api/mesa")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<Mesa,MesaDTO> _repositorio;
        public MesaController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new MesaRepositorio(cadena_conexion);
        }


        [HttpGet]
        [Route("lista")]
        public RespuestaAPI<List<MesaDTO>> Listar(Dictionary<string, object> parametros)
        {
            RespuestaAPI<List<MesaDTO>> datos = new RespuestaAPI<List<MesaDTO>>();

            datos.success = true;
            datos.message = "OK";
            datos.data = _repositorio.Listar(parametros);

            return datos;
        }

        [HttpPost]
        [Route("crear")]
        public RespuestaAPI<Mesa> Crear(Mesa entidad)
        {
            RespuestaAPI<Mesa> datos = new RespuestaAPI<Mesa>();
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
        public RespuestaAPI<Mesa> Actualizar(Mesa entidad)
        {
            RespuestaAPI<Mesa> datos = new RespuestaAPI<Mesa>();
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
        public RespuestaAPI<Mesa> Eliminar(Mesa entidad)
        {
            RespuestaAPI<Mesa> datos = new RespuestaAPI<Mesa>();
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
