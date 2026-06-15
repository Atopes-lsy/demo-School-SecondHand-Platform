using System.Windows;
using School二手Platform.ViewModels;

namespace School二手Platform.Views
{
    public partial class ProductDetailWindow : Window
    {
        public ProductDetailWindow(ProductDetailViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
