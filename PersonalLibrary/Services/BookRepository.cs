using System.Text.Json;
using PersonalLibrary.Models;

namespace PersonalLibrary.Services;

public class BookRepository
{
    private readonly string _filePath;
    private List<Book> _books;

    public BookRepository(string filePath = "library.json")
    {
        _filePath = filePath;
        _books = Load();
    }

    public List<Book> GetAll() => _books;

    public void Add(Book book)
    {
        book.Id = _books.Count > 0 ? _books.Max(b => b.Id) + 1 : 1;
        _books.Add(book);
        Save();
    }

    public void Update(Book book)
    {
        var index = _books.FindIndex(b => b.Id == book.Id);
        if (index >= 0)
        {
            _books[index] = book;
            Save();
        }
    }

    public void Delete(int id)
    {
        _books.RemoveAll(b => b.Id == id);
        Save();
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_books, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private List<Book> Load()
    {
        if (!File.Exists(_filePath)) return new List<Book>();
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
    }
}