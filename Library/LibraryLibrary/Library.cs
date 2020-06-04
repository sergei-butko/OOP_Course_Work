using System;
using System.Collections;
using System.Collections.Generic;

namespace LibraryLibrary
{
    public enum AccountType // Enumeration of Account Types
    {
        Student,
        Teacher
    }
    public class Library<T> where T : Account
    {
        public string Name { get; private set; }            // Proverty for Library Name
        public Library(string name) { this.Name = name; }   // Constructor for Library

        private List<Book> books = new List<Book>();        // A List of Books in the Library
        private int booksCounter = 0;                       // A Quantity of Books in the Library
        public int GetCounter() { return booksCounter; }    // Method to Know a Quantity of Books in the Library
        public void AddBook(string _title, string _author, string _genre)   // Method to Add a New Book to Library Book List
        {
            books.Add(new Book() { Title = _title, Author = _author, Genre = _genre });
            booksCounter++;     // Increment of Book Quantity
        }

        private T[] accounts;   // Users' Accounts List
        public int CountAccounts()  // Method to Know a Quantity Library Users
        {
            if (accounts != null)
                return accounts.Length;
            else
                return 0;
        }

        /* --- Method of Creating a New User Account --- */
        public void Create(AccountType accountType, string name, AccountStateHandler takeBookHandler,
            AccountStateHandler returnBookHandler, AccountStateHandler createAccountHandler, AccountStateHandler deleteAccountHandler)
        {
            T newAccount = null;    // Creating an Empty Account
            switch (accountType)
            {
                case AccountType.Student:
                    newAccount = new StudentAccount(name) as T;     // If Account for Student
                    break;
                case AccountType.Teacher:
                    newAccount = new TeacherAccount(name) as T;     // If Account for Teacher
                    break;
            }
            if (accounts == null)                   // If there are no Active User's Accounts in the Library
                accounts = new T[] { newAccount };  // Creating a New Account List with New User's Account
            else
            {   // Adding a New User Account to Accounts List
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; i++)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length - 1] = newAccount;
                accounts = tempAccounts;
            }
            // Adding Account's Events Handlers
            newAccount.TakeBook += takeBookHandler;
            newAccount.ReturnBook += returnBookHandler;
            newAccount.CreateAccount += createAccountHandler;
            newAccount.DeleteAccount += deleteAccountHandler;

            newAccount.Create();    // Event of Creating a New Account
        }

        /* --- Method to Iterate over all Library Books --- */
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < books.Count; i++)
                yield return books[i];
        }

        /* --- Method of Finding a Book by Index --- */
        public Book FindBook(int choice, string keyWord)
        {
            switch (choice)
            {
                case 1:
                    for(int i = 0; i < books.Count; i++)
                        if (String.Compare(keyWord, books[i].Title, true) == 0)
                            return books[i];
                    break;
                case 2:
                    for (int i = 0; i < books.Count; i++)
                        if (String.Compare(keyWord, books[i].Author, true) == 0)
                            return books[i];
                    break;
                case 3:
                    for (int i = 0; i < books.Count; i++)
                        if (String.Compare(keyWord, books[i].Genre, true) == 0)
                            return books[i];
                    break;
                default:
                    break;
            }
            return null;
        }

        /* --- Method of Taking a New Book --- */
        public void Take(int index, int id)
        {
            Account account = FindAccount(id);  // Finding User Account by ID
            if (account == null)    // If Account Was not Found
                throw new Exception("Account was not been found!");
            bool flag = true;
            for (int i = 0; i < books.Count; i++)
                if (books[i].Index == index)
                {
                    flag = false;
                    account.AddUserBook(books[i]);  // Adding Book to User Books List
                    books.Remove(books[i]);         // Removing Taken Book from Library Book List
                    account.Take();     // Event of Taking a New Book
                }
            if (flag)
                throw new Exception("This book is not available now.");
        }

        /* --- Method of Returning a Book --- */
        public void Return(int index, int id)
        {
            Account account = FindAccount(id);  // Finding User Account by ID
            if (account == null)    // If Account Was not Found
                throw new Exception("Account was not been found!");
            List<Book> userBooks = account.GetUserBooks();  // Getting User Books List
            for (int i = 0; i < userBooks.Count; i++)
                if (userBooks[i].Index == index)
                {
                    books.Add(userBooks[i]);    // Returning Book to the Library Book List
                    books.Sort(delegate (Book x, Book y)    // Sorting Books in the Library Book List
                    {
                        return x.Index.CompareTo(y.Index);
                    });
                    account.RemoveUserBook(userBooks[i]);   // Removing Returned Book from User Books List
                    account.Return();   // Event of Returning a New Book
                }
        }

        /* --- Method of Deleting a User Account --- */
        public void Delete(int id)
        {
            int index;  // Index of Account that is Going to be Deleted
            Account account = FindAccount(id, out index);   // Finding User Account by ID and returning its Index
            if (account == null)    // If Account Was not Found
                throw new Exception("Account was not been found!");

            if (accounts.Length <= 1)   // If no Other Accounts in Accounts List
                accounts = null;        // Making Account List Empty
            else
            {   // Reducing of Account List Size After Deleting Account
                T[] tempAccounts = new T[accounts.Length - 1];
                for (int i = 0, j = 0; i < accounts.Length; i++)
                    if (i != index)
                        tempAccounts[j++] = accounts[i];
                accounts = tempAccounts;
            }
            account.Delete();   // Event of Deleting Account
        }

        /* --- Functions of Finding an Account by Index --- */
        public Account FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; i++)
                if (accounts[i].ID == id)
                    return accounts[i];
            return null;    // If Account Was not Found
        }
        public Account FindAccount(int id, out int index)     // Overloaded Function
        {
            for (int i = 0; i < accounts.Length; i++)
                if (accounts[i].ID == id)
                {
                    index = i;
                    return accounts[i];
                }
            index = -1;
            return null;    // If Account Was not Found
        }
    }
}
