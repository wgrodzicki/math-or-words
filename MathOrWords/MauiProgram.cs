using Microsoft.Extensions.Logging;

namespace MathOrWords
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
                    fonts.AddFont("Fredoka-Regular.ttf", "FredokaRegular");
                    fonts.AddFont("IndieFlower-Regular.ttf", "IndieFlowerRegular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
