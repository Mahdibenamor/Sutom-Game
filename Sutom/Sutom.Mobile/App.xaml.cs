
using Sutom.Mobile.Services;
using Sutom.Mobile.ViewModels;

namespace Sutom.Mobile
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public static IServiceProvider Services { get; private set; }

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            Services = serviceProvider;
            INavigationService? navigation = serviceProvider.GetService<INavigationService>();
            MainPage = new NavigationPage(navigation?.CreatePage<MainPageViewModel>(typeof(MainPageViewModel)));
        }
    }
}
