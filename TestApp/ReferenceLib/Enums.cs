using System;

namespace ReferenceLib
{
    public class Enums
    {
        enum enumTypes
        { 
            Zero, // = О
            One, // = 1
            Two, // = 2
        }

        enum enumBytes : byte
        {
            Zero = 250, // = 25О
            One, // = 251
            Two = byte.MaxValue, // = 255
        }

        public void Enumeration()
        {
            foreach (var val in Enum.GetValues(typeof(enumTypes)))
                Console.WriteLine(val);

            foreach (var val in Enum.GetNames(typeof(enumTypes)))
                Console.WriteLine(val);
        }

        public void Parse()
        {
            var val = (enumTypes)Enum.Parse(typeof(enumTypes), "1");
        }

        public void UnderlyingType()
        {
            Console.WriteLine(Enum.GetUnderlyingType(typeof(enumBytes)));
        }
    }
}
