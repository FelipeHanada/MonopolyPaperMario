using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MonopolyPaperMario
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
                    fonts.AddFont("Jersey25-Regular.ttf", "Jersey25");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
            events.AddWindows(windows => windows
                .OnWindowCreated(window =>
                {
                    var appWindow = window.AppWindow;
                    if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter)
                    {
                        overlappedPresenter.Maximize();
                    }
                }));
#endif
            });

            return builder.Build();
        }
    }
}
