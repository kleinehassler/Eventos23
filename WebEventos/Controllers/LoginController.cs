using BDEventos.Models;
using Entidades.Clases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Claims;
using WebEventos.Dominio;

namespace WebEventos.Controllers
{
    public class LoginController : Controller
    {
        IConfiguration _configuration;

        public LoginController(IConfiguration configuration) { 
            _configuration = configuration;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> Autenticar([FromBody] Usuario data)
        {
            SesionDOM domLogin = new SesionDOM(_configuration.GetSection("Apis").GetSection("ApiEvento").Value);
            try
            {
                RespuestaAPI<UsuarioDTO> datos = await domLogin.autenticar(data);

                if (datos.success)
                    await AcreditarUsuario(datos.data);
                return Ok(JsonConvert.SerializeObject(datos));
            }
            catch (Exception ex)
            {
                return Ok(JsonConvert.SerializeObject(new Dictionary<string, object>() { { "success", false }, { "message", ex.Message } }));
            }
        }

        public async Task<IActionResult> Cerrar()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }


        private async Task AcreditarUsuario(UsuarioDTO usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Usuario1),
                new Claim("idusuario", usuario.Idusuario.ToString()),
                new Claim("token", usuario.token)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //Asigna a la sesion
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
        }

    }
}
