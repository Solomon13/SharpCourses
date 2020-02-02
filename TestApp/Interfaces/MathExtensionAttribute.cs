using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MathExtensionAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class MathExtensionMethodAttribute : Attribute
    {
        public int ArgsCount;
    }
}
