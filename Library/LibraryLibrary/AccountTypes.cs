using System;

namespace LibraryLibrary
{
    public class StudentAccount : Account   // Student Account
    {
        public StudentAccount(string name) : base(name) { } // Constructor for Student Account
        protected internal override void Create()   // Event of Creating Student Account
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;     // A Green Event Text Color
            base.OnCreated(new AccountEventArgs($"{Name}, your student account has just been created. Account ID: {ID}.", Name));
        }
    }
    public class TeacherAccount : Account   // Teacher Account
    {
        public TeacherAccount(string name) : base(name) { } // Constructor for Teacher Account
        protected internal override void Create()   // Event of Creating Teacher Account
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;     // A Green Event Text Color
            base.OnCreated(new AccountEventArgs($"{Name}, your teacher account has just been created. Account ID: {ID}.", Name));
        }
    }
}
