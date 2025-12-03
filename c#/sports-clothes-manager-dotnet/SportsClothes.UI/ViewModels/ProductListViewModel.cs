using System.Collections.ObjectModel;
using SportsClothes.UI.Dto;
using SportsClothes.INTERFACES;

namespace SportsClothes.UI.ViewModels
{
    public class ProductListViewModel : ViewModelBase
    {
        public ObservableCollection<ProductViewModel> Products { get; set; } = new ObservableCollection<ProductViewModel>();
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();

        private readonly RelayCommand _filterDataCommand;
        private readonly RelayCommand _clearFiltersCommand;

        public RelayCommand FilterDataCommand => _filterDataCommand;
        public RelayCommand ClearFiltersCommand => _clearFiltersCommand;

        public IProductFilter FilterValue { get; set; }
        public IList<ProducerData> FilterProducers { get; set; }

        private readonly BL.BL _bl;

        public ProductListViewModel()
        {
            _bl = BL.BL.Instance;
            FilterValue = new ProductFilter();
            GetAllProducts();
            GetAllProducers();
            OnPropertyChanged("Products");

            _filterDataCommand = new RelayCommand(param => FilterData());
            _clearFiltersCommand = new RelayCommand(param => ClearFilters());

            _saveProductCommand = new RelayCommand(param => SaveProduct(), param => CanSaveProduct());
            _deleteProductCommand = new RelayCommand(param => DeleteProduct(), param => CanDeleteProduct());
        }

        public void GetAllProducts()
        {
            Products.Clear();
            var products = _bl.GetProducts().ToList();

            foreach (var product in products)
            {
                Products.Add(new ProductViewModel(product));
            }
        }

        public void GetAllProducers()
        {
            Producers.Clear();
            var producers = _bl.GetProducers().ToList();

            var producersData = producers.Select(p => new ProducerData
            {
                Id = p.Id,
                Name = p.Name
            });

            var emptyProducer = new ProducerData { Id = 0, Name = "All producers" };

            FilterProducers = new List<ProducerData>(producersData);
            FilterProducers.Insert(0, emptyProducer);

            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }

        private void FilterData()
        {
            FilterValue.Type = FilterValue.Type.Equals("System.Windows.Controls.ComboBoxItem: All types") ? string.Empty : FilterValue.Type;

            Products.Clear();
            var filteredProducts = _bl.GetFilteredProducts(FilterValue).ToList();

            foreach (var product in filteredProducts)
            {
                Products.Add(new ProductViewModel(product));
            }
        }

        private void ClearFilters()
        {
            GetAllProducts();
            FilterValue = new ProductFilter();
            OnPropertyChanged(nameof(FilterValue));
        }

        private ProductViewModel _updatedProduct;

        public ProductViewModel UpdatedProduct
        {
            get => _updatedProduct;
            set
            {
                _updatedProduct = value;
                OnPropertyChanged(nameof(UpdatedProduct));
            }
        }

        private ProductViewModel _selectedProduct;

        public ProductViewModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                UpdatedProduct = value;
                OnPropertyChanged(nameof(UpdatedProduct));
            }
        }

        private RelayCommand _saveProductCommand;
        public RelayCommand SaveProductCommand => _saveProductCommand;

        private void SaveProduct()
        {
            var id = UpdatedProduct.Product.Id;
            var updatedProduct = new ProductDto(
                UpdatedProduct.Product.Name,
                UpdatedProduct.Product.Producer.Id,
                UpdatedProduct.Product.Type,
                UpdatedProduct.Product.Price,
                UpdatedProduct.Product.Size,
                UpdatedProduct.Product.Color
            );

            _bl.UpdateProduct(id, updatedProduct);
        }

        private bool CanSaveProduct()
        {
            return UpdatedProduct is { HasErrors: false };
        }

        private readonly RelayCommand _deleteProductCommand;
        public RelayCommand DeleteProductCommand => _deleteProductCommand;

        private void DeleteProduct()
        {
            if (!Products.Contains(SelectedProduct))
                return;

            _bl.DeleteProduct(SelectedProduct.Product.Id);
            Products.Remove(SelectedProduct);
            SelectedProduct = null;
            UpdatedProduct = null;
        }

        private bool CanDeleteProduct()
        {
            return UpdatedProduct is { HasErrors: false };
        }
    }
}
