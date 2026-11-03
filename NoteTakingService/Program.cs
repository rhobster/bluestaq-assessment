
using Microsoft.EntityFrameworkCore;
using NoteTakingService.Data;
using NoteTakingService.Models;

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
                dbContext.Database.EnsureCreated();
                SeedData(dbContext);
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

        private static void SeedData(NoteTakingDbContext dbContext)
        {
            var team = dbContext.Teams.FirstOrDefault(t => t.Name == "BlueStaq");

            if (team is null)
            {
                team = new Team
                {
                    Name = "BlueStaq",
                    DateModified = DateTime.UtcNow
                };

                dbContext.Teams.Add(team);
                dbContext.SaveChanges();
            }

            var userOne = dbContext.Users.FirstOrDefault(u => u.Email == "avery.brooks@bluestaq.com");
            if (userOne is null)
            {
                userOne = new User
                {
                    FirstName = "Avery",
                    LastName = "Brooks",
                    Email = "avery.brooks@bluestaq.com",
                    TeamId = team.Id,
                    DateModified = DateTime.UtcNow
                };

                dbContext.Users.Add(userOne);
                dbContext.SaveChanges();
            }

            var userTwo = dbContext.Users.FirstOrDefault(u => u.Email == "jordan.lee@bluestaq.com");
            if (userTwo is null)
            {
                userTwo = new User
                {
                    FirstName = "Jordan",
                    LastName = "Lee",
                    Email = "jordan.lee@bluestaq.com",
                    TeamId = team.Id,
                    DateModified = DateTime.UtcNow
                };

                dbContext.Users.Add(userTwo);
                dbContext.SaveChanges();
            }

            if (!dbContext.Notes.Any(n => n.UserId == userOne.Id && n.Description.Contains("kickoff")))
            {
                dbContext.Notes.Add(new Note
                {
                    Description = "Reviewed project kickoff checklist and assigned team owners.",
                    UserId = userOne.Id,
                    DateModified = DateTime.UtcNow
                });
            }

            if (!dbContext.Notes.Any(n => n.UserId == userTwo.Id && n.Description.Contains("onboarding")))
            {
                dbContext.Notes.Add(new Note
                {
                    Description = "Drafted follow-up tasks for onboarding and deployment notes.",
                    UserId = userTwo.Id,
                    DateModified = DateTime.UtcNow
                });
            }

            dbContext.SaveChanges();
        }
    }
}