using BDEventos.Models;
using Entidades.Clases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Negocio;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiEventos.Controllers
{
    [Route("api/sesion")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        IConfiguration _config;
        private UsuarioRepositorio _repositorio;

        public SesionController(IConfiguration configuration)
        {
            this._config = configuration;

            string cadena_conexion = _config["ConnectionStrings:ConexionEventos"];

            _repositorio = new UsuarioRepositorio(cadena_conexion);
        }

        [HttpPost]
        [Route("autenticar")]
        public RespuestaAPI<UsuarioDTO> autenticar(Usuario usuario)
        {
            RespuestaAPI<UsuarioDTO> rpta = new RespuestaAPI<UsuarioDTO>();

            List<UsuarioDTO> lista = _repositorio.Autenticar(usuario.Usuario1, usuario.Clave);
            if (lista.Count > 0)
            {
                rpta.success = true;
                rpta.message = "Inicio sesion de maera correcta";
                lista.First().token = GenerarToken(lista.First().Idusuario);
                rpta.data = lista.First();
            }
            else {
                rpta.success = false;
                rpta.message = "Usuario no autenticado";
            }
            return rpta;
        }

        private string GenerarToken(int idUsuario)
        {
            var key = _config.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, idUsuario.ToString()));
            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = credencialesToken
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            string tokenCreado = tokenHandler.WriteToken(tokenConfig);
            return tokenCreado;
        }
    }


    

}
