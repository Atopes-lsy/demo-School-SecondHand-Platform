using System;
using System.Linq;
using System.Windows;
using School二手Platform.Models;
using School二手Platform.Repositories;

namespace School二手Platform.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IFavoriteRepository _favoriteRepo;
        private Product _product;

        public Product Product
        {
            get => _product;
            set => SetProperty(ref _product, value);
        }

        private decimal _bidAmount;
        public decimal BidAmount
        {
            get => _bidAmount;
            set => SetProperty(ref _bidAmount, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        private bool _isFixedPrice;
        public bool IsFixedPrice
        {
            get => _isFixedPrice;
            set => SetProperty(ref _isFixedPrice, value);
        }

        private bool _isHighestBidder;
        public bool IsHighestBidder
        {
            get => _isHighestBidder;
            set => SetProperty(ref _isHighestBidder, value);
        }

        private bool _isFavorited;
        public bool IsFavorited
        {
            get => _isFavorited;
            set => SetProperty(ref _isFavorited, value);
        }

        private bool _success;
        public bool Success
        {
            get => _success;
            set => SetProperty(ref _success, value);
        }

        public RelayCommand CreateOrderCommand { get; }
        public RelayCommand PlaceBidCommand { get; }
        public RelayCommand CloseCommand { get; }
        public RelayCommand ToggleFavoriteCommand { get; }

        public ProductDetailViewModel(Product product, IProductRepository productRepo)
            : this(product, productRepo, new MockOrderRepository(), MockFavoriteRepository.Instance) { }

        public ProductDetailViewModel(Product product, IProductRepository productRepo, IOrderRepository orderRepo)
            : this(product, productRepo, orderRepo, MockFavoriteRepository.Instance) { }

        public ProductDetailViewModel(Product product, IProductRepository productRepo, IFavoriteRepository favoriteRepo)
            : this(product, productRepo, new MockOrderRepository(), favoriteRepo) { }

        public ProductDetailViewModel(Product product, IProductRepository productRepo, IOrderRepository orderRepo, IFavoriteRepository favoriteRepo)
        {
            _product = product;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
            _favoriteRepo = favoriteRepo;

            IsFixedPrice = product.Strategy == PriceStrategy.Fixed;
            IsHighestBidder = product.Strategy == PriceStrategy.HighestBidder;
            BidAmount = product.Price + 1;
            IsFavorited = _favoriteRepo.IsFavorited(999, product.Id);

            CreateOrderCommand = new RelayCommand(CreateOrder, () => IsFixedPrice);
            PlaceBidCommand = new RelayCommand(PlaceBid, () => IsHighestBidder && BidAmount > Product.Price);
            CloseCommand = new RelayCommand(Close);
            ToggleFavoriteCommand = new RelayCommand(ToggleFavorite);
        }

        private void ToggleFavorite()
        {
            _favoriteRepo.ToggleFavorite(999, Product.Id);
            IsFavorited = _favoriteRepo.IsFavorited(999, Product.Id);
        }

        private void CreateOrder()
        {
            if (Product.Status != ProductStatus.Available)
            {
                StatusMessage = "该商品已被预订或售出";
                return;
            }

            var order = new Order
            {
                ProductId = Product.Id,
                BuyerId = 999, // Demo current user
                SellerId = Product.SellerId,
                FinalPrice = Product.Price,
                Status = "Pending"
            };

            _orderRepo.CreateOrder(order);

            Product.Status = ProductStatus.Reserved;
            _productRepo.UpdateProduct(Product);

            StatusMessage = $"订单已生成！订单号：{order.Id}，请与卖家联系面交。";
            Success = true;
        }

        private void PlaceBid()
        {
            if (Product.Status != ProductStatus.Available)
            {
                StatusMessage = "该商品已被预订或售出";
                return;
            }

            if (BidAmount <= Product.Price)
            {
                StatusMessage = $"出价必须高于当前最高出价 {Product.Price:C}";
                return;
            }

            // Update product with new highest bid
            Product.Price = BidAmount;
            Product.CurrentHighestBidderId = 999; // Demo current user
            _productRepo.UpdateProduct(Product);

            StatusMessage = $"出价成功！当前最高出价：{Product.Price:C}";
            OnPropertyChanged(nameof(Product));

            // Suggest next bid
            BidAmount = Product.Price + 5;
        }

        private void Close()
        {
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this);
            window!.DialogResult = Success;
            window.Close();
        }
    }
}
