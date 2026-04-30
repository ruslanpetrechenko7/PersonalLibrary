namespace PersonalLibrary.Models;

/// <summary>
/// Представляє книгу в особистій бібліотеці.
/// </summary>
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int Rating { get; set; } = 0;
    public string Status { get; set; } = "хочу прочитати";
}