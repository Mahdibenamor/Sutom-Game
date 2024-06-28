using Microsoft.Extensions.Logging;
using Sutom.Core;
using Sutom.Mobile.Core;
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
            return builder.Build();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            BuildPageViewModelMappings(services);
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
