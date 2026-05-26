using PersonalLibrary.Models;

namespace PersonalLibrary.UI;

public static class BookDisplay
{
    public static void ShowBooks(List<Book> books)
    {
        Console.Clear();
        if (books.Count == 0)
        {
            Console.WriteLine("Книг не знайдено.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("╔═════╦══════════════════════════╦══════════════════════╦══════╦══════════════╦════════╦══════════════════════╗");
        Console.WriteLine("║ ID  ║ Назва                    ║ Автор                ║ Рік  ║ Розділ       ║ Оцінка ║ Статус               ║");
        Console.WriteLine("╠═════╬══════════════════════════╬══════════════════════╬══════╬══════════════╬════════╬══════════════════════╣");

        foreach (var book in books)
        {
            var title = book.Title.Length > 25 ? book.Title.Substring(0, 22) + "..." : book.Title;
            var author = book.Author.Length > 20 ? book.Author.Substring(0, 17) + "..." : book.Author;
            var section = book.Section.Length > 12 ? book.Section.Substring(0, 10) + ".." : book.Section;
            Console.WriteLine($"║ {book.Id,-4}║ {title,-25}║ {author,-21}║ {book.Year,-5}║ {section,-13}║ {book.Rating,-7}║ {book.Status,-21}║");
        }

        Console.WriteLine("╚═════╩══════════════════════════╩══════════════════════╩══════╩══════════════╩════════╩══════════════════════╝");
        Console.WriteLine($"\nВсього книг: {books.Count}");
        Console.ReadKey();
    }
}