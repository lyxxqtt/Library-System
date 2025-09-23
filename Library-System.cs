using System; // para magamit ang basic functionalities gaya ng Console

class LibrarySystem { // para magamit ang basic functionalities gaya ng Console
    static string[] users = new string[10]; // array para sa 10 users
    static int userCount = 0; // bilang ng users na nadagdag

    static string[] books = { // list ng available books
        "Diary of Wimp Kid",
        "Wansapanataym book 1",
        "Diary ng Lola ko",
        "ABaKaDa",
        "Holy Bible"
    };
    static int booksCount = 5; // bilang ng books na nasa list

    static string[] borrowedBooks = new string[10]; // para sa books na nahiram
    static int borrowedCount = 0; // bilang ng borrowed books

    static void Main() {
        bool running = true;  // flag para sa loop
        bool userAdded = false;  // check kung may user na nadagdag

        while (running) {  // habang running ang program
            Console.Clear();  // linisin ang screen tuwing start ng loop
            ShowMenu();  // ipakita ang main menu
            string choice = Console.ReadLine();  // basahin ang input ng user

            switch (choice) {  // depende sa pinili ng user
                case "1":
                    AddUser();  // magdagdag ng user
                    userAdded = userCount > 0;  // kung may user na, set to true
                    break;
                case "2":
                    if (!userAdded) {
                        Console.WriteLine("Please add a user first.");  // kailangan muna ng user
                    } else {
                        ViewBooks();  // ipakita ang books
                    }
                    break;
                case "3":
                    if (!userAdded) {
                        Console.WriteLine("Please add a user first.");
                    } else {
                        BorrowBook();  // hiramin ang book
                    }
                    break;
                case "4":
                    if (!userAdded) {
                        Console.WriteLine("Please add a user first.");
                    } else {
                        ReturnBook();  // isauli ang book
                    }
                    break;
                case "5":
                    Console.WriteLine("Exiting...");
                    running = false;  // stop loop
                    break;
                default:
                    Console.WriteLine("Invalid choice.");  // kung mali ang input
                    break;
            }

            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();  // hintayin ang user bago ulit linisin ang screen
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

    static void AddUser() {
        if (userCount >= users.Length) {
            Console.WriteLine("User limit reached.");  // max 10 users lang
            return;
        }
        Console.Write("Enter user name: ");
        string name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name)) {
            Console.WriteLine("Invalid name.");  // kung walang laman
            return;
        }

        users[userCount] = name;  // idagdag ang user
        userCount++;
        Console.WriteLine($"User '{name}' added.");
    }

    static void ViewBooks() {
        if (booksCount == 0) {
            Console.WriteLine("No books available.");  // walang book
            return;
        }
        Console.WriteLine("Available Books:");
        for (int i = 0; i < booksCount; i++) {
            Console.WriteLine($"{i + 1}. {books[i]}");
        }
    }


    static void BorrowBook()     static void BorrowBook() {
        if (booksCount == 0) {
            Console.WriteLine("No books available to borrow.");
            return;
        }

        ViewBooks();  // ipakita muna ang books
        Console.Write("Enter book number to borrow: ");
        string input = Console.ReadLine();
        int bookIndex;

        if (!int.TryParse(input, out bookIndex) || bookIndex < 1 || bookIndex > booksCount) {
            Console.WriteLine("Invalid selection.");  // maling number
            return;
        }

        if (borrowedCount >= borrowedBooks.Length) {
            Console.WriteLine("Borrow limit reached.");  // max 10 borrow
            return;
        }

        string borrowedBook = books[bookIndex - 1];  // kunin ang book
        borrowedBooks[borrowedCount] = borrowedBook;  // ilagay sa borrowed
        borrowedCount++;

        // shift books left
        for (int i = bookIndex - 1; i < booksCount - 1; i++) {
            books[i] = books[i + 1];
        }
        booksCount--;

        Console.WriteLine($"You borrowed '{borrowedBook}'.");
    }

    static void ReturnBook() {
        if (borrowedCount == 0) {
            Console.WriteLine("No borrowed books to return.");  // walang nahiram
            return;
        }

        Console.WriteLine("Borrowed Books:");
        for (int i = 0; i < borrowedCount; i++) {
            Console.WriteLine($"{i + 1}. {borrowedBooks[i]}");
        }

        Console.Write("Enter book number to return: ");
        string input = Console.ReadLine();
        int returnIndex;

        if (!int.TryParse(input, out returnIndex) || returnIndex < 1 || returnIndex > borrowedCount) {
            Console.WriteLine("Invalid selection.");
            return;
        }

        string returnedBook = borrowedBooks[returnIndex - 1];  // kunin ang isa-soli

        if (booksCount < books.Length) {
            books[booksCount] = returnedBook;  // idagdag sa available books
            booksCount++;
        } else {
            Console.WriteLine("Book storage full, cannot return book.");
            return;
        }

        // shift left sa borrowedBooks
        for (int i = returnIndex - 1; i < borrowedCount - 1; i++) {
            borrowedBooks[i] = borrowedBooks[i + 1];
        }
        borrowedCount--;

        Console.WriteLine($"You returned '{returnedBook}'.");
    }
`