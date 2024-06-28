using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile.Services
{
    //public class NavigationService : INavigationService
    //{
    //    private readonly IServiceProvider _serviceProvider;

    //    public NavigationService(IServiceProvider serviceProvider)
    //    {
    //        _serviceProvider = serviceProvider;
    //    }

    //    public async Task NavigateToAsync<TViewModel>() where TViewModel : BaseViewModel
    //    {
    //        var pageType = GetPageTypeForViewModel(typeof(TViewModel));
    //        var page = (Page)Activator.CreateInstance(pageType);

    //        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel));
    //        page.BindingContext = viewModel;

    //        await Shell.Current.Navigation.PushAsync(page);
    //    }

    //    public async Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : BaseViewModel
    //    {
    //        var pageType = GetPageTypeForViewModel(typeof(TViewModel));
    //        var page = (Page)Activator.CreateInstance(pageType);

    //        var viewModel = (TViewModel)_serviceProvider.GetService(typeof(TViewModel));
    //        page.BindingContext = viewModel;

    //        if (viewModel != null)
    //        {
    //            await viewModel.InitializeAsync(parameter);
    //        }

    //        await Shell.Current.Navigation.PushAsync(page);
    //    }

    //    public async Task GoBackAsync()
    //    {
    //        await Shell.Current.Navigation.PopAsync();
    //    }

    //    private Type GetPageTypeForViewModel(Type viewModelType)
    //    {
    //        var viewName = viewModelType.FullName.Replace("ViewModel", "Page");
    //        var viewModelAssemblyName = viewModelType.Assembly.FullName;
    //        var viewAssemblyName = string.Format("{0}, {1}", viewName, viewModelAssemblyName);
    //        var viewType = Type.GetType(viewAssemblyName);
    //        return viewType;
    //    }
    //}
}
