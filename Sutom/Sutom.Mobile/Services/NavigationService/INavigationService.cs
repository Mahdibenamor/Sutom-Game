using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile.Services.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel;
        Task GoBackAsync();
        Page CreatePage<TViewModel>(Type viewModelType) where TViewModel : BaseViewModel;
    }
}
