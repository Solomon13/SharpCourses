using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopSharp
{
    //Declare own exception
    public class MyOwnException : Exception
    {
        public int SomeState { get; set; }
    }

    public class ExceptionsWorker
    {
        private void ThrowException()
        {
            throw new MyOwnException { SomeState = 10 }; //generate new exception
        }

        public void HandleException()
        {
            try
            {
                ThrowException();
            }
            catch(MyOwnException own) 
            {
                Console.WriteLine(own.Message);
            }
            catch
            {
                Console.WriteLine("Catch as base");
            }
        }

        private void ThrowFromCatch()
        {
            try
            {
                ThrowException();
            }
            catch (MyOwnException e) 
            {
                Console.WriteLine("Catched");
                throw; //send next
            }
            catch // this will not handle
            {
                Console.WriteLine("Catched general");
            }
        }

        public void HandleException2()
        {
            try
            {
                ThrowFromCatch();
            }
            catch(MyOwnException e) when (e.SomeState == 10) //catch with conditions
            {
                Console.WriteLine(e.StackTrace); //review stack trace
            }
        }

        public void CatchByFinally()
        {
            try
            {
                ThrowException();
            }
            catch (Exception e) // use if you need exception object
            {
                Console.WriteLine(e.StackTrace); //review stack trace
                return;
            }
            finally //will work even if return
            {
                Console.WriteLine("In finally");
                //no catch here. But can be used for some resources releasing
            }
        }
    }
}
