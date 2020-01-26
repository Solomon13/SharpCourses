using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    //declare event args class
    public class ClassChangedEventArgs : EventArgs
    {
        public bool IsChanged { get; }
        public string ClassName { get; }

        public string Source { get; }

        public ClassChangedEventArgs(string className, string source, bool bIsChanged)
        {
            ClassName = className;
            IsChanged = bIsChanged;
            Source = source;
        }
    }

    public delegate void ClassChangedStateHandler(object obj, ClassChangedEventArgs args);
    public class WorkWithEvents
    {
        public event ClassChangedStateHandler ClassChangedState;

        private string _internalStr;

        public string SomeItem
        {
            set
            {
                _internalStr = value;

                //ClassChangedState can be null, so using safe call with ? 
                ClassChangedState?.Invoke(this, new ClassChangedEventArgs(typeof(WorkWithEvents).Name, nameof(SomeItem), true));
            }
        }
    }

    public class WorkWithEventsTester
    {
        public void TestEvents()
        {
            var classWithEvents = new WorkWithEvents();

            classWithEvents.ClassChangedState += (o, args) =>
            {
                Console.WriteLine(args.ClassName);
            };

            classWithEvents.SomeItem = "New Value";

            //oups - it will not usabscribe original lampda, because belove it's a new one
            //But no crash or exception
            classWithEvents.ClassChangedState -= (o, args) =>
            {
                Console.WriteLine(args.ClassName);
            };

            classWithEvents.SomeItem = "Once again new Value";
        }
    }
}
