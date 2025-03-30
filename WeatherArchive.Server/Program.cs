using ServiceLayer.Services;

namespace WeatherArchive.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);          

            builder.Services.AddControllers();
            builder.Services.AddAppServices();

            builder.Services.AddAppAutoMapper();

            builder.Services.AddOpenApi();
            builder.Services.AddAppSwagger();

            builder.Services.AddAppDbContext(builder.Configuration);
            builder.Services.AddHostedService<DatabaseInitService>();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}
