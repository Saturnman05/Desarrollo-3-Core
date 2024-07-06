using basededatos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Xml.Linq;

namespace Core
{
    public class Program
    {
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
            string sql = "INSERT INTO products (name, description, price, stock) VALUES (@name, @description, @price, @stock)";

            if (product == null)
            {
                Console.Write("Nombre del producto: ");
                string name = Console.ReadLine();
                Console.Write("Descripción del producto: ");
                string description = Console.ReadLine();
                Console.Write("Precio del producto: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Cantidad en stock: ");
                int stock = Convert.ToInt32(Console.ReadLine());

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Producto añadido.");
                return;
            }

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.ExecuteNonQuery();
            }
        }

        public static Product UpdateProduct(SqlConnection connection, Product product)
        {
            string sql = "UPDATE products SET name = @name, description = @description, price = @price, stock = @stock WHERE id = @id";

            if (product == null)
            {
                Console.Write("ID del producto a actualizar: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.Write("Nuevo nombre del producto: ");
                string name = Console.ReadLine();
                Console.Write("Nueva descripción del producto: ");
                string description = Console.ReadLine();
                Console.Write("Nuevo precio del producto: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Nueva cantidad en stock: ");
                int stock = Convert.ToInt32(Console.ReadLine());

                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@stock", stock);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Producto actualizado.");
                return product;
            }

            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", product.Name);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@price", product.Price);
                command.Parameters.AddWithValue("@stock", product.Stock);
                command.Parameters.AddWithValue("@id", product.Id);
                command.ExecuteNonQuery();
            }

            return product;
        }

        public static void DeleteProduct(SqlConnection connection, Product product)
        {
            string sql = "DELETE FROM products WHERE id = @id";

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

            string sql = $"SELECT * FROM products WHERE id = {id}";

            using (var command = new SqlCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read()) {
                    product.Id = id;
                    product.Name = reader["name"].ToString();
                    product.Description = reader["description"].ToString();
                    product.Price = decimal.Parse(reader["price"].ToString());
                    product.Stock = int.Parse(reader["stock"].ToString());
                }
            }

            return product;
        }

        static void Main(string[] args)
        {
            string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";

            using (var con = new SqlConnection(connstring))
            {
                con.Open();
                while (true)
                {
                    Console.WriteLine("\nGestión de la Tienda:");
                    Console.WriteLine("1. Gestión de Productos");
                    Console.WriteLine("2. Salir");
                    Console.Write("Seleccione una opción: ");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        while (true)
                        {
                            Console.WriteLine("\nGestión de Productos:");
                            Console.WriteLine("1. Obtener lista de productos");
                            Console.WriteLine("2. Añadir un nuevo producto");
                            Console.WriteLine("3. Actualizar un producto existente");
                            Console.WriteLine("4. Eliminar un producto");
                            Console.WriteLine("5. Volver al menú principal");
                            Console.Write("Seleccione una opción: ");
                            int productChoice = Convert.ToInt32(Console.ReadLine());

                            if (productChoice == 1)
                            {
                                ListProducts(con);
                            }
                            else if (productChoice == 2)
                            {
                                AddProduct(con, null);
                            }
                            else if (productChoice == 3)
                            {
                                UpdateProduct(con, null);
                            }
                            else if (productChoice == 4)
                            {
                                DeleteProduct(con, null);
                            }
                            else if (productChoice == 5)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Opción no válida.");
                            }
                        }
                    }
                    else if (choice == 2)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Opción no válida.");
                    }
                }
            }
        }
    }
}
