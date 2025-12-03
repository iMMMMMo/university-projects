using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SportsClothes.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Dictionary<string, List<string>> Errors = new();
        public bool HasErrors => Errors.Count > 0;

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return Enumerable.Empty<string>();

            return Errors.TryGetValue(propertyName, out var error) ? error : Enumerable.Empty<string>();
        }

        protected void RemoveErrors(string propertyName)
        {
            if (Errors.Remove(propertyName))
            {
                OnErrorChanged(propertyName);
            }
        }

        protected void AddError(string propertyName, string errorMsg)
        {
            if (!Errors.TryGetValue(propertyName, out var propertyErrors))
            {
                propertyErrors = new List<string>();
                Errors[propertyName] = propertyErrors;
            }

            propertyErrors.Add(errorMsg);
            OnErrorChanged(propertyName);
        }

        protected void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
