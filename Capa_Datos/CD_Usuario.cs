using Capa_Entidad;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_Usuario
    {


        #region Listar los usuarios
        public List<Usuario> ListarUsuario(string Conexion)
        {
            string con = Conexion;
            List<Usuario> Listusurios = new List<Usuario>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(con))
                {
                    string query = "SELECT * FROM USUARIO";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conexion.Open();

                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            Listusurios.Add(new Usuario
                            {
                                id = (int)result["Id"],
                                nombre = result["Nombre"].ToString(),
                                contrasena = result["Contrasena"].ToString(),
                                rol = result["Rol"].ToString()
                            });
                        }
                    }
                    return Listusurios;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return Listusurios;
            }
        }
        #endregion



        #region agregar Usuarios
        public string AgregarUsuario(string Conexion, Usuario user) {
            string status = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion))
                {
                    string query = "INSERT INTO USUARIO( NOMBRE, CONTRASENA, ROL) VALUES(@NOMBRE, @CONTRASENA, @ROL)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOMBRE", user.nombre);
                    cmd.Parameters.AddWithValue("@CONTRASENA", user.contrasena);
                    cmd.Parameters.AddWithValue("@ROL", user.rol);


                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0) {
                        status = "Usuario agregado correctamente.";
                    }
                    return status;
                    
                }

            }
            catch (Exception e)
            {
                status = e.Message;
                return status;

            }
        }
        #endregion





        #region actualizar un usuario
        public string ActualizarUsuario(string Conexion, int id, Usuario user) 
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion))
                {
                    string query = "UPDATE USUARIO SET NOMBRE = @NOMBRE, CONTRASENA = @CONTRASENA, ROL = @ROL WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOMBRE", user.nombre);
                    cmd.Parameters.AddWithValue("@CONTRASENA", user.contrasena);
                    cmd.Parameters.AddWithValue("@ROL", user.rol);
                    cmd.Parameters.AddWithValue("@ID", id);

                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        status = "Usuario actulaizado correctamente.";
                    }
                    return status;

                }

            }
            catch (Exception e)
            {
                status = e.Message;
                return status;

            }
        }
        #endregion




        #region Eliminar usuario
        public string EliminarUsuario(int id, string Conexion) 
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion))
                {
                    string query = "DELETE FROM USUARIO WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        status = "Usuario eliminado correctamente.";
                    }
                    return status;
                }

            }
            catch (Exception e)
            {
                status = e.Message;
                return status;
            }
        }
        #endregion




        #region validar las credenciales para la autenticacion
        public Usuario validarLogin(Login login, string con) 
        {
            Usuario user = new Usuario();
            try
            {
                using (SqlConnection conexion = new SqlConnection(con))
                {
                    string query = "SELECT * FROM USUARIO WHERE NOMBRE = @NOMBRE AND CONTRASENA = @CONTRASENA";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOMBRE", login.nombre);
                    cmd.Parameters.AddWithValue("@CONTRASENA", login.contrasena);

                    conexion.Open();


                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        if (result.Read())
                        {
                            user.id = (int)result["Id"];
                            user.nombre = result["Nombre"].ToString();
                            user.contrasena = result["Contrasena"].ToString();
                            user.rol = result["Rol"].ToString();
                        }

                    }
                    return user;
                    
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return user;
            }
        }
        #endregion


    }
}
