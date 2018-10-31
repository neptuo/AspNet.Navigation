using Neptuo;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Neptuo.Navigation.TestsApp.Wpf.Views
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public ProductListWindow(ProductListViewModel viewModel)
        {
            Ensure.NotNull(viewModel, "viewModel");

            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
