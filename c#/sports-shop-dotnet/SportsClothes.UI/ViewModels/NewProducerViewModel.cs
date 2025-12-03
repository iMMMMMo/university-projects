using SportsClothes.BL;
using SportsClothes.INTERFACES;
using SportsClothes.UI.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClothes.UI.ViewModels
{
    public class NewProducerViewModel : ViewModelBase
    {
        private readonly BL.BL _bl;

        private IProducerDto _producer;

        public IProducerDto Producer
        {
            get => _producer;
            set
            {
                _producer = value;
                OnPropertyChanged(nameof(Producer));
            }
        }

        public NewProducerViewModel()
        {
            _producer = new ProducerDto();
            _bl = BL.BL.Instance;

            _addProducerCommand = new RelayCommand(param => AddProducer(), param => CanAddProducer());
        }

        private readonly RelayCommand _addProducerCommand;
        public RelayCommand AddProducerCommand => _addProducerCommand;

        private void AddProducer()
        {
            var validationResults = Validate();

            if (validationResults.Any())
                return;

            _bl.CreateProducer(Producer);
            ClearForm();
        }

        private void ClearForm()
        {
            Producer = new ProducerDto();
            Name = string.Empty;
            Description = string.Empty;
            CountryOfOrigin = string.Empty;
        }

        private bool CanAddProducer()
        {
            return !Producer.Equals(null);
        }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(25, ErrorMessage = "Name cannot exceed 25 characters")]
        public string Name
        {
            get => _producer.Name;
            set
            {
                _producer.Name = value;
                OnPropertyChanged("Name");
            }
        }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
        public string Description
        {
            get => _producer.Description;
            set
            {
                _producer.Description = value;
                OnPropertyChanged("Description");
            }
        }

        [Required(ErrorMessage = "Country of origin is required")]
        [MaxLength(25, ErrorMessage = "Country of origin cannot exceed 25 characters")]
        public string CountryOfOrigin
        {
            get => _producer.CountryOfOrigin;
            set
            {
                _producer.CountryOfOrigin = value;
                OnPropertyChanged("CountryOfOrigin");
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
