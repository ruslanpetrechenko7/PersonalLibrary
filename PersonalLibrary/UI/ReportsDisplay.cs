using PersonalLibrary.Services;

namespace PersonalLibrary.UI;

public static class ReportsDisplay
{
    public static void Inventory(BookService service)
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

    public static void ShowReminders(BookService service)
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║           НАГАДУВАННЯ              ║");
        Console.WriteLine("╚════════════════════════════════════╝");

        var books = service.GetAll()
            .Where(b => b.ReminderDate.HasValue)
            .OrderBy(b => b.ReminderDate)
            .ToList();

        if (books.Count == 0)
        {
            Console.WriteLine("Нагадувань немає.");
            Console.ReadKey();
            return;
        }

        var today = DateTime.Today;

        foreach (var book in books)
        {
            var days = (book.ReminderDate!.Value.Date - today).Days;
            var status = days < 0
                ? $"прострочено на {Math.Abs(days)} днів"
                : days == 0
                    ? "сьогодні!"
                    : $"через {days} днів";

            Console.WriteLine($"[{book.ReminderDate.Value:dd.MM.yyyy}] {book.Title} — {status}");
            if (!string.IsNullOrEmpty(book.Notes))
                Console.WriteLine($"  Нотатки: {book.Notes}");
        }

        Console.ReadKey();
    }

    public static void BackupDatabase()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║         РЕЗЕРВНА КОПІЯ             ║");
        Console.WriteLine("╚════════════════════════════════════╝");

        var source = "library.json";
        var backup = $"library_backup_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.json";

        if (!File.Exists(source))
        {
            Console.WriteLine("Файл бази даних не знайдено!");
            Console.ReadKey();
            return;
        }

        File.Copy(source, backup);
        Console.WriteLine($"\n✓ Резервну копію збережено: {backup}");
        Console.ReadKey();
    }
}