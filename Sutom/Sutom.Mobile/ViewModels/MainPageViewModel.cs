
using Sutom.Mobile.Services;
using System.Windows.Input;

namespace Sutom.Mobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private int _countViewModel = 10;
        public ICommand NavigateToGamePageCommand { get; }
        private readonly INavigationService _navigationService;

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

        public MainPageViewModel(INavigationService navigation) {
            _navigationService = navigation;
            NavigateToGamePageCommand = new Command(async () => await NavigateToGamePage());
        }

        public override async Task InitializeAsync(object parameter)
        {
            await Task.CompletedTask;
        }

        public async Task NavigateToGamePage()
        {
            await _navigationService.NavigateToAsync<GamePageViewModel>();
        }
    }
}
