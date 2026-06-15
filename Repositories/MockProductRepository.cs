using System.Collections.Generic;
using System.Linq;
using School二手Platform.Models;

namespace School二手Platform.Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private static readonly List<Product> _products;
        private static int _nextId = 1;

        static MockProductRepository()
        {
            _products = new List<Product>
            {
                new Product
                {
                    Id = _nextId++, Title = "高等数学（同济第七版）考研必备",
                    Description = "九成新，只有前两章有轻微笔记，其余全新。考研党必入！",
                    Price = 25.00m, Category = "课本图书",
                    Condition = ProductCondition.MoreThanHalf,
                    Strategy = PriceStrategy.Fixed,
                    SellerId = 101, ImagePath = "/Assets/book_math.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "超大收纳盒三件套",
                    Description = "毕业季清仓，超大容量，可收纳衣物、杂物。轻微使用痕迹。",
                    Price = 15.00m, Category = "生活用品",
                    Condition = ProductCondition.MoreThanHalf,
                    Strategy = PriceStrategy.Fixed,
                    SellerId = 102, ImagePath = "/Assets/box.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "罗技G502游戏鼠标",
                    Description = "用了半年，换了新鼠标所以出掉。功能完好，箱说全。",
                    Price = 80.00m, Category = "电子产品",
                    Condition = ProductCondition.MoreThanHalf,
                    Strategy = PriceStrategy.HighestBidder,
                    SellerId = 103, ImagePath = "/Assets/mouse.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "进口临期零食大礼包（6月底到期）",
                    Description = "朋友送的零食大礼包，吃不完。内含薯片、饼干、巧克力共12包。6月30日到期，介意勿拍。",
                    Price = 20.00m, Category = "食品",
                    Condition = ProductCondition.NearExpiry,
                    Strategy = PriceStrategy.HighestBidder,
                    SellerId = 104, ImagePath = "/Assets/snacks.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "B站大会员年卡兑换码",
                    Description = "买错了，出给需要的同学。官方正版兑换码，秒到账。",
                    Price = 88.00m, Category = "虚拟产品",
                    Condition = ProductCondition.New,
                    Strategy = PriceStrategy.Fixed,
                    SellerId = 105, ImagePath = "/Assets/bilibili.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "沙宣洗发水+护发素套装（余量小于一半）",
                    Description = "用了大概三分之一，不太适合我的发质，便宜出了。",
                    Price = 12.00m, Category = "生活用品",
                    Condition = ProductCondition.LessThanHalf,
                    Strategy = PriceStrategy.Fixed,
                    SellerId = 102, ImagePath = "/Assets/shampoo.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "iPad Air 5 64G 深空灰",
                    Description = "去年购入，无磕碰无划痕，一直带壳贴膜使用。配件齐全，带原装充电器。",
                    Price = 2200.00m, Category = "电子产品",
                    Condition = ProductCondition.New,
                    Strategy = PriceStrategy.HighestBidder,
                    SellerId = 106, ImagePath = "/Assets/ipad.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "全新未拆封考研英语真题（2025版）",
                    Description = "买重了，全新未拆封，给需要的考研同学。",
                    Price = 35.00m, Category = "课本图书",
                    Condition = ProductCondition.New,
                    Strategy = PriceStrategy.Fixed,
                    SellerId = 101, ImagePath = "/Assets/english.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "半箱康师傅红烧牛肉面",
                    Description = "买了一箱吃不完，还剩半箱大概12包。保质期到今年10月。",
                    Price = 18.00m, Category = "食品",
                    Condition = ProductCondition.LessThanHalf,
                    Strategy = PriceStrategy.Fixed,
                    SellerId = 107, ImagePath = "/Assets/noodles.png"
                },
                new Product
                {
                    Id = _nextId++, Title = "校园网账号代充（100M带宽）",
                    Description = "学长留下的账号，5月毕业前可用。100M带宽，看视频打游戏无压力。",
                    Price = 50.00m, Category = "虚拟产品",
                    Condition = ProductCondition.NearExpiry,
                    Strategy = PriceStrategy.HighestBidder,
                    SellerId = 108, ImagePath = "/Assets/network.png"
                }
            };
        }

        public void AddProduct(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing != null)
            {
                var index = _products.IndexOf(existing);
                _products[index] = product;
            }
        }

        public Product? GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetFilteredProducts(string? keyword, string? category, ProductCondition? condition, PriceStrategy? strategy)
        {
            var query = _products.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(p =>
                    p.Title.Contains(keyword) || p.Description.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(category) && category != "全部")
            {
                query = query.Where(p => p.Category == category);
            }

            if (condition.HasValue)
            {
                query = query.Where(p => p.Condition == condition.Value);
            }

            if (strategy.HasValue)
            {
                query = query.Where(p => p.Strategy == strategy.Value);
            }

            return query.ToList();
        }

        public List<Product> GetProductsBySeller(int sellerId)
        {
            return _products.Where(p => p.SellerId == sellerId).ToList();
        }

        public List<Product> GetAllProducts()
        {
            return _products.ToList();
        }
    }
}
