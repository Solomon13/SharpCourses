using System;

namespace ReferenceLib
{
    public class LogicalOperations
    {
        public void If()
        {
            int a = 0;
            int b = 3;

            if (a > 0) //logical operations only
            {
                Console.WriteLine("1");
            }
            else if (b <= 3)
            {
                Console.WriteLine("2");
            }
            else
            {
                Console.WriteLine("3");
            }

        }

        public void ShortIf()
        {
            int a = 0;
            int b = 3;

            string str = b >= 3 ? "True If" : "False if"; //only assignment

            string str1 = b >= 3 ? a == 0 ? "1" : "2" : "3"; //complex if

            //b >= 3 ? Console.WriteLine("1") : Console.WriteLine("2"); not possible
        }

        public void ShortShortIf()
        {
            object o = null;

            object o1 = o ?? string.Empty; //only assignment
        }

        public void NullableSafeOperation()
        {
            string str = null;
            int length;

            if(str != null)
            {
                length = str.Length;
            }

            //Or
            length = str?.Length ?? 0;
            
            string newSring = str?.ToUpper();

            Console.WriteLine($"Not null length = {str?.Length}");

        }

        public void For()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(i);
            }

            //i = 9; i doesn't exist here

            for (; ; )//infinite
            {
                Console.WriteLine("Infinite");
                break; //go out
            }

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(i);
                continue; //go next
                Console.WriteLine("Not reached");
            }
        }

        public void While()
        {
            while (true) //infinite, condition
            {
                Console.WriteLine("Infinite");
                break; //go out
            }
        }

        public void DoWhile()
        {
            do
            {
                Console.WriteLine("Infinite");
                break; //go out
            }
            while (true); //infinite, condition
        }

        public void ForEach()
        {
            int[] arr = new[] { 1, 2, 3 };

            foreach (var i in arr)
                Console.WriteLine(i);

            var enumerator = arr.GetEnumerator();

            while (enumerator.MoveNext())
                Console.WriteLine(enumerator.Current);

        }

        public void Switch()
        {
            string langChoice = Console.ReadLine();
            int n = int.Parse(langChoice);

            switch (n)
            {
                case 1:
                    Console.WriteLine("1");
                    break;
                case 2:
                case 3:
                    Console.WriteLine("2 and 3");
                    break;
                default:
                    Console.WriteLine("All");
                    break;
            }

            switch (langChoice) // by string
            {
                case "C#":
                    Console.WriteLine("c#");
                    break;
                case "VB":
                    Console.WriteLine("VB");
                    break;
                default:
                    Console.WriteLine("All");
                    break;
            }
        }
    }
}