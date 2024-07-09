using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; }

        public static void AddOrder (SqlConnection con, Order order)
        {
            string sql = "INSERT INTO orders (product_id, quantity, total_price, date) VALUES (@product_id, @quantity, @total_price, @date)";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@product_id", order.ProductId);
                command.Parameters.AddWithValue("@quantity", order.Quantity);
                command.Parameters.AddWithValue("@total_price", order.TotalPrice);
                command.Parameters.AddWithValue("@date", order.Date);
                command.ExecuteNonQuery();
            }
        }

        public static List<Order> ListOrders (SqlConnection con)
        {
            List<Order> orders = new List<Order>();

            string sql = "SELECT * FROM orders";
            using (var command = new SqlCommand(sql, con))
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Lista de Órdenes:");
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    int productId = int.Parse(reader["product_id"].ToString());
                    int quantity = int.Parse(reader["quantity"].ToString());
                    decimal totalPrice = decimal.Parse(reader["total_price"].ToString());
                    DateTime date = DateTime.Parse(reader["date"].ToString());

                    Order order = new Order()
                    {
                        Id = id,
                        ProductId = productId,
                        Quantity = quantity,
                        TotalPrice = totalPrice,
                        Date = date
                    };

                    orders.Add(order);

                    // Console.WriteLine($"{product.Id}: {product.Name} - {product.Description} - ${product.Price} - Stock: {product.Stock}");
                }
            }

            return orders;
        }

        public static void UpdateOrder (SqlConnection conn, Order order)
        {
            string sql = "UPDATE orders SET product_id = @product_id, quantity = @quantity, total_price = @total_price, date = @date WHERE id = @id";

            using (var command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@product_id", order.ProductId);
                command.Parameters.AddWithValue("@quantity", order.Quantity);
                command.Parameters.AddWithValue("@total_price", order.TotalPrice);
                command.Parameters.AddWithValue("@date", order.Date);
                command.Parameters.AddWithValue("@id", order.Id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteOrder (SqlConnection conn, int id)
        {
            string sql = "DELETE FROM orders WHERE id = @id";

            using (var command = new SqlCommand(sql, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
