using Microsoft.Extensions.Logging;
using Sutom.Absrtractions;
using Sutom.Application.Implementations;
using Sutom.Core;
using Sutom.Mobile.Core;
using Sutom.Mobile.Services.DialogService;
using Sutom.Mobile.Services.Navigation;
using Sutom.Mobile.ViewModels;
using System.Reflection;

namespace Sutom.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            ConfigureServices(builder.Services);
            var app = builder.Build();
            var serviceProvider = app.Services;

            return app;
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            BuildPageViewModelMappings(services); 
            services.AddSingleton<IGameService, RemoteGameService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IDialogService, DialogService>();
            ConfigureViewModel(services);
            ConfigureViews(services);
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddTransient(provider => new MainPage());
            services.AddTransient(provider => new GamePage());
        }


        private static void ConfigureViewModel(IServiceCollection services)
        {
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<GamePageViewModel>();
        }

        private static void BuildPageViewModelMappings(IServiceCollection services)
        {
            ViewModelPageMapping viewModelPageMapping = new ViewModelPageMapping();
            var assembly = Assembly.GetExecutingAssembly();
            var pageTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<ViewModel>() != null)
                .ToList();

            foreach (var pageType in pageTypes)
            {
                var attribute = pageType.GetCustomAttribute<ViewModel>();
                if (attribute != null)
                {
                    var viewModelType = attribute.ViewModelType;
                    viewModelPageMapping.Register(viewModelType, pageType);
                }
            }
            services.AddSingleton(viewModelPageMapping);
        }
    }
}
