using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using School二手Platform.Models;
using School二手Platform.Repositories;

namespace School二手Platform.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IFavoriteRepository _favoriteRepo;
        private HashSet<int> _favoriteIds = new();

        public ObservableCollection<Product> FilteredProducts { get; } = new();

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set { if (SetProperty(ref _searchText, value)) ApplyFilters(); }
        }

        private string _selectedCategory = "全部";
        public string SelectedCategory
        {
            get => _selectedCategory;
            set { if (SetProperty(ref _selectedCategory, value)) ApplyFilters(); }
        }

        public List<string> Categories { get; } = new()
        {
            "全部", "课本图书", "生活用品", "电子产品", "食品", "虚拟产品"
        };

        private ProductCondition? _selectedCondition;
        public ProductCondition? SelectedCondition
        {
            get => _selectedCondition;
            set { if (SetProperty(ref _selectedCondition, value)) ApplyFilters(); }
        }

        public List<ProductCondition> ConditionOptions { get; } = new()
        {
            ProductCondition.New, ProductCondition.NearExpiry,
            ProductCondition.MoreThanHalf, ProductCondition.LessThanHalf
        };

        private PriceStrategy? _selectedStrategy;
        public PriceStrategy? SelectedStrategy
        {
            get => _selectedStrategy;
            set { if (SetProperty(ref _selectedStrategy, value)) ApplyFilters(); }
        }

        public List<PriceStrategy> StrategyOptions { get; } = new()
        {
            PriceStrategy.Fixed, PriceStrategy.HighestBidder
        };

        private bool _showFavoritesOnly;
        public bool ShowFavoritesOnly
        {
            get => _showFavoritesOnly;
            set { if (SetProperty(ref _showFavoritesOnly, value)) ApplyFilters(); }
        }

        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                if (SetProperty(ref _selectedProduct, value) && value != null)
                {
                    OpenDetail(value);
                }
            }
        }

        public RelayCommand SearchCommand { get; }
        public RelayCommand PublishNewProductCommand { get; }
        public RelayCommand ResetFiltersCommand { get; }
        public RelayCommand FavoriteFilterCommand { get; }
        public RelayCommand<Product> ToggleFavoriteCommand { get; }

        public MainViewModel() : this(new MockProductRepository(), MockFavoriteRepository.Instance) { }

        public MainViewModel(IProductRepository productRepo, IFavoriteRepository favoriteRepo)
        {
            _productRepo = productRepo;
            _favoriteRepo = favoriteRepo;

            SearchCommand = new RelayCommand(ApplyFilters);
            PublishNewProductCommand = new RelayCommand(OpenPublishWindow);
            ResetFiltersCommand = new RelayCommand(ResetFilters);
            FavoriteFilterCommand = new RelayCommand(ToggleFavoriteFilter);
            ToggleFavoriteCommand = new RelayCommand<Product>(ToggleFavorite);

            RefreshFavorites();
            ApplyFilters();
        }

        private void RefreshFavorites()
        {
            _favoriteIds = new HashSet<int>(_favoriteRepo.GetFavoriteProductIds(999));
        }

        public bool IsFavorited(Product p)
        {
            return _favoriteIds.Contains(p.Id);
        }

        private void ToggleFavorite(Product? p)
        {
            if (p == null) return;
            _favoriteRepo.ToggleFavorite(999, p.Id);
            RefreshFavorites();
            ApplyFilters();
        }

        private void ToggleFavoriteFilter()
        {
            ShowFavoritesOnly = !ShowFavoritesOnly;
        }

        public void ApplyFilters()
        {
            var products = _productRepo.GetFilteredProducts(
                string.IsNullOrWhiteSpace(SearchText) ? null : SearchText,
                SelectedCategory == "全部" ? null : SelectedCategory,
                SelectedCondition,
                SelectedStrategy);

            if (ShowFavoritesOnly)
                products = products.Where(p => _favoriteIds.Contains(p.Id)).ToList();

            FilteredProducts.Clear();
            foreach (var p in products)
                FilteredProducts.Add(p);
        }

        private void ResetFilters()
        {
            SearchText = string.Empty;
            SelectedCategory = "全部";
            SelectedCondition = null;
            SelectedStrategy = null;
            ShowFavoritesOnly = false;
            ApplyFilters();
        }

        private void OpenDetail(Product product)
        {
            var detailVm = new ProductDetailViewModel(product, _productRepo, _favoriteRepo);
            var window = new Views.ProductDetailWindow(detailVm);
            window.Owner = Application.Current.MainWindow;
            if (window.ShowDialog() == true)
            {
                RefreshFavorites();
                ApplyFilters();
            }
            else
            {
                RefreshFavorites();
                ApplyFilters();
            }
        }

        private void OpenPublishWindow()
        {
            var publishVm = new PublishProductViewModel(_productRepo);
            var window = new Views.PublishProductWindow(publishVm);
            window.Owner = Application.Current.MainWindow;
            if (window.ShowDialog() == true)
            {
                ApplyFilters();
            }
        }
    }
}
