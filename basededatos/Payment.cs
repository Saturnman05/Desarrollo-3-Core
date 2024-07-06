using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Core;

namespace basededatos
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public static void AddPayment (SqlConnection con, Payment payment)
        {
            string sql = "INSERT INTO payments (order_id, amount, date, status) VALUES (@order_id, @amount, @date, @status)";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@order_id", payment.OrderId);
                command.Parameters.AddWithValue("@amount", payment.Amount);
                command.Parameters.AddWithValue("@date", payment.Date);
                command.Parameters.AddWithValue("@status", payment.Status);
                command.ExecuteNonQuery();
            }
        }

        public static Payment GetPayment (SqlConnection con, int paymentId)
        {
            Payment payment = new Payment();

            string sql = $"SELECT * FROM products WHERE id = {paymentId}";

            using (var command = new SqlCommand(sql, con))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    payment.Id = paymentId;
                    payment.OrderId = int.Parse(reader["order_id"].ToString());
                    payment.Amount = int.Parse(reader["amount"].ToString());
                    payment.Date = DateTime.Parse(reader["date"].ToString());
                    payment.Status = reader["status"].ToString();
                }
            }

            return payment;
        }

        public static void DeletePayment (SqlConnection con, int paymentId)
        {
            string sql = "DELETE FROM orders WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", paymentId);
                command.ExecuteNonQuery();
            }
        }
    }
}
