using System;
using System.Text.RegularExpressions;
using LibraryLibrary;

namespace LibraryApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;       // A White Background Color
            Console.Clear();                                    // Full Background

            Library<Account> library = new Library<Account>("Student Library");     // Creating A New Library
            Console.ForegroundColor = ConsoleColor.Blue;                            // A Blue Library Name Color
            Console.WriteLine($"\n ------------------ Welcome to {library.Name}! ------------------- ");

            /* ---------- Adding Books to the Library List ---------- */
            library.AddBook("And Then There Were None", "Agatha Christie", "Mystery");
            library.AddBook("Angels & Demons", "Dan Brown", "Mysthery thriller");
            library.AddBook("Harry Potter and the Philosopher's Stone", "Joanne K. Rowling", "Fantasy, Mystery");
            library.AddBook("Lolita", "Vladimir Nabokov", "General");
            library.AddBook("Nineteen Eighty-Four", "George Orwell", "Dystopian novel");
            library.AddBook("Perfume", "Patrick Süskind", "Historical Fantasy novel");
            library.AddBook("Strange Case of Dr Jekyll and Mr Hyde", "Robert Louis Stevenson", "Gothic novel");
            library.AddBook("The Da Vinci Code", "Dan Brown", "Mysthery thriller");
            library.AddBook("The Girl with the Dragon Tattoo", "Stieg Larsson", "Psychological thriller");
            library.AddBook("The Great Gatsby", "Francis Scott Fitzgerald", "Novel");
            library.AddBook("The Hobbit", "John R.R. Tolkien", "Fantasy");
            library.AddBook("The Lion, the Witch and the Wardrobe", "Clive S. Lewis", "Fantasy");
            library.AddBook("The Little Prince", "Antoine de Saint-Exupery", "Fantasy (picture book)");
            library.AddBook("The Lord of the Rings", "John R.R. Tolkien", "Fantasy");
            library.AddBook("The Master and Margarita", "Mikhail Bulgakov", "Supernatural, Romance, Satirical novel");

            bool alive = true;
            while (alive)       // Till the User Choose to Close an Application
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;     // A Magenta Text Color
                Console.WriteLine("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("1. Create an Account\t 2. Show Book List\t 3. Find a Book");     //               //
                Console.WriteLine("4. Take a New Book  \t 5. Return a Book \t 6. Show My Books");   //  User's Menu  //
                Console.WriteLine("7. Delete an Account\t 8. Close Program");                       //               //
                Console.ForegroundColor = ConsoleColor.Black;    // A Black Text Color

                try
                {
                    Console.Write("Choose What to Do: ");               // Suggesting to make a Choice
                    int command = Convert.ToInt32(Console.ReadLine());  // User's Choice
                    switch (command)
                    {
                        case 1:
                            CreateAccount(library);     // Create an Account
                            break;
                        case 2:
                            ShowBookList(library);      // Show Book List
                            break;
                        case 3:
                            FindBook(library);          // Find Book
                            break;
                        case 4:
                            TakeBook(library);          // Take a New Book
                            break;
                        case 5:
                            ReturnBook(library);        // Return a Book
                            break;
                        case 6:
                            ShowMyBooks(library);       // Show User Books
                            break;
                        case 7:
                            DeleteAccount(library);     // Delete an Account
                            break;
                        case 8:
                            alive = false;              // Close Program
                            continue;
                        default:
                            throw new Exception("No such command was found!");  // If a Wrong Choice
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;     // A Red Error Message Color
                    Console.WriteLine(ex.Message);                      // Show Error Message
                }
            }
        }

        /* --- Method of Creating a New User Account --- */
        private static void CreateAccount(Library<Account> library)
        {
            Console.Write("\nEnter your name: ");
            string name = Console.ReadLine();   // Input of User Name
            if ((new Regex(" ")).Replace(name, "").Equals(""))  // If User Do not Input Name
                throw new Exception("You have not input your name.");

            AccountType accountType;
            Console.WriteLine("--- There are two account types ---");
            Console.WriteLine("1. For Student      2. For Teacher");
            Console.Write("Choose an account type: ");
            int choice = Convert.ToInt32(Console.ReadLine());   // Choosing an Account Type
            switch (choice)
            {
                case 1:
                    accountType = AccountType.Student;      // Creating Student Account
                    library.Create(accountType, name, TakeBookHandler, ReturnBookHandler,
                        CreateAccountHandler, DeleteAccountHandler);
                    break;
                case 2:
                    accountType = AccountType.Teacher;      // Creating Teacher Account
                    library.Create(accountType, name, TakeBookHandler, ReturnBookHandler,
                        CreateAccountHandler, DeleteAccountHandler);
                    break;
                default:    // If a Wrong Choice
                    throw new Exception("No such account type was found!");
            }
        }

        /* --- Method to Display all Available Books in Library --- */
        private static void ShowBookList(Library<Account> library)
        {
            Console.WriteLine("\nThese books are available in our library:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(" № |                  Title" +
                "                   |          Author          |      Genre");
            Console.ForegroundColor = ConsoleColor.Black;
            foreach (Book book in library)  // Iterate over all Library Books
                Console.WriteLine($"{book.Index.ToString().PadLeft(2)} |" +
                    $" {book.Title.PadRight(40)} | {book.Author.PadRight(24)} | {book.Genre}");
        }

        /* --- Method of Finding a Book --- */
        private static void FindBook(Library<Account> library)      // Method to Find a Book by Index
        {
            Console.WriteLine("\n----- There are three search methods ------");
            Console.WriteLine("1. By Title\t2. By Author\t3. By Genre");
            Console.Write("Choose search method: ");            // Choosing a Search Method
            int choice = Convert.ToInt32(Console.ReadLine());   // User's Choice
            string keyWord;
            switch (choice)
            {
                case 1:
                    Console.Write("Enter book title: ");
                    keyWord = Console.ReadLine();
                    break;
                case 2:
                    Console.Write("Enter book author: ");
                    keyWord = Console.ReadLine();
                    break;
                case 3:
                    Console.Write("Enter book genre: ");
                    keyWord = Console.ReadLine();
                    break;
                default:// If a Wrong Choice
                    throw new Exception("No such method was found!");
            }
            Book someBook = library.FindBook(choice, keyWord);   // Finding Book
            if (someBook == null)
                throw new Exception("No book was found!");
            else
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"{someBook.Index} - \"{someBook.Title}\". {someBook.Author}. {someBook.Genre}");
        }

        /* --- Method of Taking a New Book --- */
        private static void TakeBook(Library<Account> library)
        {
            Console.Write("\nEnter Your ID: ");
            int id = Convert.ToInt32(Console.ReadLine());   // Input of User ID
            if (id > library.CountAccounts() || id <= 0)    // If Wrong User ID
                throw new Exception("No user with such ID was found!");
            Account account = library.FindAccount(id);      // Finding Account by ID
            if (account.GetUserBooksCounter() == 10)        // If User Books List is Full
                throw new Exception("Sorry. Your list is already full.");
            Console.Write("Choose the Book Index: ");
            int index = Convert.ToInt32(Console.ReadLine());    // Input of Book Index
            if (index > library.GetCounter() || index <= 0)
                throw new Exception("No book with such index was found!");
            library.Take(index, id);    // Call of Taking Book Method
        }

        /* --- Method of Returning a Book --- */
        private static void ReturnBook(Library<Account> library)
        {
            Console.Write("\nEnter Your ID: ");
            int id = Convert.ToInt32(Console.ReadLine());   // Input of User ID
            if (id > library.CountAccounts() || id <= 0)    // If Wrong User ID
                throw new Exception("No user with such ID was found!");
            Account account = library.FindAccount(id);      // Finding Account by ID
            if (account.GetUserBooksCounter() == 0)         // If User Books List is Empty
                throw new Exception("You have no book to be returned.");
            Console.Write("Choose the Book Index: ");
            int index = Convert.ToInt32(Console.ReadLine());    // Input of Book Index
            if (index > library.GetCounter() || index <= 0)
                throw new Exception("No book with such index was found!");
            library.Return(index, id);      // Call of Returning Book Method
        }

        /* --- Method to Display all User Books --- */
        private static void ShowMyBooks(Library<Account> library)
        {
            Console.Write("\nEnter Your ID: ");
            int id = Convert.ToInt32(Console.ReadLine());   // Input of User ID
            if (id > library.CountAccounts() || id <= 0)    // If Wrong User ID
                throw new Exception("No user with such ID was found!");
            Account account = library.FindAccount(id);      // Finding Account by ID
            if (account.GetUserBooksCounter() == 0)         // If User Do not Have any Books
                throw new Exception("You do not have any taken books.");
            Console.WriteLine("These books you are already have:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (Book book in account)                  // Iterate over all User Books
                Console.WriteLine($"{book.Index} - \"{book.Title}\". {book.Author}. {book.Genre}");
        }

        /* --- Method of Deleting a User Account --- */
        private static void DeleteAccount(Library<Account> library)
        {
            Console.Write("\nEnter Your ID: ");
            int id = Convert.ToInt32(Console.ReadLine());   // Input of User ID
            if (id > library.CountAccounts() || id <= 0)    // If Wrong User ID
                throw new Exception("No user with such ID was found!");
            Account account = library.FindAccount(id);      // Finding Account by ID
            if (account.GetUserBooksCounter() != 0)         // If User Do not Returned All Books
                throw new Exception("You have not returned taken books!");
            library.Delete(id);     // Call of Deleting Account Method
        }

        /* --- Account Event Handlers --- */
        private static void CreateAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void TakeBookHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void ReturnBookHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void DeleteAccountHandler(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
