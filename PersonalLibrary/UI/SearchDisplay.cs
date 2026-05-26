using PersonalLibrary.Models;
using PersonalLibrary.Services;

namespace PersonalLibrary.UI;

public static class SearchDisplay
{
    public static void SearchBooks(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║            ПОШУК КНИГИ             ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.Write("Введіть запит (назва, автор або жанр): ");
        var query = Console.ReadLine() ?? string.Empty;

        var results = service.Search(query);

        if (results.Count == 0)
        {
            Console.WriteLine("Книг не знайдено.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"\nЗнайдено {results.Count} книг. Як відсортувати?");
        Console.WriteLine("1. За роком видання");
        Console.WriteLine("2. За оцінкою");
        Console.WriteLine("3. Без сортування");
        Console.Write("Оберіть (1-3): ");

        results = Console.ReadLine() switch
        {
            "1" => results.OrderBy(b => b.Year).ToList(),
            "2" => results.OrderByDescending(b => b.Rating).ToList(),
            _ => results
        };

        BookDisplay.ShowBooks(results);
    }

    public static void AdvancedSearchBooks(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║          РОЗШИРЕНИЙ ПОШУК          ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.WriteLine("(натисніть Enter щоб пропустити поле)");
        Console.WriteLine();

        Console.Write("Назва: ");
        var title = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Автор: ");
        var author = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Жанр: ");
        var genre = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Рік від: ");
        int? yearFrom = null;
        if (int.TryParse(Console.ReadLine(), out int yf)) yearFrom = yf;

        Console.Write("Рік до: ");
        int? yearTo = null;
        if (int.TryParse(Console.ReadLine(), out int yt)) yearTo = yt;

        BookDisplay.ShowBooks(service.AdvancedSearch(title, author, genre, yearFrom, yearTo));
    }

    public static void FilterBooks(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║       ФІЛЬТРАЦІЯ ЗА СТАТУСОМ       ║");
        Console.WriteLine("╠════════════════════════════════════╣");
        Console.WriteLine("║  1. читаю                          ║");
        Console.WriteLine("║  2. прочитана                      ║");
        Console.WriteLine("║  3. хочу прочитати                 ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.Write("Оберіть (1-3): ");

        var status = Console.ReadLine() switch
        {
            "1" => "читаю",
            "2" => "прочитана",
            _ => "хочу прочитати"
        };

        BookDisplay.ShowBooks(service.FilterByStatus(status));
    }
}