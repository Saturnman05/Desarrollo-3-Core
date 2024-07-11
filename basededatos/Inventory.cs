using Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public static void AddItem(SqlConnection con, Inventory item)
        {
            string sql = "INSERT INTO inventory (product_id, quantity) VALUES (@product_id, @quantity)";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@product_id", item.ProductId);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                command.ExecuteNonQuery();
            }
        }

        public static List<Inventory> ListItems (SqlConnection con)
        {
            List<Inventory> inventory = new List<Inventory>();

            string sql = "SELECT * FROM inventory";
            using (var command = new SqlCommand(sql, con))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    int productId = int.Parse(reader["product_id"].ToString());
                    int quantity = int.Parse(reader["quantity"].ToString());

                    Inventory item = new Inventory()
                    {
                        Id = id,
                        ProductId = productId,
                        Quantity = quantity
                    };

                    inventory.Add(item);
                }
            }

            return inventory;
        }

        public static void UpdateInventory (SqlConnection con, Inventory item)
        {
            string sql = "UPDATE inventory SET product_id = @product_id, quantity = @quantity WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@product_id", item.ProductId);
                command.Parameters.AddWithValue("@quantity", item.Quantity);
                command.Parameters.AddWithValue("@id", item.Id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteItem (SqlConnection con, int itemId)
        {
            string sql = "DELETE FROM inventory WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", itemId);
                command.ExecuteNonQuery();
            }
        }

        public static Inventory GetItem(SqlConnection con, int id)
        {
            Inventory item = new Inventory() { Id = 0, ProductId = 0, Quantity = 0};

            string sql = $"SELECT * FROM inventory WHERE [id] = {id}";

            using (var command = new SqlCommand(sql, con))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    item.Id = id;
                    item.ProductId = int.Parse(reader["product_id"].ToString());
                    item.Quantity = int.Parse(reader["quantity"].ToString());
                }
            }

            return item;
        }

        public static int GetItemId (SqlConnection con, Product product)
        {
            int id = 0;

            string sql = "SELECT inventory.id FROM inventory JOIN products ON inventory.product_id = products.id WHERE product_id = @product_id";

            using (var command = new SqlCommand(sql, con)) 
            {
                command.Parameters.AddWithValue("@product_id", product.Id);
                id = Convert.ToInt32(command.ExecuteScalar());
            }

            return id;
        }
    }
}
