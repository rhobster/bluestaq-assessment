using Microsoft.EntityFrameworkCore;
using NoteTakingService.Data;
using NoteTakingService.Models;

namespace NoteTakingService.Repositories;

public static class NoteRepository
{
    public static List<Note> GetAll()
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Notes.AsNoTracking().ToList();
    }

    public static Note? GetById(int id)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Notes.AsNoTracking().FirstOrDefault(note => note.Id == id);
    }

    public static List<Note> GetByUserId(int userId)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        return db.Notes.AsNoTracking().Where(note => note.UserId == userId).ToList();
    }

    public static Note Create(Note note)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        note.DateModified = DateTime.UtcNow;
        db.Notes.Add(note);
        db.SaveChanges();
        return note;
    }

    public static Note? Update(int id, Note note)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        var existing = db.Notes.FirstOrDefault(n => n.Id == id);
        if (existing is null)
        {
            return null;
        }

        existing.Description = note.Description;
        existing.UserId = note.UserId;
        existing.DateModified = DateTime.UtcNow;
        db.SaveChanges();

        return existing;
    }

    public static bool Delete(int id)
    {
        using var db = new NoteTakingDbContext(new DbContextOptionsBuilder<NoteTakingDbContext>()
            .UseSqlite("Data Source=NoteTakingService.db")
            .Options);

        var note = db.Notes.FirstOrDefault(n => n.Id == id);
        if (note is null)
        {
            return false;
        }

        db.Notes.Remove(note);
        db.SaveChanges();
        return true;
    }
}
