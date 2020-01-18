using System; //declaration of using the namespace. Will be availabele everywhere in current file
using Int = System.Int64; //applying the synonnym usage

namespace ReferenceLib
{
    using System.Linq;     //declaration of using the namespace. Will be available below the current namespace          

    public class WorkWithTypes
    {
        public void SimpleTypes()
        {
            int intVar = 4;                 //int declaration using the keyword - int
            Int32 intVarName = 9;           //int declaration using the class name - Int32
            System.Int32 intVarFullName = 8; // int declaration using the full name System.Int32
            Int synVar = 8;                  //use synonym var type
        }

        public void VarType()
        {
            var contextVar = "str";         //str declaration of variable. Type will be detected from the context

            //var unsupported;              you can't declare var without assigning the value
            var boolType = new[] { 1, 2, 3 }.Any(); //var is useful with LINQ
        }

        public void TypesFromContext()
        {
            float fType = (float)2.2; //requires casting, because numbers with point has double type by default
            float fTypeWithF = 2.2f;  //use f literal to tell that type is float

            int iType = 5;
            byte sType = (byte)(iType + 67); //byte convertion needed, because iType+ 67 = int type
        }

        public void ObjectBaseClass()
        {
            int iType = 2;
            object oType = iType;
            WorkWithTypes refType = new WorkWithTypes();

            Console.WriteLine(iType);            //Outputs the 2
            Console.WriteLine(iType.ToString()); //outputs 2 but interpreted as string type
            Console.WriteLine(oType.ToString()); //outputs 2 again, because underlying type is Int
            Console.WriteLine(refType.ToString()); //outputs full class name by default
        }

        public void BoxingUnboxing()
        {
            double dType1 = 2.3; //value type in stack
            double dType2 = new double(); // value type in stack again

            object oType = dType1;        //boxing. dType1 value will be copyed to heap. oType as a reference will be available in stack
            double dType3 = (double)oType; //unboxing. Cast to double neccessary 
            int iType = (int)oType;        //Runtime exception. Invalid cast

            WorkWithTypes refType = new WorkWithTypes(); //reference type. Declared at heap
        }

        public void DefaultWord()
        {
            int iType = default(int); //0
            int iType1 = 0;

            string strType = default(string); //string null
            string strType1 = string.Empty; //string empty constant
            string strType2 = ""; //string empty string

            object oType = default(object); //null
            object oType1 = null;

            WorkWithTypes refType = default(WorkWithTypes); //null
            WorkWithTypes refType1 = null;
        }

        public void NullableTypes()
        {
            int iType = 0;
            //int iType1 = null;    error, int isn't nullable
            int? iNullType = null; //nullable int
            int? iNullTypeWithValue = 2; //nullable int with value

            Console.WriteLine(iNullType.HasValue); //True if not null

            iType = (int)iNullTypeWithValue; //direct cast to int neccessary
            iType = iNullTypeWithValue.Value; //cast not neccessary

            Console.WriteLine(iNullType.Value); //exception if null
        }

        public void Checked()
        {
            byte b1 = 250;
            byte bOverflow = (byte)(b1 + 50); //overflow = 44. No exception

            byte bNoOverflow = checked((byte)(b1 + 50)); // Checked - exception
        }

        public void Cast()
        {
            int a = 123;
            double d = a; //possible without direct cast

            short s = (short)a; //direct cast

            short s1 = short.Parse("1"); //parse to short. Exception if not possible
            bool isParse = short.TryParse("2", out short res); //No exception. False result if not possible

            int a1 = Convert.ToInt32(s1); //Converter helper class
        }
    }
}
