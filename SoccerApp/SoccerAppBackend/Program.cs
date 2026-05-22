using SoccerAppBackend.Data;

namespace SoccerAppBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IDatabaseService, DatabaseService>();
            builder.Services.AddScoped<ICoachesService, CoachesService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/", () => "SoccerAppBackend is running...");

            app.MapControllers();

            app.Run();
        }
    }
}
