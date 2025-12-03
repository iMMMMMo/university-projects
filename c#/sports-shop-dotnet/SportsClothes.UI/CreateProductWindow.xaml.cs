using SportsClothes.INTERFACES;
using System.Collections.ObjectModel;
using System.Windows;

namespace SportsClothes.UI
{
    /// <summary>
    /// Interaction logic for CreateProductWindow.xaml
    /// </summary>
    public partial class CreateProductWindow : Window
    {
        private readonly ObservableCollection<IProducer> _producersData = new ObservableCollection<IProducer>();

        public CreateProductWindow()
        {
            InitializeComponent();

            foreach (var x in NewProductViewModel.Producers)
            {
                _producersData.Add(x.Producer);
            }

            ProducerComboBox.ItemsSource = _producersData;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
