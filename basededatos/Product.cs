using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price {  get; set; }
        public int Stock { get; set; }

        public static List<Product> ListProducts(SqlConnection connection)
        {
            List<Product> productos = new List<Product>();

            string sql = "SELECT * FROM products";
            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                Console.WriteLine("Lista de Productos:");
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string description = reader["description"].ToString();
                    decimal price = decimal.Parse(reader["price"].ToString());
                    int stock = int.Parse(reader["stock"].ToString());

                    Product product = new Product()
                    {
                        Id = id,
                        Name = name,
                        Description = description,
                        Price = price,
                        Stock = stock
                    };

                    productos.Add(product);

                    Console.WriteLine($"{product.Id}: {product.Name} - {product.Description} - ${product.Price} - Stock: {product.Stock}");
                }
            }

            return productos;
        }

        public static void AddProduct(SqlConnection connection, Product product)
        {
            string otroSql = "SELECT COUNT(*) AS [idPrev] FROM products";
            int id = 0;
            using (var com = new SqlCommand(otroSql, connection))
            {
                using (var reader = com.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = int.Parse(reader["idPrev"].ToString()) + 1;
                    }
                }
            }

            string sql = "INSERT INTO products ([id], [name], [description], [price], [stock]) VALUES (@id, @name, @description, @price, @stock)";

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.ExecuteNonQuery();
            }
        }

        public static void UpdateProduct(SqlConnection connection, Product product)
        {
            string sql = "UPDATE products SET [name] = @name, [description] = @description, [price] = @price, [stock] = @stock WHERE [id] = @id";

            //if (product == null)
            //{
            //    Console.Write("ID del producto a actualizar: ");
            //    int id = Convert.ToInt32(Console.ReadLine());
            //    Console.Write("Nuevo nombre del producto: ");
            //    string name = Console.ReadLine();
            //    Console.Write("Nueva descripción del producto: ");
            //    string description = Console.ReadLine();
            //    Console.Write("Nuevo precio del producto: ");
            //    decimal price = Convert.ToDecimal(Console.ReadLine());
            //    Console.Write("Nueva cantidad en stock: ");
            //    int stock = Convert.ToInt32(Console.ReadLine());

            //    using (var command = new SqlCommand(sql, connection))
            //    {
            //        command.Parameters.AddWithValue("@name", name);
            //        command.Parameters.AddWithValue("@description", description);
            //        command.Parameters.AddWithValue("@price", price);
            //        command.Parameters.AddWithValue("@stock", stock);
            //        command.Parameters.AddWithValue("@id", id);
            //        command.ExecuteNonQuery();
            //    }

            //    Console.WriteLine("Producto actualizado.");
            //}

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@id", product.Id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteProduct(SqlConnection connection, Product product)
        {
            string sql = "DELETE FROM products WHERE [id] = @id";

            if (product == null)
            {
                Console.Write("ID del producto a eliminar: ");
                int id = Convert.ToInt32(Console.ReadLine());

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Producto eliminado.");
            }

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", product.Id);
                command.ExecuteNonQuery();
            }
        }

        public static Product GetProduct(SqlConnection connection, int id)
        {
            Product product = new Product();

            string sql = $"SELECT * FROM products WHERE [id] = {id}";

            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    product.Id = id;
                    product.Name = reader["name"].ToString();
                    product.Description = reader["description"].ToString();
                    product.Price = decimal.Parse(reader["price"].ToString());
                    product.Stock = int.Parse(reader["stock"].ToString());
                }
            }

            return product;
        }
    }
}
