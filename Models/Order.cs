using System;

namespace School二手Platform.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public decimal FinalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
