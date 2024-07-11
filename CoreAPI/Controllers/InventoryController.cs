using Core;
using CoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        // GET: api/<InventoryController>
        [HttpGet]
        public List<Inventory> Get()
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                return Inventory.ListItems(con);
            }
        }

        // GET api/<InventoryController>/5
        [HttpGet("{id}")]
        public Inventory Get(int id)
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                return Inventory.GetItem(con, id);
            }
        }

        // POST api/<InventoryController>
        [HttpPost]
        public IActionResult Post([FromBody] InventoryModel inventory)
        {
            if (inventory == null) return BadRequest("No hay pago.");

            using (var conn = new SqlConnection(Core.Program.ConnString))
            {
                conn.Open();

                Inventory newItem = new Inventory()
                {
                    Id = inventory.Id,
                    ProductId = inventory.ProductId,
                    Quantity = inventory.Quantity
                };

                Inventory.AddItem(conn, newItem);
            }

            return Ok(inventory);
        }

        // PUT api/<InventoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] InventoryModel inventory)
        {
            if (inventory == null || inventory.Id != id) return BadRequest("El producto es null o el ID no es igual.");

            try
            {
                using (var con = new SqlConnection(Core.Program.ConnString))
                {
                    con.Open();

                    var updateItem = new Inventory()
                    {
                        Id = id,
                        ProductId = inventory.ProductId,
                        Quantity = inventory.Quantity
                    };

                    Inventory.UpdateInventory(con, updateItem);
                }
            }
            catch (Exception)
            {
                return BadRequest("Error al actualizar el pago.");
            }

            return Ok(inventory);
        }

        // DELETE api/<InventoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();

                Inventory payment = new Inventory { Id = id };
                Inventory.DeleteItem(connection, payment.Id);
            }

            return Ok("El producto se eliminó del inventario exitosamente.");
        }
    }
}
