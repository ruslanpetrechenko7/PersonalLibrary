namespace PersonalLibrary.UI;

public static class MenuDisplay
{
    public static void Show()
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
        Console.WriteLine("║  11. Нагадування                   ║");
        Console.WriteLine("║  12. Резервна копія                ║");
        Console.WriteLine("║  0. Вийти                          ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        Console.Write("\nОберіть пункт: ");
    }
}