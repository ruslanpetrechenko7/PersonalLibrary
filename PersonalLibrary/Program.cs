using PersonalLibrary.Models;
using PersonalLibrary.Services;

var repository = new BookRepository();
var service = new BookService(repository);

while (true)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║       ОСОБИСТА БІБЛІОТЕКА          ║");
    Console.WriteLine("╠════════════════════════════════════╣");
    Console.WriteLine("║  1. Показати всі книги             ║");
    Console.WriteLine("║  2. Додати книгу                   ║");
    Console.WriteLine("║  3. Редагувати книгу               ║");
    Console.WriteLine("║  4. Видалити книгу                 ║");
    Console.WriteLine("║  5. Пошук книги                    ║");
    Console.WriteLine("║  6. Розширений пошук               ║");
    Console.WriteLine("║  7. Фільтрувати за статусом        ║");
    Console.WriteLine("║  8. Сортувати за роком видання     ║");
    Console.WriteLine("║  9. Сортувати за оцінкою           ║");
    Console.WriteLine("║  10. Інвентаризація                ║");
    Console.WriteLine("║  0. Вийти                          ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.Write("\nОберіть пункт: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": ShowBooks(service.GetAll()); break;
        case "2": AddBook(service); break;
        case "3": EditBook(service); break;
        case "4": DeleteBook(service); break;
        case "5": SearchBooks(service); break;
        case "6": AdvancedSearchBooks(service); break;
        case "7": FilterBooks(service); break;
        case "8": ShowBooks(service.SortByYear()); break;
        case "9": ShowBooks(service.SortByRating()); break;
        case "10": Inventory(service); break;
        case "0": return;
        default:
            Console.WriteLine("Невірний вибір!");
            Console.ReadKey();
            break;
    }
}

void ShowBooks(List<Book> books)
{
    Console.Clear();
    if (books.Count == 0)
    {
        Console.WriteLine("Книг не знайдено.");
        Console.ReadKey();
        return;
    }

    Console.WriteLine("╔═════╦══════════════════════════╦════════════════╦══════╦══════════╦══════════════╦════════════╦════════╦══════════════════════╗");
    Console.WriteLine("║ ID  ║ Назва                    ║ Автор          ║ Рік  ║ Жанр     ║ Розділ       ║ Походження ║ Оцінка ║ Статус               ║");
    Console.WriteLine("╠═════╬══════════════════════════╬════════════════╬══════╬══════════╬══════════════╬════════════╬════════╬══════════════════════╣");

    foreach (var book in books)
    {
        var title = book.Title.Length > 25 ? book.Title.Substring(0, 22) + "..." : book.Title;
        var author = book.Author.Length > 14 ? book.Author.Substring(0, 11) + "..." : book.Author;
        Console.WriteLine($"║ {book.Id,-4}║ {title,-25}║ {author,-15}║ {book.Year,-5}║ {book.Genre,-9}║ {book.Section,-13}║ {book.Origin,-11}║ {book.Rating,-7}║ {book.Status,-21}║");
    }

    Console.WriteLine("╚═════╩══════════════════════════╩════════════════╩══════╩══════════╩══════════════╩════════════╩════════╩══════════════════════╝");
    Console.WriteLine($"\nВсього книг: {books.Count}");
    Console.ReadKey();
}

void AddBook(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║          ДОДАВАННЯ КНИГИ           ║");
    Console.WriteLine("╚════════════════════════════════════╝");

    var book = new Book();

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

    Console.WriteLine("Розділ: 1. Спеціальна  2. Хобі  3. Белетристика  4. Дом. господарство  5. Інше");
    Console.Write("Оберіть (1-5): ");
    book.Section = Console.ReadLine() switch
    {
        "1" => "Спеціальна",
        "2" => "Хобі",
        "3" => "Белетристика",
        "4" => "Дом. господарство",
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

    service.Add(book);
    Console.WriteLine("\n✓ Книгу успішно додано!");
    Console.ReadKey();
}

void EditBook(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║        РЕДАГУВАННЯ КНИГИ           ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    ShowBooks(service.GetAll());

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

    service.Update(book);
    Console.WriteLine("\n✓ Книгу успішно оновлено!");
    Console.ReadKey();
}

void DeleteBook(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║          ВИДАЛЕННЯ КНИГИ           ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    ShowBooks(service.GetAll());

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

void SearchBooks(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║            ПОШУК КНИГИ             ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.Write("Введіть запит (назва, автор або жанр): ");
    var query = Console.ReadLine() ?? string.Empty;
    ShowBooks(service.Search(query));
}

void AdvancedSearchBooks(BookService service)
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

    ShowBooks(service.AdvancedSearch(title, author, genre, yearFrom, yearTo));
}

void FilterBooks(BookService service)
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

    ShowBooks(service.FilterByStatus(status));
}

void Inventory(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔════════════════════════════════════╗");
    Console.WriteLine("║          ІНВЕНТАРИЗАЦІЯ            ║");
    Console.WriteLine("╚════════════════════════════════════╝");

    var books = service.GetAll();

    Console.WriteLine($"Всього книг:         {books.Count}");
    Console.WriteLine($"Прочитано:           {books.Count(b => b.Status == "прочитана")}");
    Console.WriteLine($"Читаю зараз:         {books.Count(b => b.Status == "читаю")}");
    Console.WriteLine($"Хочу прочитати:      {books.Count(b => b.Status == "хочу прочитати")}");
    Console.WriteLine($"Є в наявності:       {books.Count(b => b.IsAvailable)}");
    Console.WriteLine($"Відсутні (позичені): {books.Count(b => !b.IsAvailable)}");
    Console.WriteLine();
    Console.WriteLine("─── За розділами ───");
    foreach (var section in books.GroupBy(b => b.Section))
        Console.WriteLine($"  {section.Key}: {section.Count()} книг");
    Console.WriteLine();
    Console.WriteLine("─── За походженням ───");
    foreach (var origin in books.GroupBy(b => b.Origin))
        Console.WriteLine($"  {origin.Key}: {origin.Count()} книг");

    Console.ReadKey();
}