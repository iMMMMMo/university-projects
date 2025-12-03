using SportsClothes.CORE;
using SportsClothes.INTERFACES;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SportsClothes.UI.ViewModels
{
    public class ProducerViewModel : ViewModelBase
    {
        private IProducer _producer;
        public IProducer Producer => _producer;

        public ProducerViewModel(IProducer producer)
        {
            _producer = producer;
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name
        {
            get => _producer.Name;
            set
            {
                _producer.Name = value;
                Validate();
                OnPropertyChanged("Name");
            }
        }

        [Required(ErrorMessage = "Producer's country of origin is required")]
        public string CountryOfOrigin
        {
            get => _producer.CountryOfOrigin;
            set
            {
                _producer.CountryOfOrigin = value;
                Validate();
                OnPropertyChanged("CountryOfOrigin");
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
                Validate();
                OnPropertyChanged("Description");
            }
        }


        public void Validate()
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
        }
    }
}
