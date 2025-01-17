﻿namespace CoreAPI.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
    }
}
