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
                    Console.WriteLine("3. Gestión de Inventario");
                    Console.WriteLine("4. Pagos");
                    Console.WriteLine("5. Orden");
                    Console.WriteLine("6. Salir");
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
                                Product.ListProducts(con);
                            }
                            else if (productChoice == 2)
                            {
                                Product.AddProduct(con, null);
                            }
                            else if (productChoice == 3)
                            {
                                Product.UpdateProduct(con, null);
                            }
                            else if (productChoice == 4)
                            {
                                Product.DeleteProduct(con, null);
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
                    else if (choice == 6)
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
