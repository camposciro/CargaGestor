using CargaGestor;  // << Adicione esta linha para acessar App
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

#if ANDROID
using Android.App;
using Plugin.Fingerprint;
using Microsoft.Maui.Platform;
#endif

namespace CargaGestor
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()  // Aqui o compilador precisa encontrar App
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if ANDROID
            CrossFingerprint.SetCurrentActivityResolver(() =>
            {
                var activity = Platform.CurrentActivity;
                return activity ?? throw new InvalidOperationException("Current Activity is null");
            });
#endif

            return builder.Build();
        }
    }
}
