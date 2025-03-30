using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceLayer.Abstractions;
using ServiceLayer.Services;
using WeatherArchive.Server.Automapper;

namespace WeatherArchive.Server
{
    public static class IServiceCollectionExtensions
    {
        public static void AddAppSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "ArchiveAPIComments.xml");
                opt.IncludeXmlComments(xmlPath);

                string version = "v1";
                opt.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = "Weather Archive API",
                    Description = "API для работы с архивами погодных условий",
                });
            });
        }

        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IParseWeatherXlsService, ParseWeatherXlsService>();
            services.AddSingleton<IWeatherRecordDateFilterService, WeatherRecordDateFilterService>();

            services.AddScoped<IWeatherRecordsService, WeatherRecordsService>();
        }

        public static void AddAppAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppMappingProfile));
        }


        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options
               => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
