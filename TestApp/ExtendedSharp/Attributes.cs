using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    //where you can adjust attribute usage - for class, method, interface...
    [AttributeUsage(AttributeTargets.Class)] 
    public class ForClassOnlyAttribute : Attribute
    {
        public string Description { get; }
        public ForClassOnlyAttribute(string desc)
        {
            Description = desc;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = true)] //use binary combination
    public class ForFieldAndPropertyAttribute : Attribute
    {
        public string FieldDescription { get; set; }
        public int Order { get; set; }

        public void AttributeMethod()
        {
            Console.WriteLine($"We are in attribute method, order = {Order}");
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)] //use binary combination
    public class ForFuncArgAttribute : Attribute
    {
        public string ParamDescription { get; set; }
    }

    [ForClassOnly("My attribute usage")] //pay attention to attribute usage - Attribute  is missed
    public class ClassWithAttributes
    {
        //[ForClassOnly("No you can't")]
        public int IntProperty { get; set; }

        [ForFieldAndProperty(Order = 1)] //you can use like this even if you don't have constructor
        public int IntPropertyWithAttribute { get; set; }

        //you can use many
        [ForFieldAndProperty(Order = 1, FieldDescription = "Simple attribute")]
        [ForFieldAndProperty(Order = 2, FieldDescription = "Another simple attribute")]
        private string _field;

        //use arguments for func parameters
        public int Sum([ForFuncArg(ParamDescription = "Description for arg1")] int arg1,
                       [ForFuncArg(ParamDescription = "Description for arg1")] int arg2)
        {
            return arg1 + arg2;
        }
    }

    public class AttributesTester
    {
        public void TestClassAttribute()
        {
            var t = typeof(ClassWithAttributes);
            var attributes = t.GetCustomAttributes(false); //true if you want allow inharit

            foreach(var a in attributes)
            {
                Console.WriteLine(a.GetType().ToString());
            }
        }

        public void TestFieldAttribute()
        {
            //use refrection
            var t = typeof(ClassWithAttributes);
            var fieldInfo = t.GetField("_field", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var attributes = fieldInfo.GetCustomAttributes(typeof(ForFieldAndPropertyAttribute), false);

            //use cast, because GetCustomAttributes return Attrubute class
            foreach (var a in attributes.OfType<ForFieldAndPropertyAttribute>())
            {
                a.AttributeMethod(); //you can use methods if you want
            }
        }
    }
}
