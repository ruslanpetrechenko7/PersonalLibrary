using PersonalLibrary.Models;
using PersonalLibrary.Services;

namespace PersonalLibrary.UI;

public static class BookInput
{
    public static void AddBook(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║          ДОДАВАННЯ КНИГИ           ║");
        Console.WriteLine("╚════════════════════════════════════╝");

        var book = new Book();
        book.AddedDate = DateTime.Now;

        Console.Write("Назва: ");
        book.Title = Console.ReadLine()?.Trim() ?? string.Empty;
        if (string.IsNullOrEmpty(book.Title))
        {
            Console.WriteLine("Назва не може бути порожньою!");
            Console.ReadKey();
            return;
        }

        Console.Write("Автор: ");
        book.Author = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Видавництво: ");
        book.Publisher = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Рік видання: ");
        if (int.TryParse(Console.ReadLine(), out int year) && year >= 1000 && year <= DateTime.Now.Year)
            book.Year = year;

        Console.Write("Жанр: ");
        book.Genre = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.WriteLine("Розділ: 1. Спеціальна  2. Хобі  3. Белетристика  4. Дом. госп.  5. Інше");
        Console.Write("Оберіть (1-5): ");
        book.Section = Console.ReadLine() switch
        {
            "1" => "Спеціальна",
            "2" => "Хобі",
            "3" => "Белетристика",
            "4" => "Дом. госп.",
            _ => "Інше"
        };

        Console.WriteLine("Походження: 1. Куплена  2. Подарована  3. Позичена");
        Console.Write("Оберіть (1-3): ");
        book.Origin = Console.ReadLine() switch
        {
            "1" => "куплена",
            "2" => "подарована",
            _ => "позичена"
        };

        Console.WriteLine("Наявність: 1. Є в наявності  2. Відсутня (позичена комусь)");
        Console.Write("Оберіть (1-2): ");
        book.IsAvailable = Console.ReadLine() != "2";

        Console.Write("Оцінка (1-10): ");
        if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 10)
            book.Rating = rating;

        Console.WriteLine("Статус: 1. читаю  2. прочитана  3. хочу прочитати");
        Console.Write("Оберіть (1-3): ");
        book.Status = Console.ReadLine() switch
        {
            "1" => "читаю",
            "2" => "прочитана",
            _ => "хочу прочитати"
        };

        Console.Write("Нотатки (або Enter щоб пропустити): ");
        book.Notes = Console.ReadLine()?.Trim() ?? string.Empty;

        Console.Write("Нагадування (дд.мм.рррр або Enter щоб пропустити): ");
        var reminderInput = Console.ReadLine()?.Trim();
        if (DateTime.TryParseExact(reminderInput, "dd.MM.yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime reminder))
            book.ReminderDate = reminder;

        service.Add(book);
        Console.WriteLine($"\n✓ Книгу успішно додано! Дата додавання: {book.AddedDate:dd.MM.yyyy}");
        Console.ReadKey();
    }

    public static void EditBook(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║        РЕДАГУВАННЯ КНИГИ           ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        BookDisplay.ShowBooks(service.GetAll());

        Console.Write("Введіть ID книги для редагування: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний ID!");
            Console.ReadKey();
            return;
        }

        var book = service.GetAll().FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            Console.WriteLine("Книгу не знайдено!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Назва ({book.Title}): ");
        var title = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(title)) book.Title = title;

        Console.Write($"Автор ({book.Author}): ");
        var author = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(author)) book.Author = author;

        Console.Write($"Видавництво ({book.Publisher}): ");
        var publisher = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(publisher)) book.Publisher = publisher;

        Console.Write($"Рік ({book.Year}): ");
        if (int.TryParse(Console.ReadLine(), out int year) && year >= 1000 && year <= DateTime.Now.Year)
            book.Year = year;

        Console.Write($"Жанр ({book.Genre}): ");
        var genre = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(genre)) book.Genre = genre;

        Console.Write($"Розділ ({book.Section}): ");
        var section = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(section)) book.Section = section;

        Console.Write($"Походження ({book.Origin}): ");
        var origin = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(origin)) book.Origin = origin;

        Console.WriteLine($"Наявність ({(book.IsAvailable ? "є" : "немає")}): 1. Є  2. Немає");
        Console.Write("Оберіть (1-2): ");
        var avail = Console.ReadLine();
        if (avail == "1") book.IsAvailable = true;
        else if (avail == "2") book.IsAvailable = false;

        Console.Write($"Оцінка ({book.Rating}): ");
        if (int.TryParse(Console.ReadLine(), out int rating) && rating >= 1 && rating <= 10)
            book.Rating = rating;

        Console.WriteLine("Статус: 1. читаю  2. прочитана  3. хочу прочитати");
        Console.Write("Оберіть (1-3): ");
        book.Status = Console.ReadLine() switch
        {
            "1" => "читаю",
            "2" => "прочитана",
            "3" => "хочу прочитати",
            _ => book.Status
        };

        Console.Write($"Нотатки ({book.Notes}): ");
        var notes = Console.ReadLine()?.Trim();
        if (!string.IsNullOrEmpty(notes)) book.Notes = notes;

        Console.Write($"Нагадування ({(book.ReminderDate.HasValue ? book.ReminderDate.Value.ToString("dd.MM.yyyy") : "немає")}): ");
        var reminderInput = Console.ReadLine()?.Trim();
        if (DateTime.TryParseExact(reminderInput, "dd.MM.yyyy",
            System.Globalization.CultureInfo.InvariantCulture,
            System.Globalization.DateTimeStyles.None, out DateTime reminder))
            book.ReminderDate = reminder;

        service.Update(book);
        Console.WriteLine("\n✓ Книгу успішно оновлено!");
        Console.ReadKey();
    }

    public static void DeleteBook(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║          ВИДАЛЕННЯ КНИГИ           ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        BookDisplay.ShowBooks(service.GetAll());

        Console.Write("Введіть ID книги для видалення: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Невірний ID!");
            Console.ReadKey();
            return;
        }

        var book = service.GetAll().FirstOrDefault(b => b.Id == id);
        if (book == null)
        {
            Console.WriteLine("Книгу не знайдено!");
            Console.ReadKey();
            return;
        }

        Console.Write($"Ви впевнені що хочете видалити \"{book.Title}\"? (так/ні): ");
        if (Console.ReadLine()?.ToLower() == "так")
        {
            service.Delete(id);
            Console.WriteLine("\n✓ Книгу видалено!");
        }
        Console.ReadKey();
    }
}