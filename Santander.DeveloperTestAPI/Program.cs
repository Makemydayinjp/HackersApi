
using Santander.DeveloperTestAPI.Cache;
using Santander.DeveloperTestAPI.Middleware;
using Santander.DeveloperTestAPI.Services;

namespace Santander.DeveloperTestAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddHttpClient("hackerNewsAPI",
                    client =>
                    {
                        client.BaseAddress = new Uri(builder.Configuration["HackersApiBaseAddress"]);
                    });

            builder.Services.AddScoped<INewsGetterService, NewsGetterService>();

            builder.Services.AddScoped<ILocalCache, LocalCache>();
            builder.Services.AddMemoryCache();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
