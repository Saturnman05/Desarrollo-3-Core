using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using Core;
using basededatos;
using System.Data.SqlClient;

namespace CoreWebService
{
    /// <summary>
    /// Summary description for CoreWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CoreWebService : System.Web.Services.WebService
    {
        string connstring = "Data Source = DESKTOP-MFFG200;Initial Catalog=CoreDB;Integrated Security=true";

        [WebMethod]
        public List<Product> ListaProductos ()
        {
            using (var connection = new SqlConnection(connstring))
            {
                connection.Open();
                return Program.ListProducts(connection);
            }
        }

        [WebMethod]
        public void AñadirProducto (string name, string description, string price, int stock)
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
    }
}
