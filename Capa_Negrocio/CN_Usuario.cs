using Capa_Datos;
using Capa_Entidad;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Capa_Negrocio
{
    public class CN_Usuario
    {
        CD_Usuario objUsuario = new CD_Usuario();
        public CN_Usuario() { }

        #region listar los usuarios
        public List<Usuario> ListarUusario(string cadenaConexion) { 
            return objUsuario.ListarUsuario(cadenaConexion);
        }
        #endregion



        #region agregar usuario
        public string AgregarUsuario(string cadenaConexion, Usuario user)
        {


            if (string.IsNullOrEmpty(user.nombre))
            {
                return "El nombre es requerido.";
            }
            else if (string.IsNullOrEmpty(user.contrasena) || string.IsNullOrWhiteSpace(user.contrasena))
            {
                return "la contraseña es requerida.";
            }
            else if (string.IsNullOrWhiteSpace(user.rol) || string.IsNullOrEmpty(user.rol)) {

                return "El rol es requerido.";
            }
            else
            {
                string contrasenaEncriptada = EncriptarContrasena(user.contrasena);
                user.contrasena = contrasenaEncriptada;
                return objUsuario.AgregarUsuario(cadenaConexion, user);
            }

        }
        #endregion


        #region actualizar usuario
        public string ActualizarUsuario(string cadenaConexion, int id, Usuario user)
        {


            if (string.IsNullOrEmpty(user.nombre))
            {
                return "El nombre es requerido.";
            }
            else if (string.IsNullOrEmpty(user.contrasena) || string.IsNullOrWhiteSpace(user.contrasena))
            {
                return "la contraseña es requerida.";
            }
            else if (string.IsNullOrWhiteSpace(user.rol) || string.IsNullOrEmpty(user.rol))
            {

                return "El rol es requerido.";
            }
            else
            {
                return objUsuario.ActualizarUsuario(cadenaConexion,id, user);
            }

        }
        #endregion




        #region Eliminar Usuario
        public string EliminarUsuario(string cadenaConexion, int id)
        {
            if (id == 0 || id == null)
            {
                return "El id es requerido.";
            }
            else 
            {
                return objUsuario.EliminarUsuario(id, cadenaConexion);
            }
        }
        #endregion



        #region validar las credencaiales
        public Usuario ValidarLogin(string cadena, Login login)
        {

            if (string.IsNullOrEmpty(login.nombre) || string.IsNullOrEmpty(login.contrasena)) {
                return new Usuario() ;
            }
            string contrasenaEncriptada = EncriptarContrasena(login.contrasena);
            login.contrasena = contrasenaEncriptada;
            return objUsuario.validarLogin(login, cadena);
        }
        #endregion





        #region encriptar contraseña
        public string EncriptarContrasena(string contrasena) 
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(encoding.GetBytes(contrasena));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        #endregion
    }
}