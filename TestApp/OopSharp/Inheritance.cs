using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    public interface IBaseInterface
    {
        int SomeProperty { get; set; } // you can declare only properies in interfaces
    }

    public interface IFirstInreface : IBaseInterface //can derive from another interface
    {
        //int SomeProperty; -> can't contain fields
        int AnotherProperty { get; /*private set;*/ } // -> you can't use 'private' in interface 
        /*public*/ void DoMethod(); //access modificator not used, even if it's public. At least in C# 7

    }

    public interface ISecondInreface
    {
        void DoMethod(); //Declare method with the same name

    }

    public class BaseClass /*: StaticClass -> can't derive from static class*/ 
    {
        private int PrivateProperty { get; set; }
        protected int PropectedProperty { get; set; }

        public int PublicProperty { get; set; }

        public virtual void Print() //polymorph method
        {
            Console.WriteLine("BaseClass::Print");
        }

        protected BaseClass(int privatePropValue) //protected constructor
        {
            PrivateProperty = privatePropValue;
        }

        public BaseClass()
        {

        }
    }

    public class Inheritance : BaseClass, IFirstInreface, ISecondInreface
    {
        public int SomeProperty { get; set; } //Interface inheritance with public modificator

        public int AnotherProperty { get; }

        void IFirstInreface.DoMethod() //when we have the same names, you can use a direct interface naming
        {
            Console.WriteLine("IFirstInreface.DoMethod()");
        }

        void ISecondInreface.DoMethod()
        {
            Console.WriteLine("ISecondInreface.DoMethod()");
        }

        public Inheritance() : base(5) // -> init base private prop via protected constructor
        {
            //PrivateProperty = 5; -> not possible
            PropectedProperty = 10; //possible, because of protected
            PublicProperty = 9; //posible, because of public
            //DoMethod(); // not possible.
            
            ((ISecondInreface) this).DoMethod(); //cast to needed interface
        }

        public override void Print() //ovveride the method from BaseClass
        {
            Console.WriteLine("Inheritance::Print");
        }

        public override bool Equals(object obj) //override check for equals from object
        {
            var inharitance = obj as Inheritance;

            if (inharitance == null)
                return false;

            return base.Equals(obj); // we can redirect call next to parent, using base keyword
        }
    }

    public sealed class FinalInheritance : Inheritance //prevent from making deriving from this class
    {
        public new void Print() //use new, if you want re-declare the method. Be careful, it's not override
        {
            Console.WriteLine("FinalInheritance::Print");
        }
    }

    // Can't use FinalInheritance as base, because it's sealed
    //public class FinalFinalInheritance : FinalInheritance
    //{

    //}

    public class InheritanceTester
    {
        public void TestPolymorph()
        {
            var finalInheritance = new FinalInheritance();
            finalInheritance.Print(); //will print "FinalInheritance::Print"

            Inheritance inheritance = (Inheritance)finalInheritance;
            inheritance.Print(); //will "Inheritance::Print"

            BaseClass baseClass = inheritance as BaseClass;
            baseClass.Print(); //"Inheritance::Print"

            var selfBase = new BaseClass();
            selfBase.Print(); //"BaseClass::Print"
        }
    }
}
