using System;

namespace LibraryLibrary
{
    /* --- Delegate for Account Events --- */
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        public string Message { get; private set; }             // Event Message
        public string Name { get; private set; }                // Name of User
        public AccountEventArgs(string _message, string _name)  // Event Constructor
        {
            Message = _message;
            Name = _name;
        }
    }
}
