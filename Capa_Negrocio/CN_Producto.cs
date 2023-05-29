using Capa_Datos;
using Capa_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Negocio
{
    public class CN_Producto
    {
        CD_producto objProducto = new CD_producto();
        public CN_Producto() { }

        #region listar los productos
        public List<Producto> ListarProductos(string cadenaConexion)
        {
            return objProducto.ListarProductos(cadenaConexion);
        }
        #endregion




        #region agregar Producto
        public string AgregarProducto(string cadenaConexion, Producto producto)
        {


            if (string.IsNullOrEmpty(producto.nombre))
            {
                return "El nombre es requerido.";
            }
            else if (producto.precio == null )
            {
                return "El precio es requerido";
            }
           
            else
            {
                return objProducto.AgregarProducto(cadenaConexion, producto);
            }

        }
        #endregion




        #region actualizar Producto
        public string ActualizarProducto(string cadenaConexion, int id, Producto producto)
        {


            if (string.IsNullOrEmpty(producto.nombre))
            {
                return "El nombre es requerido.";
            }
            else if (producto.precio == null)
            {
                return "El precio es requerido";
            }

            else
            {
                return objProducto.ActualizarProducto(cadenaConexion, id, producto);
            }

        }
        #endregion




        #region Eliminar Prodcuto
        public string EliminarProducto(string cadenaConexion, int id)
        {
            if (id == 0 || id == null)
            {
                return "El id es requerido.";
            }
            else
            {
                return objProducto.EliminarProducto(id, cadenaConexion);
            }
        }
        #endregion
    }
}
