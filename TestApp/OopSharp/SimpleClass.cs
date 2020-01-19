using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    public class SimpleClass //Internal by default
    {
        private enum enumColor //private embeded type
        {
            Red, Green, Blue
        }

        //use regions
        #region Fields
        string PrivateField; //private by default
        string FieldWithData = "SomeData"; //The same if define in default constructor
        int PrivateInt;
        int InitWithConstructor;
        #endregion

        #region Constructors
        private SimpleClass()  => PrivateInt = 10; //private constructor

        public SimpleClass(string s, int a)
        {
            PrivateField = s;
            InitWithConstructor = a;
            Console.WriteLine("SimpleClass 2 args");
        }

        public SimpleClass(string s) : this(s, default(int)) //use another constructor, but private constructor will not call
        {
            Console.WriteLine("SimpleClass 1 args");
        }

        //this usage difference 
        public SimpleClass(int InitWithConstructor) : this() //use private cinstructor
        {
            this.InitWithConstructor = InitWithConstructor; //use to identify the class member
            Console.WriteLine("SimpleClass 1 args");
        }
        #endregion

        #region Methods
        void PrivateMethod() { }//private by default
        #endregion
    }
}
