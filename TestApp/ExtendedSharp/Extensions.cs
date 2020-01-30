using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    public class Extensions
    {
        class TestData
        {
            public string DataToSum { get; set; }
        }

        private TRes Sum<TItem, TRes>(IEnumerable<TItem> items, Func<TItem, TRes, TRes> SumFunc)
        {
            TRes result = default(TRes);

            foreach (TItem item in items)
                result = SumFunc(item, result);

            return result;
        }

        public void TestSum()
        {
            int[] intArray = new int[] { 2, 4, 6, 7 };
            double[] doubleArray = new double[] { 2.2, 3.3, 4.4, 5.5 };
            TestData[] testDataArray = new TestData[]
            {
                new TestData{ DataToSum = "a"},
                new TestData{ DataToSum = "b"},
                new TestData{ DataToSum = "c"}
            };


            int intRes = Sum<int, int>(intArray, (i, res) => i + res);
            double doubleRes = Sum<double, double>(doubleArray, (i, res) => i + res);
            string stringRes = Sum<TestData, string>(testDataArray, (i, res) => res + i.DataToSum);
        }

        class TestDataExtensions
        {
            public string DataToSum { get; set; }

            //pay attention on this overload, it will allow + usage
            public static TestDataExtensions operator +(TestDataExtensions t1, TestDataExtensions t2)
            {
                var data1 = t1?.DataToSum ?? string.Empty;
                var data2 = t2?.DataToSum ?? string.Empty;

                return new TestDataExtensions { DataToSum = data1 + data2 };
            }

            //now we can cast this class to string
            public static explicit operator string(TestDataExtensions data)
            {
                return data.DataToSum;
            }

            //now we can cast this class to string
            public static implicit operator int(TestDataExtensions data)
            {
                return data.DataToSum.Length;
            }
        }

        public void TestExtensions()
        {
            TestDataExtensions[] testDataArray = new TestDataExtensions[]
            {
                new TestDataExtensions{ DataToSum = "a"},
                new TestDataExtensions{ DataToSum = "b"},
                new TestDataExtensions{ DataToSum = "c"}
            };

            TestDataExtensions testDataRes = Sum<TestDataExtensions, TestDataExtensions>(testDataArray, (i, res) => res + i);

            string convertion = (string)testDataRes; //now it's possible
            int intConvertion = testDataRes; //direct cast to int possible now

            //here it's not possible because explicit operator string not exist for this class
            //TestData tAnother = new TestData { DataToSum = "SomeData" };
            //string anotherString = (string)TestData;
        }
    }
}
