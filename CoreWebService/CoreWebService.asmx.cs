using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using Core;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Xml.Linq;
using System.Data.Odbc;

namespace CoreWebService
{
    /// <summary>
    /// Summary description for CoreWebService
    /// </summary>
    [WebService(Namespace = "http://core.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CoreWebService : System.Web.Services.WebService
    {
        // private string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";

        // Usuario
        [WebMethod]
        public void PostUser (string username, string password, int rol)
        {
            using (var connection = new SqlConnection(Program.ConnString))
            {
                connection.Open();

                User user = new User()
                {
                    Username = username,
                    Password = password,
                    Rol = rol
                };

                Core.User.AddUser(connection, user);
            }
        }

        [WebMethod]
        public User GetUser (int userId)
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                return Core.User.GetUser(con, userId);
            }
        }

        [WebMethod]
        public void PutUser (int userId, string username, string password, int rol)
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();

                User user = new User()
                {
                    Id = userId,
                    Username = username,
                    Password = password,
                    Rol = rol
                };

                Core.User.UpdateUser(con, user);
            }
        }

        [WebMethod]
        public void DeleteUser (int userId) {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                Core.User.DeleteUser(con, userId);
            }
        }

        [WebMethod]
        public User LogInUser (string username, string password)
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                return Core.User.LogIn(con, username, password);
            }
        }
        
        // Productos
        [WebMethod]
        public List<Product> GetProducts ()
        {
            using (var connection = new SqlConnection(Program.ConnString))
            {
                connection.Open();
                return Product.ListProducts(connection);
            }
        }

        [WebMethod]
        public void PostProducts (string name, string description, decimal price, int stock)
        {
            using (var connection = new SqlConnection(Program.ConnString))
            {
                connection.Open();

                Product product = new Product()
                {
                    Description = description,
                    Name = name,
                    Price = price,
                    Stock = stock
                };

                Product.AddProduct(connection, product);
            }
        }

        [WebMethod]
        public void PutProduct (int id, string name, string description, decimal price, int stock)
        {
            using (var connection = new SqlConnection(Program.ConnString))
            {
                connection.Open();

                Product product = new Product()
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Price = price,
                    Stock = stock
                };

                Product.UpdateProduct(connection, product);
            }
        }

        [WebMethod]
        public void DeleteProduct (int id) 
        {
            using (var connection = new SqlConnection(Program.ConnString))
            {
                connection.Open();
                Product product = new Product { Id = id };
                Product.DeleteProduct(connection, product);
            }
        }

        [WebMethod]
        public Product GetProduct (int id) 
        {
            using (var connection = new SqlConnection(Program.ConnString))
            {
                connection.Open();
                return Product.GetProduct(connection, id);
            }
        }

        // Pedidos
        [WebMethod]
        public void PostOrder (int productId, int quantity, decimal totalPrice, DateTime date) 
        { 
            using (var conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                Order order = new Order()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    TotalPrice = totalPrice,
                    Date = date
                };

                Order.AddOrder(conn, order);
            }
        }

        [WebMethod]
        public List<Order> GetOrders () 
        {
            using (var conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                return Order.ListOrders(conn);
            }
        }

        [WebMethod]
        public void PutOrder (int orderId, int productId, int quantity, decimal totalPrice, DateTime date) 
        {
            using (var conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                Order order = new Order()
                {
                    Id = orderId,
                    ProductId = productId,
                    Quantity = quantity,
                    TotalPrice = totalPrice,
                    Date = date
                };

                Order.UpdateOrder(conn, order);
            }
        }

        [WebMethod]
        public void DeleteOrder (int orderId) 
        {
            using (var conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();
                Order.DeleteOrder(conn, orderId);
            }
        }

        // Pagos
        [WebMethod]
        public void PostPayment (int order_id, decimal amount, DateTime date, string status) 
        {
            using (var conn = new SqlConnection(Program.ConnString))
            {
                conn.Open();

                Payment payment = new Payment() 
                {
                    OrderId = order_id,
                    Amount = amount,
                    Date = date,
                    Status = status
                };

                Payment.AddPayment(conn, payment);
            }
        }

        [WebMethod]
        public Payment GetPayment (int paymentId) 
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                return Payment.GetPayment(con, paymentId);
            }
        }

        [WebMethod]
        public void DeletePayment (int paymentId) 
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                Payment.DeletePayment(con, paymentId);
            }
        }

        // Inventario
        [WebMethod]
        public List<Inventory> GetInventory () 
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                return Inventory.ListItems(con);
            }
        }

        [WebMethod]
        public void PostInventoryItem (int productId, int quantity) 
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();

                Inventory item = new Inventory()
                {
                    ProductId = productId,
                    Quantity = quantity
                };

                Inventory.AddItem(con, item);
            }
        }

        [WebMethod]
        public void PutIventoryItem (int itemId, int productId, int quantity) 
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();

                Inventory item = new Inventory()
                {
                    Id = itemId,
                    ProductId = productId,
                    Quantity = quantity
                };

                Inventory.UpdateInventory(con, item);
            }
        }

        [WebMethod]
        public void DeleteIventoryItem (int itemId) 
        {
            using (var con = new SqlConnection(Program.ConnString))
            {
                con.Open();
                Inventory.DeleteItem(con, itemId);
            }
        }
    }
}
