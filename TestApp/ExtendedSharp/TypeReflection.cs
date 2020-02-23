using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    class TypeReflection
    {
        private string PrivateField;

        protected int ProtectedProperty { private get;  set; }

        public void TestMethod()
        {
            Console.WriteLine("Test Method");
        }

        public void TestMethodWithArg(ref int arg, out string str)
        {
            Console.WriteLine($"Input Arg = {arg}");
            arg = 10;
            str = "QWERT";
            
        }

        public static void StaticMethod()
        {
            Console.WriteLine("Static Method");
        }
    }

    public class TypeReflectionTester
    {
        public void DetectType()
        {
            var reflector = new TypeReflection();

            var t1 = reflector.GetType();
            var t2 = typeof(TypeReflection);
            var t3 = Type.GetType("ExtendedSharp.TypeReflection");

            if(t1.ToString() == t2.ToString() && t2.ToString() == t3.ToString())
            {
                Console.WriteLine("Types Equal");
            }
        }

        public void InvokeMethodNoArgs()
        {
            var t1 = typeof(TypeReflection);

            //find needed method
            var methodInfo = t1.GetMethods().FirstOrDefault(t => t.Name.StartsWith("TestMethod"));

            //create instance
            var reflector = Activator.CreateInstance(t1);

            methodInfo.Invoke(reflector, null);

            var r2 = new TypeReflection();
            methodInfo.Invoke(r2, null);
        }

        public void InvokeMethodWithArgs()
        {
            var t1 = typeof(TypeReflection);

            //Find needed method
            var methodInfo = t1.GetMethods().FirstOrDefault(t => t.Name.StartsWith("TestMethodWithArg"));

            //Create instance via reflection
            var reflector = Activator.CreateInstance(t1);
            //Prepare argiments to use as ref and out
            object[] arguments = new object[] { 5, null };

            //invoke
            methodInfo.Invoke(reflector, arguments);

            Console.WriteLine($"Out Arg1 = {arguments[0]}, Out Args2 = {arguments[1]}");
        }

        public void InvokeStatic()
        {
            var t1 = typeof(TypeReflection);

            //Find needed method
            var methodInfo = t1.GetMethods().FirstOrDefault(t => t.Name.StartsWith("StaticMethod"));

            //invoke static, no instance
            methodInfo.Invoke(null, System.Reflection.BindingFlags.Static, null, null, null);
        }

        public void InvokePrivate()
        {
            var t1 = typeof(TypeReflection);

            //Find needed private field
            var fieldInfo = t1.GetField("PrivateField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            //Create instance
            var reflector = Activator.CreateInstance(t1);

            fieldInfo.SetValue(reflector, "New value");

            //Find needed property
            var propInfo = t1.GetProperty("ProtectedProperty", System.Reflection.BindingFlags.NonPublic | 
                                                            System.Reflection.BindingFlags.Instance);

            var setter = propInfo.GetSetMethod(true); //true for non public

            if (setter != null)
                setter.Invoke(reflector, new object []{ 15 });
        }

       public void OtherAbilities()
       {
            var t1 = typeof(TypeReflection);

            var interfaces = t1.GetInterfaces();
            var attributes = t1.GetCustomAttributes(typeof(Attribute), true);
        }
    }
}
