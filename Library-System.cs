using System; // para magamit ang basic functionalities gaya ng Console
using System.IO; // para sa file operations

class LibrarySystem {
    static string[] users = new string[10]; // array para sa 10 users
    static int userCount = 0; // bilang ng users na nadagdag

    static readonly string[] books = // list ng available books
    {
        "Diary of Wimp Kid",
        "Wansapanataym book",
        "Diary ng Lola ko",
        "ABaKaDa",
        "Holy Bible"
    };
    static int booksCount = 5; // bilang ng books na nasa list

    static readonly string[] borrowedBooks = new string[10]; // para sa books na nahiram
    static int borrowedCount = 0; // bilang ng books na nahiram

    static void Main() {
        bool running = true;  // flag para sa loop
        bool userAdded = false;  // check kung may user na nadagdag

        string filePath = "LibrarySystemOutput.txt"; // file path for output

        while (running) {  // habang running ang program
            Console.Clear();  // linisin ang screen tuwing start ng loop
            ShowMenu();  // ipakita ang main menu
            string choice = Console.ReadLine();  // basahin ang input ng user

            try {
                switch (choice) {  // depende sa pinili ng user
                    case "1":
                        AddUser(filePath);  // magdagdag ng user
                        userAdded = userCount > 0;  // kung may user na, set to true
                        break;
                    case "2":
                        if (!userAdded) {
                            Console.WriteLine("Please add a user first.");
                        } else {
                            ViewBooks();
                        }
                        break;
                    case "3":
                        if (!userAdded) {
                            Console.WriteLine("Please add a user first.");
                        } else {
                            BorrowBook(filePath);  // hiramin ang book
                        }
                        break;
                    case "4":
                        if (!userAdded) {
                            Console.WriteLine("Please add a user first.");
                        } else {
                            ReturnBook();
                        }
                        break;
                    case "5":
                        Console.WriteLine("Exiting...");
                        running = false;  // stop loop
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } catch (Exception ex) {
                Console.WriteLine($"An error occurred: {ex.Message}");
                File.AppendAllText(filePath, $"Error: {ex.Message}\n");
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }

    static void ShowMenu() {
        Console.WriteLine("---- Library System ----");
        Console.WriteLine("Add User [1]");
        Console.WriteLine("View Books [2]");
        Console.WriteLine("Borrow [3]");
        Console.WriteLine("Return [4]");
        Console.WriteLine("Exit [5]");
        Console.Write("Choice: ");
    }

    static void AddUser(string filePath) {
        if (userCount >= users.Length) {
            Console.WriteLine("User limit reached.");
            return;
        }
        Console.Write("Enter user name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Invalid name.");
            return;
        }

        users[userCount] = name;
        userCount++;
        Console.WriteLine($"User '{name}' added.");
        
        // i-save sa file
        File.AppendAllText(filePath, $"User Added: {name}\n");
    }

    static void ViewBooks() {
        if (booksCount == 0) {
            Console.WriteLine("No books available.");
            return;
        }
        Console.WriteLine("Available Books:");
        for (int i = 0; i < booksCount; i++) {
            Console.WriteLine($"{i + 1}. {books[i]}");
        }
    }

    static void BorrowBook(string filePath) {
        if (booksCount == 0) {
            Console.WriteLine("No books available to borrow.");
            return;
        }

        ViewBooks();
        Console.Write("Enter book number to borrow: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int bookIndex) || bookIndex < 1 || bookIndex > booksCount) {
            Console.WriteLine("Invalid selection.");
            return;
        }

        if (borrowedCount >= borrowedBooks.Length) {
            Console.WriteLine("Borrow limit reached.");
            return;
        }

        string borrowedBook = books[bookIndex - 1];
        borrowedBooks[borrowedCount] = borrowedBook;
        borrowedCount++;

        for (int i = bookIndex - 1; i < booksCount - 1; i++) {
            books[i] = books[i + 1];
        }
        booksCount--;

        Console.WriteLine($"You borrowed '{borrowedBook}'.");
        
        // i-save sa file
        File.AppendAllText(filePath, $"Book Borrowed: {borrowedBook}\n");
    }

    static void ReturnBook() {
        if (borrowedCount == 0) {
            Console.WriteLine("No borrowed books to return.");
            return;
        }

        Console.WriteLine("Borrowed Books:");
        for (int i = 0; i < borrowedCount; i++) {
            Console.WriteLine($"{i + 1}. {borrowedBooks[i]}");
        }

        Console.Write("Enter book number to return: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out int returnIndex) || returnIndex < 1 || returnIndex > borrowedCount) {
            return;
        }

        string returnedBook = borrowedBooks[returnIndex - 1];

        if (booksCount < books.Length) {
            books[booksCount] = returnedBook;
            booksCount++;
        } else {
            Console.WriteLine("Book storage full, cannot return book.");
            return;
        }

        for (int i = returnIndex - 1; i < borrowedCount - 1; i++) {
            borrowedBooks[i] = borrowedBooks[i + 1];
        }
        borrowedCount--;

        Console.WriteLine($"You returned '{returnedBook}'.");
    }
}
