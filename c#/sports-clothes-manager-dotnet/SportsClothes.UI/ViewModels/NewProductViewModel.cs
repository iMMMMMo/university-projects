using SportsClothes.CORE;
using SportsClothes.INTERFACES;
using SportsClothes.UI.Dto;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace SportsClothes.UI.ViewModels
{
    public class NewProductViewModel : ViewModelBase
    {
        public ObservableCollection<ProducerViewModel> Producers { get; set; } = new ObservableCollection<ProducerViewModel>();
        private readonly BL.BL _bl;
        private IProductDto _product;

        public IProductDto Product
        {
            get => _product;
            set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public NewProductViewModel()
        {
            _product = new ProductDto();
            _bl = BL.BL.Instance;
            GetAllProducers();

            _addProductCommand = new RelayCommand(param => AddProduct(), param => CanAddProduct());
        }

        private void GetAllProducers()
        {
            Producers.Clear();
            var producers = _bl.GetProducers().ToList();

            foreach (var producer in producers)
            {
                Producers.Add(new ProducerViewModel(producer));
            }
        }

        private readonly RelayCommand _addProductCommand;
        public RelayCommand AddProductCommand => _addProductCommand;

        private void AddProduct()
        {
            var validationResults = Validate();
            if (validationResults.Any())
                return;

            _bl.CreateProduct(Product);
            ClearForm();
        }

        private void ClearForm()
        {
            Product = new ProductDto();
            Name = string.Empty;
            Price = 0;
            Size = string.Empty;
            Color = string.Empty;
            Type = default;
            NewProductProducer = null;
        }

        private bool CanAddProduct()
        {
            return !string.IsNullOrEmpty(Product.Name);
        }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(25, ErrorMessage = "Name cannot exceed 25 characters")]
        public string Name
        {
            get => _product.Name;
            set
            {
                _product.Name = value;
                OnPropertyChanged("Name");
            }
        }

        private IProducer _newProductProducer;

        [Required(ErrorMessage = "Producer is required")]
        public IProducer NewProductProducer
        {
            get => _newProductProducer;
            set
            {
                _newProductProducer = value;
                if (value != null)
                {
                    _product.ProducerId = value.Id;
                }
                OnPropertyChanged("NewProductProducer");
            }
        }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, 99999, ErrorMessage = "Price must be greater than 0 and smaller than 99999")]
        public double Price
        {
            get => _product.Price;
            set
            {
                _product.Price = value;
                OnPropertyChanged("Price");
            }
        }

        [Required(ErrorMessage = "Size is required")]
        [MaxLength(2, ErrorMessage = "Size cannot exceed 2 characters")]
        public string Size
        {
            get => _product.Size;
            set
            {
                _product.Size = value;
                OnPropertyChanged("Size");
            }
        }

        [Required(ErrorMessage = "Color is required")]
        [MaxLength(25, ErrorMessage = "Color cannot exceed 25 characters")]
        public string Color
        {
            get => _product.Color;
            set
            {
                _product.Color = value;
                OnPropertyChanged("Color");
            }
        }

        public ProductType Type
        {
            get => _product.Type;
            set
            {
                _product.Type = value;
                OnPropertyChanged("Type");
            }
        }

        public IList<ValidationResult> Validate()
        {
            var valContext = new ValidationContext(this, null, null);
            var valResults = new List<ValidationResult>();

            Validator.TryValidateObject(this, valContext, valResults, true);

            foreach (var x in Errors.ToList().Where(x => valResults.All(r => r.MemberNames.All(m => m != x.Key))))
            {
                Errors.Remove(x.Key);
                OnErrorChanged(x.Key);
            }

            var query = from f1 in valResults
                        from f2 in f1.MemberNames
                        group f1 by f2 into g
                        select g;
            foreach (var x in query)
            {
                var messages = x.Select(r => r.ErrorMessage).ToList();
                if (Errors.ContainsKey(x.Key))
                {
                    Errors.Remove(x.Key);
                }

                Errors.Add(x.Key, messages);
                OnErrorChanged(x.Key);
            }

            return valResults;
        }
    }
}
