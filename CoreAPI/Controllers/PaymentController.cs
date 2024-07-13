using Core;
using CoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        // GET: api/<PaymentController>
        [HttpGet]
        public List<Payment> Get()
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                return Payment.ListPayments(con);
            }
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public Payment Get(int id)
        {
            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();
                return Payment.GetPayment(con, id);
            }
        }

        // POST api/<PaymentController>
        [HttpPost]
        public IActionResult Post([FromBody] PaymentModel payment)
        {
            if (payment == null) return BadRequest("No hay pago.");

            using (var conn = new SqlConnection(Core.Program.ConnString))
            {
                conn.Open();

                Payment newPayment = new Payment()
                {
                    Id = payment.Id,
                    OrderId = payment.OrderId,
                    OrderNumber = payment.OrderNumber,
                    Amount = payment.Amount,
                    Date = payment.Date,
                    Status = payment.Status
                };

                Payment.AddPayment(conn, newPayment);
            }

            return Ok(payment);
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PaymentModel payment)
        {
            if (payment == null || payment.Id != id) return BadRequest("El producto es null o el ID no es igual.");

            try
            {
                using (var con = new SqlConnection(Core.Program.ConnString))
                {
                    con.Open();

                    var updatedPayment = new Payment()
                    {
                        Id = id,
                        OrderId = payment.OrderId,
                        OrderNumber = payment.OrderNumber,
                        Amount = payment.Amount,
                        Date = payment.Date,
                        Status = payment.Status
                    };

                    Payment.UpdatePayment(con, updatedPayment);
                }
            }
            catch (Exception)
            {
                return BadRequest("Error al actualizar el pago.");
            }

            return Ok(payment);
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();

                Payment payment = new Payment { Id = id };
                Payment.DeletePayment(connection, payment.Id);
            }

            return Ok("El pago se eliminó exitosamente.");
        }
    }
}
