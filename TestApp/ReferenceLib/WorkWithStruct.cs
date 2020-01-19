using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceLib
{
    //sealed by default. Can't be derived from 
    struct Point : ISerializable //can realize interfaces
    {
        public int X;
        public int Y;

        //public Point() {}//you can't create default constructor. The same for finalizer

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Increment()
        {
            X++; Y++;
        }

        public void Decrement()
        {
            X--;
            Y--;
        }

        public void Display()
        {
            Console.WriteLine("X = {0}, Y = {1}", X, Y);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }

    public class WorkWithStruct
    {
        public void NewStruct()
        {
            Point point;
            point.X = 10;

            //pi.Display(); //error of struct usage. Y not assigned

            Point validPoint = new Point();
            validPoint.Display(); //it's good. X and Y has default values
        }
    }
}
