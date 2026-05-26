using PersonalLibrary.Services;
using PersonalLibrary.UI;

var repository = new BookRepository();
var service = new BookService(repository);

while (true)
{
    MenuDisplay.Show();

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": BookDisplay.ShowBooks(service.GetAll()); break;
        case "2": BookInput.AddBook(service); break;
        case "3": BookInput.EditBook(service); break;
        case "4": BookInput.DeleteBook(service); break;
        case "5": SearchDisplay.SearchBooks(service); break;
        case "6": SearchDisplay.AdvancedSearchBooks(service); break;
        case "7": SearchDisplay.FilterBooks(service); break;
        case "8": BookDisplay.ShowBooks(service.SortByYear()); break;
        case "9": BookDisplay.ShowBooks(service.SortByRating()); break;
        case "10": ReportsDisplay.Inventory(service); break;
        case "11": ReportsDisplay.ShowReminders(service); break;
        case "12": ReportsDisplay.BackupDatabase(); break;
        case "0": return;
        default:
            Console.WriteLine("Невірний вибір!");
            Console.ReadKey();
            break;
    }
}