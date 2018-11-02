using Neptuo.Navigation.TestsApp.Wpf.Models;
using Neptuo.Navigation.TestsApp.Wpf.ViewModels;
using Neptuo.Navigation.Views;
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
        private readonly IViewContext<List<Guid>> viewContext;

        public ProductListWindow(ProductListViewModel viewModel, IViewContext<List<Guid>> viewContext)
        {
            Ensure.NotNull(viewModel, "viewModel");
            this.viewContext = viewContext;

            InitializeComponent();

            DataContext = viewModel;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            var selectIds = lvwItems.SelectedItems.OfType<Product>().Select(p => p.Id).ToList();
            viewContext.Close(selectIds);
        }
    }
}
