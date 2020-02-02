using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    public class DynamicLibraryTester
    {
        public void TestDynamicAssembly()
        {
            var assembly = Assembly.Load("TestAssembly"); //load assembly by name, be careful

            if (assembly == null)
                return;

            //find needed class by attribute and reflection
            var extentionType = assembly.GetTypes().Where(t => t.GetCustomAttribute<MathExtensionAttribute>() != null)
                                                    .FirstOrDefault();

            if (extentionType == null)
                return;

            //find needed method by attribute and reflection
            var methodToCall = extentionType.GetMethods().FirstOrDefault(m => m.GetCustomAttribute<MathExtensionMethodAttribute>() != null);

            if (methodToCall == null)
                return;

            var argsAttribute = methodToCall.GetCustomAttribute<MathExtensionMethodAttribute>();

            object[] args = new object[argsAttribute.ArgsCount];
            var random = new Random();

            for (int i = 0; i < argsAttribute.ArgsCount; i++)
                args[i] = random.Next(100);

            var instance = Activator.CreateInstance(extentionType);
            
            Console.WriteLine($"Dynamic call result = {methodToCall.Invoke(instance, args)}");

        }
    }
}
