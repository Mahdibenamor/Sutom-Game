
namespace Sutom.Mobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int _countViewModel = 10;
        public int CountViewModel
        {
            get { return _countViewModel; }
            set
            {
                if (_countViewModel != value)
                {
                    _countViewModel = value;
                    OnPropertyChanged(nameof(CountViewModel));
                }
            }
        }

        public MainPageViewModel() { }

        public override async Task InitializeAsync(object parameter)
        {
            await Task.CompletedTask;
        }
    }
}
