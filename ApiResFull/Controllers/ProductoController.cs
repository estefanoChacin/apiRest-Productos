using Capa_Entidad;
using Capa_Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiResFull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private IConfiguration _config;
        private string? cadenaSQL;
        CN_Producto objProducto = new CN_Producto();


        public ProductoController(IConfiguration config) { 
            _config= config;
            cadenaSQL = config.GetConnectionString("conexion");
        }




        #region litar todos los Productos
        [HttpGet]
        [Route("ListarProductos")]
        [Authorize(Roles = "admin,seguridad,cliente")]
        public ActionResult ListarProductos()
        {
            try
            {
                List<Producto> productos = objProducto.ListarProductos(cadenaSQL);
                return Ok(productos);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
        #endregion




        #region agregar un producto
        [HttpPost]
        [Route("AgregarProducto")]
        [Authorize(Roles = "admin,seguridad")]
        public ActionResult AgregarProducto(Producto producto)
        {
            try
            {
                string status = objProducto.AgregarProducto(cadenaSQL, producto);
                return Ok(status);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }
        #endregion




        #region Actualizar producto
        [HttpPut("{id}")]
        [Authorize(Roles = "admin,seguridad")]
        public ActionResult ActualizarProducto(int id, Producto producto)
        {
            try
            {
                string status = objProducto.ActualizarProducto(cadenaSQL, id, producto);
                return Ok(status);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }
        #endregion




        #region Eliminar prodcuto
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin,seguridad")]
        public ActionResult EliminarProducto(int id)
        {
            try
            {
                string status = objProducto.EliminarProducto(cadenaSQL, id);
                return Ok(status);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
                throw;
            }
        }
        #endregion
    }
}
