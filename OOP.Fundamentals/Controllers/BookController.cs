using OOP.Fundamentals.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Fundamentals.Controllers
{
    internal class BookController : BaseController, IBaseController
    {
        public void AddItem()
        {
            var title = AnsiConsole.Ask<string>("Enter the [green]title[/] of the book to add:");
            var author = AnsiConsole.Ask<string>("Enter the [green]author[/] of the book:");
            var category = AnsiConsole.Ask<string>("Enter the [green]category[/] of the book:");
            var location = AnsiConsole.Ask<string>("Enter the [green]location[/] of the book:");
            var pages = AnsiConsole.Ask<int>("Enter the [green]number of pages[/] in the book:");

            if (MockDatabase.LibraryItems.OfType<Book>().Any(b => b.Name.Equals(title, StringComparison.OrdinalIgnoreCase)))
            {
                DisplayMessage("This book already exists", "red");
            }
            else
            {
                var newBook = new Book(MockDatabase.LibraryItems.Count + 1, title, author, category, location, pages);
                MockDatabase.LibraryItems.Add(newBook);
                DisplayMessage("Book added successfully!", "green");
            }

            DisplayMessage("Press Any Key to Continue.");
            Console.ReadKey();
        }

        public void DeleteItem()
        {
            var books = MockDatabase.LibraryItems.OfType<Book>().ToList();

            if (books.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No books available to delete.[/]");
                Console.ReadKey();
                return;
            }

            var bookToDelete = AnsiConsole.Prompt(
                new SelectionPrompt<Book>()
                    .Title("Select a [red]book[/] to delete:")
                    .UseConverter(b => $"{b.Name} by {b.Author}")
                    .AddChoices(books));

            if (ConfirmDeletion(bookToDelete.Name))
            {
                if (MockDatabase.LibraryItems.Remove(bookToDelete))
                {
                    DisplayMessage("Book deleted successfully!", "red");
                }
                else
                {
                    DisplayMessage("Book not found");
                }
            }   else
            {
                DisplayMessage("Deletion canceled.", "yellow");
            }

            AnsiConsole.MarkupLine("Press Any Key to Continue.");
            Console.ReadKey();
        }

        public void ViewItems()
        {
            var table = new Table();

            table.Border(TableBorder.Rounded);
            table.AddColumn("[yellow]ID[/]");
            table.AddColumn("[yellow]Title[/]");
            table.AddColumn("[yellow]Author[/]");
            table.AddColumn("[yellow]Category[/]");
            table.AddColumn("[yellow]Location[/]");
            table.AddColumn("[yellow]Pages[/]");

            var books = MockDatabase.LibraryItems.OfType<Book>();

            foreach (var book in books)
            {
                table.AddRow(
                    book.Id.ToString(),
                    $"[cyan]{book.Name}[/]",
                    $"[cyan]{book.Author}[/]",
                    $"[green]{book.Category}[/]",
                    $"[blue]{book.Location}[/]",
                    book.Pages.ToString()
                );
            }

            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("Press Any Key to Continue.");
            Console.ReadKey();
        }
    }
}
