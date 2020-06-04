using System;
using System.Collections;
using System.Collections.Generic;

namespace LibraryLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler TakeBook;          // Event of Taking Book
        protected internal event AccountStateHandler ReturnBook;        // Event of Returning Book
        protected internal event AccountStateHandler CreateAccount;     // Event of Opening an Account
        protected internal event AccountStateHandler DeleteAccount;     // Event of Closing an Account

        private static int counter = 0;     // A Quantity of Active Accounts
        public Account(string name)         // Constructor for Account
        {
            Name = name;
            ID = ++counter;
        }
        public string Name { get; private set; }    // Property for User Name
        public int ID { get; private set; }         // Property for User ID

        private List<Book> userBooks = new List<Book>();        // User Books List
        public List<Book> GetUserBooks() { return userBooks; }  // Method to Get User Books List

        private int userBooksCounter = 0;                        // A Quantity of User Books
        public int GetUserBooksCounter() { return userBooksCounter; }   // Method to Get a Quantity of User Books
        
        /* --- Method of Adding a New Book in the User Books List --- */
        public void AddUserBook(Book book)
        {
            userBooks.Add(book);
            userBooksCounter++;
        }

        /* --- Method of Removing Book from the User Books List --- */
        public void RemoveUserBook(Book book)
        {
            userBooks.Remove(book);
            userBooksCounter--;
        }

        /* --- Method to Iterate over all User Books --- */
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < userBooks.Count; i++)
                yield return userBooks[i];
        }

        /* --- Method of Calling an Event --- */
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }

        /* --- Call of Separate Events. Defining of Virtual Method for Every Event --- */
        protected virtual void OnCreated(AccountEventArgs e) { CallEvent(e, CreateAccount); }
        protected virtual void OnDeleted(AccountEventArgs e) { CallEvent(e, DeleteAccount); }
        protected virtual void OnTook(AccountEventArgs e) { CallEvent(e, TakeBook); }
        protected virtual void OnReturned(AccountEventArgs e) { CallEvent(e, ReturnBook); }

        /* --- Method for Event of Taking a New Book --- */
        public virtual void Take()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;     // A Green Event Text Color
            OnTook(new AccountEventArgs($"{Name}, you have just taken a new book.", Name));
        }
        /* --- Method for Event of Returng a Book --- */
        public virtual void Return()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;     // A Green Event Text Color
            OnReturned(new AccountEventArgs($"Thank you, {Name}, your book was returned.", Name));
        }
        /* --- Method for Event of Creating a New Account  --- */
        protected virtual internal void Create()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;     // A Green Event Text Color
            OnCreated(new AccountEventArgs($"{Name}, your account has just been created. Account ID: {ID}.", Name));
        }
        /* --- Method for Event of Closing Account--- */
        protected internal virtual void Delete()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;     // A Green Event Text Color
            OnCreated(new AccountEventArgs($"{Name}, your account has just been deleted", Name));
        }
    }
}
