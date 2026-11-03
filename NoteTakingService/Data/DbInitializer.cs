using NoteTakingService.Models;

namespace NoteTakingService.Data;

public static class DbInitializer
{
    public static void Initialize(NoteTakingDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();

        if (dbContext.Teams.Any())
        {
            return;
        }

        var blueStaq = new Team
        {
            Name = "BlueStaq",
            DateModified = DateTime.UtcNow
        };

        dbContext.Teams.Add(blueStaq);
        dbContext.SaveChanges();

        var users = new[]
        {
            new User
            {
                FirstName = "Avery",
                LastName = "Brooks",
                Email = "avery.brooks@bluestaq.com",
                TeamId = blueStaq.Id,
                DateModified = DateTime.UtcNow
            },
            new User
            {
                FirstName = "Jordan",
                LastName = "Lee",
                Email = "jordan.lee@bluestaq.com",
                TeamId = blueStaq.Id,
                DateModified = DateTime.UtcNow
            },
            new User
            {
                FirstName = "Maya",
                LastName = "Patel",
                Email = "maya.patel@bluestaq.com",
                TeamId = blueStaq.Id,
                DateModified = DateTime.UtcNow
            }
        };

        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();

        dbContext.Notes.AddRange(
            new Note
            {
                Description = "Reviewed project kickoff checklist and assigned team owners.",
                UserId = users[0].Id,
                DateModified = DateTime.UtcNow
            },
            new Note
            {
                Description = "Drafted follow-up tasks for onboarding and deployment notes.",
                UserId = users[1].Id,
                DateModified = DateTime.UtcNow
            },
            new Note
            {
                Description = "Captured release plan and stakeholder feedback for the next sprint.",
                UserId = users[2].Id,
                DateModified = DateTime.UtcNow
            });

        dbContext.SaveChanges();
    }
}
