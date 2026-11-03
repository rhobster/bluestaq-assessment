namespace NoteTakingService.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public DateTime DateModified { get; set; } = DateTime.UtcNow;

    public Team Team { get; set; } = null!;
    public List<Note> Notes { get; set; } = new();
}
