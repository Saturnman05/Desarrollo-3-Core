using Core;
using CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        [HttpGet]
        public List<Order> Get()
        {
            using (var conn = new SqlConnection(Core.Program.ConnString))
            {
                conn.Open();
                return Order.ListOrders(conn);
            }
        }

        // GET api/<OrderController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post([FromBody] OrderModel order)
        {
            if (order == null) return BadRequest("La nueva orden está vacía.");

            using (var conn = new SqlConnection(Core.Program.ConnString))
            {
                conn.Open();

                var newOrder = new Order()
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    Date = order.Date
                };

                Order.AddOrder(conn, newOrder);
            }

            return Ok("Se creó la orden exitosamente.");
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderModel order)
        {
            if (order == null) return BadRequest("La orden está vacía.");

            using (var conn = new SqlConnection(Core.Program.ConnString))
            {
                conn.Open();

                Order newOrder = new Order()
                {
                    Id = id,
                    ProductId = order.ProductId,
                    Quantity = order.Quantity,
                    TotalPrice = order.TotalPrice,
                    Date = order.Date
                };

                Order.UpdateOrder(conn, newOrder);
            }

            return Ok(order);
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (var connection = new SqlConnection(Core.Program.ConnString))
                {
                    connection.Open();

                    var order = new Order() { Id = id };
                    Order.DeleteOrder(connection, order.Id);
                }
            }
            catch (Exception)
            {
                return BadRequest("No se pudo eliminar la orden.");
            }

            return Ok("Se eliminó la orden correctamente.");
        }
    }
}
