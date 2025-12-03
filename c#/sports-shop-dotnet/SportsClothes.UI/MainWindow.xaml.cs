using SportsClothes.INTERFACES;
using SportsClothes.UI.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SportsClothes.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ObservableCollection<IProducer> _producersData = new();
        public MainWindow()
        {
            InitializeComponent();

            foreach (var x in ProductListViewModel.Producers)
            {
                _producersData.Add(x.Producer);
            }

            ProducerComboBox.ItemsSource = _producersData;
            Tc.SelectionChanged += TabControl_SelectionChanged;
        }   

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is not TabControl)
                return;

            ProductListViewModel.GetAllProducts();
            _producersData.Clear();
            foreach (var x in ProductListViewModel.Producers)
            {
                _producersData.Add(x.Producer);
            }
        }

        private void CreateProduct(object sender, RoutedEventArgs e)
        {
            var createProductWindow = new CreateProductWindow
            {
                Owner = this
            };

            createProductWindow.Closed += (s, args) => ProductListViewModel.GetAllProducts();
            createProductWindow.ShowDialog();
        }

        private void CreateProducer(object sender, RoutedEventArgs e)
        {
            var createProducerWindow = new CreateProducerWindow
            {
                Owner = this
            };

            createProducerWindow.Closed += (s, args) => ProducerListViewModel.GetAllProducers();
            createProducerWindow.ShowDialog();
        }
    }
}