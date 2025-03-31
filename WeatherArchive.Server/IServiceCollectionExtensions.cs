using DataLayer;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServiceLayer.Abstractions;
using ServiceLayer.Services;
using WeatherArchive.Server.Automapper;
using WeatherArchive.Server.Models;
using WeatherArchive.Server.Validation;

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

        public static void AddAppValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<WeatherRecordsRequest>, WeatherRecordsRequestValidator>();
            services.AddScoped<IValidator<FilesUploadRequest>, FilesUploadRequestValidator>();
            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            });
        }
    }
}
