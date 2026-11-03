
using Microsoft.EntityFrameworkCore;
using NoteTakingService.Data;

namespace NoteTakingService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<NoteTakingDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=NoteTakingService.db"));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NoteTakingDbContext>();
                DbInitializer.Initialize(dbContext);
            }

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapGet("/docs", () => Results.Redirect("/openapi/v1.json"));
            }

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}