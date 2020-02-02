using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAssembly
{
    [MathExtension]
    public class MathExtentionsClass
    {
        [MathExtensionMethod(ArgsCount = 2)]
        public int SumMethod(int a, int b)
        {
            return a + b;
        }
    }
}
