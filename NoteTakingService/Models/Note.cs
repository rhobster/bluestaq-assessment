namespace NoteTakingService.Models;

public class Note
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }

    public User User { get; set; } = null!;
}
