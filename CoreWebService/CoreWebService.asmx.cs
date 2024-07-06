using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using Core;
using basededatos;
using System.Data.SqlClient;

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
        string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";
        
        // Productos
        [WebMethod]
        public List<Product> GetProducts ()
        {
            using (var connection = new SqlConnection(connstring))
            {
                connection.Open();
                return Program.ListProducts(connection);
            }
        }

        [WebMethod]
        public void PostProducts (string name, string description, decimal price, int stock)
        {
            using (var connection = new SqlConnection(connstring))
            {
                connection.Open();

                Product product = new Product()
                {
                    Description = description,
                    Name = name,
                    Price = price,
                    Stock = stock
                };

                Program.AddProduct(connection, product);
            }

            return;
        }

        [WebMethod]
        public void PutProduct (int id, string name, string description, decimal price, int stock)
        {
            using (var connection = new SqlConnection(connstring))
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

                Program.UpdateProduct(connection, product);
            }
        }

        [WebMethod]
        public void DeleteProduct (int id) 
        {
            using (var connection = new SqlConnection(connstring))
            {
                connection.Open();
                Product product = new Product { Id = id };
                Program.DeleteProduct(connection, product);
            }
        }

        [WebMethod]
        public Product GetProduct (int id) 
        {
            using (var connection = new SqlConnection(connstring))
            {
                connection.Open();
                return Program.GetProduct(connection, id);
            }
        }

        // Pedidos
        [WebMethod]
        public void PostOrder (int productId, int quantity, decimal totalPrice, DateTime date) 
        { 
            using (var conn = new SqlConnection(connstring))
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
        public List<Order> GetOrders() 
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();

                return Order.ListOrders(conn);
            }
        }

        [WebMethod]
        public void PutOrder(int orderId, int productId, int quantity, decimal totalPrice, DateTime date) 
        {
            using (var conn = new SqlConnection(connstring))
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
        public void DeleteOrder(int orderId) 
        {
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                Order.DeleteOrder(conn, orderId);
            }
        }

        // Pagos

        // Inventario
    }
}
