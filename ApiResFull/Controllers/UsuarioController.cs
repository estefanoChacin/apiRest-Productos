using Capa_Entidad;
using Capa_Negrocio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.CodeDom.Compiler;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ApiResFull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private IConfiguration? _config;
        private readonly string? cadenaSQL;
        CN_Usuario objUsuario = new CN_Usuario();

        public UsuarioController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("conexion");
            _config = config;
        }




        #region litar todos los usuarios
        [HttpGet]
        [Route("Listar")]
        [Authorize(Roles = "admin")]
        public ActionResult ListarUsuarios()
        {
            try
            {
                List<Usuario> usuarios = objUsuario.ListarUusario(cadenaSQL);
                return Ok(usuarios);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        #endregion




        #region agregar un usuario
        [HttpPost]
        [Route("Agregar usuario")]
        [Authorize(Roles = "admin")]
        public ActionResult AgregarUsuario(Usuario user)
        {
            try
            {
                string status = objUsuario.AgregarUsuario(cadenaSQL, user);
                return Ok(status);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }
        #endregion




        #region Actualizar usuario
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult ActualizarUsuario(int id, Usuario user)
        {
            try
            {
                string status = objUsuario.ActualizarUsuario(cadenaSQL, id, user);
                return Ok(status);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }
        #endregion




        #region Eliminar usuario
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult EliminarUsuario(int id)
        {
            try
            {
                string status = objUsuario.EliminarUsuario(cadenaSQL, id);
                return Ok(status);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }
        #endregion


        #region Generar Json Web Token
        private string Generate(Usuario user)
        {
            var cc = _config["JWT:key"];
            var security = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_config["JWT:key"]));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            //crear los claims
            var claims = new[] {
                new Claim("Id", user.id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.nombre),
                new Claim(ClaimTypes.Surname, user.contrasena),
                new Claim(ClaimTypes.Role, user.rol),
            };

            //crear el token
            var token = new JwtSecurityToken(
                    _config["JWT:issuer"],
                    _config["JWT:audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(2),
                    signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion


        [HttpGet]
        [Route("Autenticar")]
        public ActionResult validarLogin(Login login)
        {
            Usuario user;
            try
            {
                user  = objUsuario.ValidarLogin(cadenaSQL, login);
                if (user.id != null && user.id != 0)
                {
                    string token = Generate(user);
                    return Ok(token);
                }
                else 
                { 
                
                    return NotFound("Credenciales incorrectas.");
                }
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }

    }
}
