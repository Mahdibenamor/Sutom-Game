using Sutom.Mobile.Services.Navigation;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public static IServiceProvider Services { get; private set; }
        public static NavigationPage Navigation { get; private set; }
        public static Page Page { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;
            INavigationService? navigation = serviceProvider.GetService<INavigationService>();
            Navigation = new NavigationPage(navigation?.CreatePage<MainPageViewModel>(typeof(MainPageViewModel)));
            MainPage = Navigation;
            Page = MainPage;
        }
    }
}
