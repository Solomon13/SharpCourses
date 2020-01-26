using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    public class ClassWithGeneric
    {
        //simple generic method
        public void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }


        //Generic method for comparison and usage clarification
        public bool IsGreaterThan<T>(T actual, T comp) where T : IComparable<T>
        {
            return actual.CompareTo(comp) > 0;
        }

        //sum value in enumeration using adder
        public T Sum<T>(IEnumerable<T> values, IAdder<T> adder)
        {
            T result = adder.Zero;

            foreach (var value in values)
            {
                result = adder.Add(result, value);
            }

            return result;
        }

    }

    //Generic interface that can add items
    public interface IAdder<T>
    {
        T Zero { get; }

        T Add(T a, T b);
    }

    //correct realization
    public class Int32Adder : IAdder<Int32>
    {
        public static readonly Int32Adder Instance = new Int32Adder();

        public int Zero { get { return 0; } }

        public int Add(int a, int b) { return a + b; }
    }

}
