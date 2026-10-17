using Microsoft.Extensions.Logging;
using SRSInventory.Classes;
using SRSInventory.Interfaces;
using SRSInventory.Pages.MainPage;
using SRSInventory.Pages.DetailsPage;
using SRSInventory.Services;
using CommunityToolkit.Maui;

namespace SRSInventory
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<Interfaces.ILogger>(new Logger());
            builder.Services.AddSingleton<IDatabaseService>(new DatabaseService());
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<DetailsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
