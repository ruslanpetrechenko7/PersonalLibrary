using PersonalLibrary.Models;
using PersonalLibrary.Services;

var repository = new BookRepository();
var service = new BookService(repository);

while (true)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║      ОСОБИСТА БІБЛІОТЕКА         ║");
    Console.WriteLine("╠══════════════════════════════════╣");
    Console.WriteLine("║  1. Показати всі книги           ║");
    Console.WriteLine("║  2. Додати книгу                 ║");
    Console.WriteLine("║  3. Редагувати книгу             ║");
    Console.WriteLine("║  4. Видалити книгу               ║");
    Console.WriteLine("║  5. Пошук книги                  ║");
    Console.WriteLine("║  6. Фільтрувати за статусом      ║");
    Console.WriteLine("║  7. Сортувати за роком видання   ║");
    Console.WriteLine("║  8. Сортувати за оцінкою         ║");
    Console.WriteLine("║  0. Вийти                        ║");
    Console.WriteLine("╚══════════════════════════════════╝");
    Console.Write("\nОберіть пункт: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            ShowBooks(service.GetAll());
            break;
        case "2":
            AddBook(service);
            break;
        case "3":
            EditBook(service);
            break;
        case "4":
            DeleteBook(service);
            break;
        case "5":
            SearchBooks(service);
            break;
        case "6":
            FilterBooks(service);
            break;
        case "7":
            ShowBooks(service.SortByYear());
            break;
        case "8":
            ShowBooks(service.SortByRating());
            break;
        case "0":
            return;
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

    Console.WriteLine("╔═════╦══════════════════════════════╦════════════════════╦══════╦═══════════════╦════════╦══════════════════════╗");
    Console.WriteLine("║ ID  ║ Назва                        ║ Автор              ║ Рік  ║ Жанр          ║ Оцінка ║ Статус               ║");
    Console.WriteLine("╠═════╬══════════════════════════════╬════════════════════╬══════╬═══════════════╬════════╬══════════════════════╣");

    foreach (var book in books)
    {
        Console.WriteLine($"║ {book.Id,-4}║ {book.Title,-29}║ {book.Author,-19}║ {book.Year,-5}║ {book.Genre,-14}║ {book.Rating,-7}║ {book.Status,-21}║");
    }

    Console.WriteLine("╚═════╩══════════════════════════════╩════════════════════╩══════╩═══════════════╩════════╩══════════════════════╝");
    Console.WriteLine($"\nВсього книг: {books.Count}");
    Console.ReadKey();
}

void AddBook(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║        ДОДАВАННЯ КНИГИ           ║");
    Console.WriteLine("╚══════════════════════════════════╝");

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
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║       РЕДАГУВАННЯ КНИГИ          ║");
    Console.WriteLine("╚══════════════════════════════════╝");
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
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║        ВИДАЛЕННЯ КНИГИ           ║");
    Console.WriteLine("╚══════════════════════════════════╝");
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
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║          ПОШУК КНИГИ             ║");
    Console.WriteLine("╚══════════════════════════════════╝");
    Console.Write("Введіть запит (назва, автор або жанр): ");
    var query = Console.ReadLine() ?? string.Empty;
    ShowBooks(service.Search(query));
}

void FilterBooks(BookService service)
{
    Console.Clear();
    Console.WriteLine("╔══════════════════════════════════╗");
    Console.WriteLine("║     ФІЛЬТРАЦІЯ ЗА СТАТУСОМ       ║");
    Console.WriteLine("╠══════════════════════════════════╣");
    Console.WriteLine("║  1. читаю                        ║");
    Console.WriteLine("║  2. прочитана                    ║");
    Console.WriteLine("║  3. хочу прочитати               ║");
    Console.WriteLine("╚══════════════════════════════════╝");
    Console.Write("Оберіть (1-3): ");

    var status = Console.ReadLine() switch
    {
        "1" => "читаю",
        "2" => "прочитана",
        _ => "хочу прочитати"
    };

    ShowBooks(service.FilterByStatus(status));
}