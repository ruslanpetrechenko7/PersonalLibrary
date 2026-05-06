using PersonalLibrary.Models;

namespace PersonalLibrary.Services;

public class BookService
{
    private readonly BookRepository _repository;

    public BookService(BookRepository repository)
    {
        _repository = repository;
    }

    public List<Book> GetAll() => _repository.GetAll();

    public void Add(Book book) => _repository.Add(book);

    public void Update(Book book) => _repository.Update(book);

    public void Delete(int id) => _repository.Delete(id);

    public List<Book> Search(string query)
    {
        query = query.ToLower();
        return _repository.GetAll()
            .Where(b => b.Title.ToLower().Contains(query) ||
                        b.Author.ToLower().Contains(query) ||
                        b.Genre.ToLower().Contains(query))
            .ToList();
    }

    public List<Book> FilterByStatus(string status)
    {
        return _repository.GetAll()
            .Where(b => b.Status == status)
            .ToList();
    }

    public List<Book> SortByYear()
    {
        return _repository.GetAll()
            .OrderBy(b => b.Year)
            .ToList();
    }

    public List<Book> SortByRating()
    {
        return _repository.GetAll()
            .OrderByDescending(b => b.Rating)
            .ToList();
    }
}