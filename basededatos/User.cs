using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basededatos
{
    public class User
    {
        public int Id { get; set; }
        public string Username {  get; set; }
        public string Password { get; set; }
        public int Rol {  get; set; }

        public static void AddUser (SqlConnection con,  User user)
        {
            string sql = "INSERT INTO users (username, password, Rol) VALUES (@username, @password, @Rol)";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@Rol", user.Rol);
                command.ExecuteNonQuery();
            }
        }

        public static User GetUser(SqlConnection con, int userId)
        {
            User user = new User();

            string sql = $"SELECT * FROM users WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", userId);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user.Id = userId;
                        user.Username = reader["username"].ToString();
                        user.Password = reader["password"].ToString();
                        user.Rol = int.Parse(reader["Rol"].ToString());
                    }
                }
            }

            return user;
        }

        public static void UpdateUser (SqlConnection con, User user)
        {
            string sql = "UPDATE users SET username = @username, password = @password, Rol = @Rol WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@Rol", user.Rol);
                command.Parameters.AddWithValue("@id", user.Id);
                command.ExecuteNonQuery();
            }
        }

        public static void DeleteUser (SqlConnection con, User user)
        {
            string sql = "DELETE FROM users WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", user.Id);
                command.ExecuteNonQuery();
            }
        }
    }
}
