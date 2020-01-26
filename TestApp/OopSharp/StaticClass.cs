using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console; //pay attention

namespace OopSharp
{
    //can't be used as base class
    public static class StaticClass /*: SimpleClass can't derive from another class*/
    {
        #region Static area
        public static double PI = 3.14; //init in static constructor first
        public static double AnotherPI;

        /*public*/
        static StaticClass() //can be only one. No access mofiicator, no arguments
        {
            AnotherPI = 3.14; //use for static fields initialization
        }

        //StaticClass() { } //instance constructors not supported

        public static void StaticMethod() //static methods only
        {

        }

        //use static and this for methods extension
        public static string ToUpperLast(this string str)
        {
            WriteLine("It's simple Simple"); //Console writing not neccessary, because using static below

            char c = str.Last();

            str = str.Remove(str.Length - 1); //string is immutable
            
            return str + char.ToUpper(c);
        }

        //use static and this for methods extension - can be applied for interfaces
        public static void EnumerableExtension(this IEnumerable<object> enumerable)
        {

        }

        #endregion
    }
}
