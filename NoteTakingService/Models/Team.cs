namespace NoteTakingService.Models;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime DateModified { get; set; } = DateTime.UtcNow;

    public List<User> Users { get; set; } = new();
}
