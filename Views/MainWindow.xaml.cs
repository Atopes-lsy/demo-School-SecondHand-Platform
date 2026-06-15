using System.Windows;
using System.Windows.Input;
using School二手Platform.Models;
using School二手Platform.ViewModels;

namespace School二手Platform.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void ProductCard_Click(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is Product product)
            {
                // Check if the click originated from the heart button — skip if so
                if (e.OriginalSource is FrameworkElement source &&
                    (source.Name == "favIcon" || source.Parent is FrameworkElement pe && pe.Name == "favIcon"))
                    return;

                _viewModel.SelectedProduct = product;
            }
        }

        private void FavoriteToggle_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.Tag is Product product)
            {
                _viewModel.ToggleFavoriteCommand.Execute(product);
                e.Handled = true;
            }
        }
    }
}
