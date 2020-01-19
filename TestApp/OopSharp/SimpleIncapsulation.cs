using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    public class SimpleIncapsulation
    {
        private string _privateStr; //good option to hide class state

        public string SelfProperty { get; set; } = "Value"; //simple property with init value

        public string PublicStr //use Properties. 
        {
            get { return _privateStr; } //they have getter and setter methods
            set
            { 
                if(string.Compare(_privateStr, value, true) != 0) //use value keyword to detect the input
                {
                    _privateStr = value;
                }
            }
        }

        public string SelfWithPrivateSet { get; private set; } //you can use modificator 

        public SimpleIncapsulation()
        {
            SelfWithPrivateSet = "Assign private property"; //you can assign it everywhere in class. But not possible outside
        }
    }
}
