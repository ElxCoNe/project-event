namespace EventProject.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? ImageURL { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Status { get; set; } = "Draft";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}