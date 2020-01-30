using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    //declare a delegate. 
    public delegate int BinaryOp(int x, int y);

    //this will mean somethig like this 

    //sealed class BinaryOp : System.MulticastDelegate
    //{
    //    public int Invoke(int x, int y);
    //    public IAsyncResult Beginlnvoke(int x, int y,
    //    AsyncCallback cb, object state);
    //    public int Endlnvoke(IAsyncResult result);
    //}

    //declare generic delegate
    public delegate T BinaryOp<T>(T x, T y);

    public class WorkWithDelegates
    {
        private int BinaryAnd(int x, int y)
        {
            Console.WriteLine($"x & y = {x & y}");
            return x & y;
        }

        private int BinaryOr(int x, int y)
        {
            Console.WriteLine($"x | y = {x | y}");
            return x | y;
        }

        public void TestDelegates()
        {
            var d = new BinaryOp(BinaryAnd); //create delegate instance

            Console.WriteLine(d(2, 7)); //call delegate
            Console.WriteLine(d.Invoke(2,7)); //call delegate directly
        }

        public void TestDelegatesGeneric()
        {
            var d = new BinaryOp<int>(BinaryAnd); //create delegate instance generic

            Console.WriteLine(d(2, 7)); //call delegate
            Console.WriteLine(d.Invoke(2, 7)); //call delegate directly
        }

        public void TestMulticast()
        {
            BinaryOp d = BinaryAnd; //you can skip the new init
            d += BinaryOr; //add more method

            var delegateRes = d(2, 7);  //will call all methods in the list
            Console.WriteLine(delegateRes); //but last will be as a result

            //call all methods dirrectly
            foreach (var method in d.GetInvocationList())
                Console.WriteLine(method.DynamicInvoke(2, 7));

            //unsubscribe 
            d -= BinaryOr;
            d -= BinaryAnd;

            if (d != null)
                d(5, 6); //will newer call, all unsubscribed, delegate empty
        }

        private void Write()
        {
            Console.WriteLine("QWERTY");
        }

        public int MultiplyTwice(int arg)
        {
            return arg * 2;
        }

        private void UseAction(Action act)
        {
            act();
        }

        private void UseActionGeneric(Action<int, int> act)
        {
            act(2,4);
        }

        private T UseFunc<T>(Func<T, T> func, T arg)
        {
            return func(arg);
        }

        public void TestStandardDelegates()
        {
            UseAction(new Action(Write));
            UseAction(Write); //the same 

            UseFunc(MultiplyTwice, 12);
        }

        public void TestAnonym()
        {
            //declare anonym method
            Action<int, int> act = delegate (int x, int y) { Console.WriteLine($"{x + y}"); };

            //use it as argument
            UseActionGeneric(act);

            //use lampda
            Func<int, int> func = (x) => 2 * x;

            Console.WriteLine(UseFunc(func, 10));

            //or
            Console.WriteLine(UseFunc((x) => 2 * x, 10));
        }

        public void UseLampda()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };

            //based on Func
            var newArr = arr.Where(i => i > 3);
        }
    }
}
