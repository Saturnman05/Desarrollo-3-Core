using Microsoft.AspNetCore.Mvc;
using Core;
using System.Data.SqlClient;
using CoreAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        [HttpGet]
        public List<Product> Get()
        {
            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();
                return Product.ListProducts(connection);
            }
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();
                return Product.GetProduct(connection, id);
            }
        }

        // POST api/<ProductsController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductModel product)
        {
            if (product == null) return BadRequest("El producto es null.");

            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();

                var newProduct = new Product()
                {
                    Id = product.Id,
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock
                };

                Product.AddProduct(connection, newProduct);
            }

            return Ok("Producto añadido con éxito.");
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductModel product)
        {
            if (product == null || product.Id != id) return BadRequest("El producto es null o el ID no es igual.");

            using (var con = new SqlConnection(Core.Program.ConnString))
            {
                con.Open();

                var updatedProduct = new Product()
                {
                    Id = id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock
                };

                Product.UpdateProduct(con, updatedProduct);
            }

            return Ok("El producto se actualizó exitosamente.");
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var connection = new SqlConnection(Core.Program.ConnString))
            {
                connection.Open();

                Product product = new Product { Id = id };
                Product.DeleteProduct(connection, product);
            }

            return Ok("El producto se eliminó exitosamente.");
        }
    }
}
