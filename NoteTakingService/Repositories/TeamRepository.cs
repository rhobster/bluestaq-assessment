using Microsoft.EntityFrameworkCore;
using NoteTakingService.Data;
using NoteTakingService.Models;

namespace NoteTakingService.Repositories;

public static class TeamRepository
{
    public static List<Team> GetAll()
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Teams.AsNoTracking().ToList();
    }

    public static Team? GetById(int id)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Teams.AsNoTracking().FirstOrDefault(team => team.Id == id);
    }

    public static Team Create(Team team)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        team.DateModified = DateTime.UtcNow;
        db.Teams.Add(team);
        db.SaveChanges();
        return team;
    }

    public static Team? Update(int id, Team team)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        var existing = db.Teams.FirstOrDefault(t => t.Id == id);
        if (existing is null)
        {
            return null;
        }

        existing.Name = team.Name;
        existing.DateModified = DateTime.UtcNow;
        db.SaveChanges();

        return existing;
    }

    public static bool Delete(int id)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        var team = db.Teams.FirstOrDefault(t => t.Id == id);
        if (team is null)
        {
            return false;
        }

        db.Teams.Remove(team);
        db.SaveChanges();
        return true;
    }
}
