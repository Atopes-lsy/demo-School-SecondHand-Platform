using System.Windows;
using School二手Platform.ViewModels;

namespace School二手Platform.Views
{
    public partial class PublishProductWindow : Window
    {
        public PublishProductWindow(PublishProductViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
