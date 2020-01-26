using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    public abstract class AbstractClass
    {
        public abstract int AbstractProperty { get; set; } //can declare abstract property
        public abstract void Print(); //it can't have body

        public void SelfPrint() //can declare a normal methods
        {
            Console.WriteLine("SelfPrint");
        }

        //But you can have constructors
        public AbstractClass()
        {
            Console.WriteLine("AbstractClass constructor");
        }

        public AbstractClass(int a)
        {
            Console.WriteLine("AbstractClass constructor with param");
        }
    }

    public abstract class AnotherAbstract : AbstractClass //another abstract, no needs to realize base abstraction
    {
        public abstract void AnotherAbstractPrint(); //can declare new abstracts
    }

    public class AbstractDerive : AnotherAbstract
    {
        private int _intValue;
        public override int AbstractProperty 
        { 
            get => _intValue; 
            set => _intValue = value; 
        }

        public override void AnotherAbstractPrint()
        {
            Console.WriteLine("AnotherAbstractPrint");
        }

        public override void Print()
        {
            Console.WriteLine("Print");
        }
    }
}
