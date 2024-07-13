using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Core
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public static void AddPayment (SqlConnection con, Payment payment)
        {
            int id = 1;
            string prevSql = "SELECT id FROM payments ORDER BY id DESC;";
            using (var command = new SqlCommand(prevSql, con))
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                    id = int.Parse(reader["id"].ToString());
            }


            string sql = "INSERT INTO payments (id, order_id, order_number, amount, status) VALUES (@id, @order_id, @order_number, @amount, @status)";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", ++id);
                command.Parameters.AddWithValue("@order_id", payment.OrderId);
                command.Parameters.AddWithValue("@order_number", payment.OrderNumber);
                command.Parameters.AddWithValue("@amount", payment.Amount);
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
                    payment.OrderNumber = reader["order_number"].ToString();
                    payment.Amount = int.Parse(reader["amount"].ToString());
                    payment.Date = DateTime.Parse(reader["date"].ToString());
                    payment.Status = reader["status"].ToString();
                }
            }

            return payment;
        }

        public static void DeletePayment (SqlConnection con, int paymentId)
        {
            string sql = "DELETE FROM payments WHERE id = @id";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@id", paymentId);
                command.ExecuteNonQuery();
            }
        }

        public static List<Payment> ListPayments (SqlConnection con)
        {
            List<Payment> paymentList = new List<Payment>();
            string sql = "SELECT * FROM payments";

            using (var command = new SqlCommand(sql, con))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Payment payment = new Payment()
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            OrderId = int.Parse(reader["order_id"].ToString()),
                            OrderNumber = reader["order_number"].ToString(),
                            Amount = decimal.Parse(reader["amount"].ToString()),
                            Date = DateTime.Parse(reader["date"].ToString()),
                            Status = reader["status"].ToString(),
                        };

                        paymentList.Add(payment);
                    }
                }
            }

            return paymentList;
        }

        public static void UpdatePayment(SqlConnection con, Payment payment) {
            string sql = "UPDATE payments SET [order_id] = @order_id, [amount] = @amount, [date] = @date, [status] = @status WHERE order_number = @order_number";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@order_id", payment.OrderId);
                command.Parameters.AddWithValue("@order_number", payment.OrderNumber);
                command.Parameters.AddWithValue("@amount", payment.Amount);
                command.Parameters.AddWithValue("@date", payment.Date);
                command.Parameters.AddWithValue("@status", payment.Status);
                command.ExecuteNonQuery();
            }
        }

        public static List<Payment> GetPaymentByNumeroOrden (SqlConnection con, string numeroOrden)
        {
            List<Payment> paymentList = new List<Payment>();
            string sql = "SELECT * FROM payments WHERE order_number = @order_number";

            using (var command = new SqlCommand(sql, con))
            {
                command.Parameters.AddWithValue("@order_number", numeroOrden);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Payment payment = new Payment()
                        {
                            Id = int.Parse(reader["id"].ToString()),
                            OrderId = int.Parse(reader["order_id"].ToString()),
                            OrderNumber = reader["order_number"].ToString(),
                            Amount = decimal.Parse(reader["amount"].ToString()),
                            Date = DateTime.Parse(reader["date"].ToString()),
                            Status = reader["status"].ToString(),
                        };

                        paymentList.Add(payment);
                    }
                }
            }

            return paymentList;
        }
    }
}
