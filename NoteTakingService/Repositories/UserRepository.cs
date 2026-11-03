using Microsoft.EntityFrameworkCore;
using NoteTakingService.Data;
using NoteTakingService.Models;

namespace NoteTakingService.Repositories;

public static class UserRepository
{
    public static List<User> GetAll()
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Users.AsNoTracking().ToList();
    }

    public static User? GetById(int id)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Users.AsNoTracking().FirstOrDefault(user => user.Id == id);
    }

    public static List<User> GetByTeamId(int teamId)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Users.AsNoTracking().Where(user => user.TeamId == teamId).ToList();
    }

    public static User Create(User user)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        user.DateModified = DateTime.UtcNow;
        db.Users.Add(user);
        db.SaveChanges();
        return user;
    }

    public static User? Update(int id, User user)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        var existing = db.Users.FirstOrDefault(u => u.Id == id);
        if (existing is null)
        {
            return null;
        }

        existing.FirstName = user.FirstName;
        existing.LastName = user.LastName;
        existing.Email = user.Email;
        existing.TeamId = user.TeamId;
        existing.DateModified = DateTime.UtcNow;
        db.SaveChanges();

        return existing;
    }

    public static bool Delete(int id)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        var user = db.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            return false;
        }

        db.Users.Remove(user);
        db.SaveChanges();
        return true;
    }
}
