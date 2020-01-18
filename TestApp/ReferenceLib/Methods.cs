using System.Linq;

namespace ReferenceLib
{
    public class Methods
    {
        public short Sum(short a, short b)
        {
            return (short)(a + b);
        }

        public int Sum(int a, int b) => a + b;

        public int SumDefault(int a, int b = 2) => a + b;

        public void SumOut(int a, int b, out int res) => res = a + b;

        public int SumRef(int a, ref int b /*= 8 default not possible*/)
        {
            return a + (b = 4);
        }

        public int AddWrapper(int x, int y) //internal func 
        {
            return Add();

            int Add()
            {
                return x + y;
            }
        }


        public int SumParams(params int[] args) => args.Sum();

        public void UseSum()
        {
            Sum(b : 2, a : 5);
            SumDefault(6);
            SumDefault(a : 6);
            SumOut(2, 3, out int res);

            SumRef(3, ref /*6* -> variable only*/ res);

            SumParams(1, 2, 3);

        }
    }
}
