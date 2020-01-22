using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    public partial class PartialClass
    {
        public void MethodInPartialTwo()
        {
            Console.WriteLine("MethodInPartialTwo");
        }
    }

    public class PartialVerify
    {
        void TestPartial()
        {
            var partial = new PartialClass();

            //both methods available
            partial.MethodInPartialOne();
            partial.MethodInPartialTwo();
        }
    }
}
