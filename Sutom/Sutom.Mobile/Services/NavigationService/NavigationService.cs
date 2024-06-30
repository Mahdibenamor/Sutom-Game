using Sutom.Mobile.Core;
using Sutom.Mobile.Pages;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly ViewModelPageMapping _viewLocator;
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(ViewModelPageMapping viewLocator, IServiceProvider serviceProvider)
        {
            _viewLocator = viewLocator;
            _serviceProvider = serviceProvider;
        }
        public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            var page = CreatePage<TViewModel>(typeof(TViewModel));
            await App.Navigation.PushAsync(page);
        }

        public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
        {
            var page = CreatePage<TViewModel>(typeof(TViewModel));
            var viewModel = (BaseViewModel)page.BindingContext;

            if (viewModel != null)
            {
                await viewModel.InitializeAsync(parameter);
            }

            await App.Navigation.PushAsync(page);
        }

        public async Task GoBackAsync()
        {
            await App.Navigation.PopAsync();
        }

        public Page CreatePage<TViewModel>(Type viewModelType) where TViewModel : BaseViewModel
        {
            var pageType = _viewLocator.GetPageTypeForViewModel(viewModelType);
            var page = _serviceProvider.GetService(pageType) as Page;
            var viewModel = _serviceProvider.GetService(viewModelType) as TViewModel;
            page.BindingContext = viewModel;
            (page as IBasePage<TViewModel>).Initialize();
            return page;
        }
    }
}
