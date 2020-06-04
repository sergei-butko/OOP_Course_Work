using System;

namespace LibraryLibrary
{
    public class Book
    {
        private static int counter = 0;         // A Counter for Book Index
        public Book() { Index = ++counter; }    // Constructor for Book
        public int Index { get; set; }          // Property for Book Index
        public string Title { get; set; }       // Property for Book Title
        public string Author { get; set; }      // Property for Book Author
        public string Genre { get; set; }       // Property for Book Genre
    }
}
