using Scalar.AspNetCore;

namespace WeatherArchive.Server
{
    public static class WebApplicationExtensions
    {
        public static void MapAppScalarApi(this WebApplication app)
        {
            app.MapScalarApiReference(options =>
            {
                options
               .WithTitle("Weather Archive API")
               .WithSidebar(true);
            });
        }
    }
}
