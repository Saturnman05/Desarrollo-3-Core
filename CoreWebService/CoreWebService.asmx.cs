using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Services;
using System.Data.SQLite;
using Core;
using basededatos;

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

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<Product> ListaProductos ()
        {
            using (var connection = new SQLiteConnection("Data Source=:memory:;Version=3;New=True;"))
            {
                connection.Open();
                Core.Program.CreateTables(connection);
                Product produt = new Product() {
                    Name = "foo",
                    Description = "bar",
                    Price = "12",
                    Stock = 12,
                };

                Core.Program.AddProduct(connection, produt);

                return Core.Program.ListProducts(connection);
            }
        }
    }
}
