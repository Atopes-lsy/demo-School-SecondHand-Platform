using System;

namespace School二手Platform.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public ProductCondition Condition { get; set; }
        public PriceStrategy Strategy { get; set; }
        public int SellerId { get; set; }
        public int? CurrentHighestBidderId { get; set; }
        public ProductStatus Status { get; set; } = ProductStatus.Available;
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
