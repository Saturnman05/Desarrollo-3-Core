using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
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

        public static void DeleteUser (SqlConnection con, int userId)
        {
            string sql = "DELETE FROM users WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", userId);
                command.ExecuteNonQuery();
            }
        }

        public static User LogIn (SqlConnection con, string username, string password)
        {
            Core.User user = new Core.User();
            string sql = $"SELECT * FROM users WHERE username = @username AND password = @password";

            try
            {
                using (var command = new SqlCommand(sql, con))
                using (var reader = command.ExecuteReader())
                {
                    user.Id = int.Parse(reader["id"].ToString());
                    user.Username = reader["username"].ToString();
                    user.Password = reader["password"].ToString();
                } 
            }
            catch (Exception)
            {

                user.Id = 0;
                user.Username = "0";
                user.Password = "0";
            }

            return user;
        }
    }
}
