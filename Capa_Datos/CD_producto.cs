using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Datos
{
    public class CD_producto
    {



        #region Listar los Productos
        public List<Producto> ListarProductos(string Conexion)
        {
            string con = Conexion;
            List<Producto> ListProductos = new List<Producto>();
            try
            {
                using (SqlConnection conexion = new SqlConnection(con))
                {
                    string query = "SELECT * FROM PRODUCTO";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conexion.Open();

                    using (SqlDataReader result = cmd.ExecuteReader())
                    {
                        while (result.Read())
                        {
                            ListProductos.Add(new Producto()
                            {
                                id = (int)result["Id"],
                                nombre = result["Nombre"].ToString(),
                                precio = (decimal)result["Precio"]
                            });
                        }
                    }
                    return ListProductos;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                return ListProductos;
            }
        }
        #endregion



        #region agregar Productos
        public string AgregarProducto(string Conexion, Producto producto)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion))
                {
                    string query = "INSERT INTO Producto( NOMBRE, PRECIO) VALUES(@NOMBRE, @PRECIO)";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOMBRE", producto.nombre);
                    cmd.Parameters.AddWithValue("@PRECIO", producto.precio);
                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        status = "Producto agregado correctamente.";
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





        #region actualizar un Producto
        public string ActualizarProducto(string Conexion, int id, Producto producto)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion))
                {
                    string query = "UPDATE PRODUCTO SET NOMBRE = @NOMBRE, PRECIO = @PRECIO WHERE ID = @ID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOMBRE", producto.nombre);
                    cmd.Parameters.AddWithValue("@PRECIO", producto.precio);
                    cmd.Parameters.AddWithValue("@ID", id);
                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        status = "Producto actualizado correctamente.";
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




        #region Eliminar producto
        public string EliminarProducto(int id, string Conexion)
        {
            string status = string.Empty;
            try
            {
                using (SqlConnection conexion = new SqlConnection(Conexion))
                {
                    string query = "DELETE FROM Producto WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(query, conexion);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        status = "Prodcuto eliminado correctamente.";
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
    }
}
