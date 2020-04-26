using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Model
{
    public class User
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public enumGender Gender { get; private set; }

        public User(string firstName, string lastName, enumGender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
        }

        //public override string ToString()
        //{
        //    return $"{FirstName} {LastName}";
        //}
    }
}
