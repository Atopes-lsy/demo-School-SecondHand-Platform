using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using School二手Platform.Models;
using School二手Platform.Repositories;

namespace School二手Platform.ViewModels
{
    public class PublishProductViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepo;

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        private string _selectedCategory = "课本图书";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set => SetProperty(ref _selectedCategory, value);
        }

        public List<string> Categories { get; } = new()
        {
            "课本图书", "生活用品", "电子产品", "食品", "虚拟产品"
        };

        public List<ProductCondition> ConditionOptions { get; } = new()
        {
            ProductCondition.New, ProductCondition.NearExpiry,
            ProductCondition.MoreThanHalf, ProductCondition.LessThanHalf
        };

        private ProductCondition _selectedCondition = ProductCondition.New;
        public ProductCondition SelectedCondition
        {
            get => _selectedCondition;
            set => SetProperty(ref _selectedCondition, value);
        }

        public List<PriceStrategy> StrategyOptions { get; } = new()
        {
            PriceStrategy.Fixed, PriceStrategy.HighestBidder
        };

        private PriceStrategy _selectedStrategy = PriceStrategy.Fixed;
        public PriceStrategy SelectedStrategy
        {
            get => _selectedStrategy;
            set => SetProperty(ref _selectedStrategy, value);
        }

        private string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set => SetProperty(ref _statusMessage, value);
        }

        public RelayCommand PublishCommand { get; }
        public RelayCommand CancelCommand { get; }

        public PublishProductViewModel(IProductRepository productRepo)
        {
            _productRepo = productRepo;

            PublishCommand = new RelayCommand(Publish, CanPublish);
            CancelCommand = new RelayCommand(Cancel);
        }

        private bool CanPublish()
        {
            return !string.IsNullOrWhiteSpace(Title) && Price > 0;
        }

        private void Publish()
        {
            var product = new Product
            {
                Title = Title.Trim(),
                Description = Description.Trim(),
                Price = Price,
                Category = SelectedCategory,
                Condition = SelectedCondition,
                Strategy = SelectedStrategy,
                SellerId = 999, // Demo current user
                ImagePath = "/Assets/placeholder.png"
            };

            _productRepo.AddProduct(product);
            StatusMessage = $"发布成功！商品「{product.Title}」已上架。";

            // Close after short delay
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this);
            if (window != null)
            {
                window.DialogResult = true;
                window.Close();
            }
        }

        private void Cancel()
        {
            var window = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this);
            if (window != null)
            {
                window.DialogResult = false;
                window.Close();
            }
        }
    }
}
