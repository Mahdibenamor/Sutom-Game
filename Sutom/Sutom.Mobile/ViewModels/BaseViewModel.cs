

using System.ComponentModel;

namespace Sutom.Mobile.ViewModels
{

    public interface IBaseViewModel
    {
        public Task InitializeAsync(object parameter);

    }

    public abstract class BaseViewModel : INotifyPropertyChanged, IBaseViewModel 
    {
        public BaseViewModel() { }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitializeAsync(object parameter)
        {
            return Task.CompletedTask;
        }
    }
}
