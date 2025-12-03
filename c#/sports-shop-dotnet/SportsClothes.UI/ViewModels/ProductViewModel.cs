using System.ComponentModel.DataAnnotations;
using SportsClothes.UI.ViewModels;
using SportsClothes.CORE;
using SportsClothes.INTERFACES;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsClothes.UI.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly IProduct _product;
        public IProduct Product => _product;

        public ProductViewModel(IProduct product)
        {
            _product = product;
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get => _product.Name;
            set
            {
                _product.Name = value;
                Validate();
                OnPropertyChanged("Name");
            }
        }

        [Required(ErrorMessage = "Producer is required")]
        public IProducer Producer
        {
            get => _product.Producer;
            set
            {
                _product.Producer = value;
                Validate();
                OnPropertyChanged("Producer");
            }
        }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public double Price
        {
            get => _product.Price;
            set
            {
                _product.Price = value;
                Validate();
                OnPropertyChanged("Price");
            }
        }

        [Required(ErrorMessage = "Type is required")]
        public ProductType Type
        {
            get => _product.Type;
            set
            {
                _product.Type = value;
                Validate();
                OnPropertyChanged("Type");
            }
        }

        [Required(ErrorMessage = "Size is required")]
        public string Size
        {
            get => _product.Size;
            set
            {
                _product.Size = value;
                Validate();
                OnPropertyChanged("Size");
            }
        }

        [Required(ErrorMessage = "Color is required")]
        public string Color
        {
            get => _product.Color;
            set
            {
                _product.Color = value;
                Validate();
                OnPropertyChanged("Color");
            }
        }

        public void Validate()
        {
            var valContext = new ValidationContext(this, null, null);
            var valResults = new List<ValidationResult>();

            // Perform validation
            Validator.TryValidateObject(this, valContext, valResults, true);

            // Remove errors that are no longer present
            foreach (var x in Errors.ToList().Where(x => valResults.All(r => r.MemberNames.All(m => m != x.Key))))
            {
                Errors.Remove(x.Key);
                OnErrorChanged(x.Key);
            }

            // Add new validation errors
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
        }
    }
}
