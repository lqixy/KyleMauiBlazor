using Microsoft.Extensions.Logging;
using KyleMauiBlazor.TodoList.Data;
using KyleMauiBlazor.TodoList.DbContext;
using Newtonsoft.Json;

namespace KyleMauiBlazor.TodoList;

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
            });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<CategoryDatabase>();
        builder.Services.AddTransient<TodoItemDatabase>();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddSingleton<TodoItemService>();
        builder.Services.AddSingleton<CategoryService>();

        builder.Services.AddMasaBlazor();

        return builder.Build();
    }
}

