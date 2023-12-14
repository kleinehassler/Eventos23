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
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private IConfiguration _configuration;
        private IRepositorio<Menu,MenuDTO> _repositorio;
        public MenuController(IConfiguration configuration)
        {
            _configuration = configuration;

            string cadena_conexion = _configuration["ConnectionStrings:ConexionEventos"];

            _repositorio = new MenuRepositorio(cadena_conexion);
        }


        [HttpGet]
        [Route("lista")]
        public RespuestaAPI<List<MenuDTO>> Listar(Dictionary<string, object> parametros)
        {
            RespuestaAPI<List<MenuDTO>> datos = new RespuestaAPI<List<MenuDTO>>();

            datos.success = true;
            datos.message = "OK";
            datos.data = _repositorio.Listar(parametros);

            return datos;
        }

        [HttpPost]
        [Route("crear")]
        public RespuestaAPI<Menu> Crear(Menu entidad)
        {
            RespuestaAPI<Menu> datos = new RespuestaAPI<Menu>();
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
        public RespuestaAPI<Menu> Actualizar(Menu entidad)
        {
            RespuestaAPI<Menu> datos = new RespuestaAPI<Menu>();
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
        public RespuestaAPI<Menu> Eliminar(Menu entidad)
        {
            RespuestaAPI<Menu> datos = new RespuestaAPI<Menu>();
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
