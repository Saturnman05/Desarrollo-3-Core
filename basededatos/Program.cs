using System;
using System.Data.SQLite;

class Program
{
    static void ExecuteSQL(SQLiteConnection connection, string sql)
    {
        using (var command = new SQLiteCommand(sql, connection))
        {
            command.ExecuteNonQuery();
        }
    }

    static void CreateTables(SQLiteConnection connection)
    {
        string createProductsTable = @"
            CREATE TABLE IF NOT EXISTS products (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                name TEXT,
                description TEXT,
                price REAL,
                stock INTEGER
            );";

        string createOrdersTable = @"
            CREATE TABLE IF NOT EXISTS orders (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                product_id INTEGER,
                quantity INTEGER,
                total_price REAL,
                date TEXT,
                FOREIGN KEY(product_id) REFERENCES products(id)
            );";

        string createPaymentsTable = @"
            CREATE TABLE IF NOT EXISTS payments (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                order_id INTEGER,
                amount REAL,
                date TEXT,
                status TEXT,
                FOREIGN KEY(order_id) REFERENCES orders(id)
            );";

        string createInventoryTable = @"
            CREATE TABLE IF NOT EXISTS inventory (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                product_id INTEGER,
                quantity INTEGER,
                FOREIGN KEY(product_id) REFERENCES products(id)
            );";

        ExecuteSQL(connection, createProductsTable);
        ExecuteSQL(connection, createOrdersTable);
        ExecuteSQL(connection, createPaymentsTable);
        ExecuteSQL(connection, createInventoryTable);
    }

    static void AddProduct(SQLiteConnection connection)
    {
        Console.Write("Nombre del producto: ");
        string name = Console.ReadLine();
        Console.Write("Descripción del producto: ");
        string description = Console.ReadLine();
        Console.Write("Precio del producto: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Cantidad en stock: ");
        int stock = Convert.ToInt32(Console.ReadLine());

        string sql = "INSERT INTO products (name, description, price, stock) VALUES (@name, @description, @price, @stock)";
        using (var command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@stock", stock);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Producto añadido.");
    }

    static void ListProducts(SQLiteConnection connection)
    {
        string sql = "SELECT * FROM products";
        using (var command = new SQLiteCommand(sql, connection))
        using (var reader = command.ExecuteReader())
        {
            Console.WriteLine("Lista de Productos:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["id"]}: {reader["name"]} - {reader["description"]} - ${reader["price"]} - Stock: {reader["stock"]}");
            }
        }
    }

    static void UpdateProduct(SQLiteConnection connection)
    {
        Console.Write("ID del producto a actualizar: ");
        int id = Convert.ToInt32(Console.ReadLine());
        Console.Write("Nuevo nombre del producto: ");
        string name = Console.ReadLine();
        Console.Write("Nueva descripción del producto: ");
        string description = Console.ReadLine();
        Console.Write("Nuevo precio del producto: ");
        double price = Convert.ToDouble(Console.ReadLine());
        Console.Write("Nueva cantidad en stock: ");
        int stock = Convert.ToInt32(Console.ReadLine());

        string sql = "UPDATE products SET name = @name, description = @description, price = @price, stock = @stock WHERE id = @id";
        using (var command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@description", description);
            command.Parameters.AddWithValue("@price", price);
            command.Parameters.AddWithValue("@stock", stock);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Producto actualizado.");
    }

    static void DeleteProduct(SQLiteConnection connection)
    {
        Console.Write("ID del producto a eliminar: ");
        int id = Convert.ToInt32(Console.ReadLine());

        string sql = "DELETE FROM products WHERE id = @id";
        using (var command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Producto eliminado.");
    }

    static void Main(string[] args)
    {
        using (var connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;"))
        {
            connection.Open();
            CreateTables(connection);

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
                            ListProducts(connection);
                        }
                        else if (productChoice == 2)
                        {
                            AddProduct(connection);
                        }
                        else if (productChoice == 3)
                        {
                            UpdateProduct(connection);
                        }
                        else if (productChoice == 4)
                        {
                            DeleteProduct(connection);
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
