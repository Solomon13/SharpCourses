using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedSharp
{
    public class LINQ
    {
        public void AnonymType()
        {
            //Create an anonym type
            var purchaseltem = new
            {
                TimeBought = DateTime.Now,
                ItemBought = new { Color = "Red", Make = "Saab", CurrentSpeed = 55 },
                Price = 34.000
            };

            //Can't change - read only
            //purchaseltem.Price = 2;
            //purchaseltem.TimeBought = DateTime.Now.AddMinutes(1);

            Console.WriteLine(purchaseltem.Price);
            Console.WriteLine(purchaseltem.GetType().ToString());
        }

        public void LinqDirect()
        {
            string[] currentVideoGames = { "Morrowind", "Uncharted 2", "Fallout 3", "Daxter", "System Shock 2" };

            //simple LINQ - strong typed
            IEnumerable<string> subset = from g in currentVideoGames //like foreach
                                         where g.Contains(" ") //condition check
                                         orderby g //results ordering
                                         select g; //result selection

            foreach (string s in subset)
                Console.WriteLine("Item: {0}", s);

            //simple LINQ - strong typed
            IEnumerable<int> subsetLength = from g in currentVideoGames //like foreach
                                            where g.Contains(" ") //condition check
                                            orderby g //results ordering
                                            select g.Length; //result selection

            //SQL like syntax
            var anotherSubset = from g in currentVideoGames select g.Substring(3);

        }

        public void LinqExtensionMethods()
        {
            string[] currentVideoGames = { "Morrowind", "Uncharted 2", "Fallout 3", "Daxter", "System Shock 2" };

            //simple LINQ - strong typed
            IEnumerable<string> subset = currentVideoGames.Where(g => g.Contains(" ")).
                                                           OrderBy(g => g).
                                                           Select(g => g);


            foreach (string s in subset)
                Console.WriteLine("Item: {0}", s);

            //simple LINQ - strong typed - IEnumereble<int>
            var subsetLength = currentVideoGames.Where(g => g.Contains(" ")).
                                                 OrderBy(g => g).
                                                 Select(g => g.Length);

            //SQL like syntax
            var anotherSubset = currentVideoGames.Select(g => g.Substring(3));
        }

        public void WithoutLinq()
        {
            string[] currentVideoGames = { "Morrowind", "Uncharted 2", "Fallout 3", "Daxter", "System Shock 2" };
            var gamesWithSpaces = new string[5];

            for (var i = 0; i < currentVideoGames.Length; i++)
            {
                if (currentVideoGames[i].Contains(" "))
                    gamesWithSpaces[i] = currentVideoGames[i];
            }

            // Sorting
            Array.Sort(gamesWithSpaces);

            // Show results
            foreach (string s in gamesWithSpaces)
            {
                if (s != null)
                    Console.WriteLine("Item: {0}", s);
            }
        }

        public void LinqDelayExecution()
        {
            List<string> currentVideoGames = new List<string> { "Morrowind 2", "Uncharted 2", "Fallout 3", "Daxter", "System Shock 2" };

            //Build query
            var subset = from g in currentVideoGames //like foreach
                         where g.Contains(" ") //condition check
                         orderby g //results ordering
                         select g; //result selection

            currentVideoGames.Remove(currentVideoGames.Last());
            bool bFirst = true;

            foreach (string s in subset)
            {
                Console.WriteLine("Item: {0}", s);

                if (bFirst)
                {
                    //this will not go to foreach
                    currentVideoGames.Add("Warcraft 3");
                    bFirst = false;
                }
            }

            var subsetList = subset.ToList();

            currentVideoGames.Remove(currentVideoGames.First());

            Console.WriteLine("=========================");

            foreach (string s in subsetList)
                Console.WriteLine("Item: {0}", s);
        }

        private List<string> _colors = new List<string> { "Red", "Green", "Dark Red", "Black" };
        private string _filter = "Red";

        private IEnumerable<string> GetFilteredColors()
        {
            return _colors.Where(c => c.Contains(_filter));
        }

        public void TestLinqMethod()
        {
            var colorsLinq = GetFilteredColors(); //Dangerous, be careful

            foreach (var c in colorsLinq)
                Console.WriteLine($"Color = {c}");

            _colors.Remove(_colors.First()); //remove original source

            Console.WriteLine("=========================");

            foreach (var c in colorsLinq)
                Console.WriteLine($"Color = {c}");

            Console.WriteLine("=========================");

            var collection = colorsLinq.ToList();

            _filter = "Green";

            foreach (var c in collection)
                Console.WriteLine($"Color = {c}");
        }

        public void TestLinqNonGeneric()
        {
            ArrayList arr = new ArrayList
            {
                "qwerty",
                "asdfg",
                "bbbb"
            };

            //LINQ will not work with non generic collections
            //arr.Where(i => i.Length > 2);

            //Use OfType here
            arr.OfType<string>().Where(i => i.Length > 2);
        }

        class ProductInfo
        {
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int NumberlnStock { get; set; } = 0;
        }

        public void TestAnonymLinq()
        {
            var itemsInStock = new[]
            {
                new ProductInfo
                { 
                    Name = "Mac's Coffee",
                    Description = "Coffee with TEETH",
                    NumberlnStock = 24
                },
                new ProductInfo
                {
                    Name = "Milk Maid Milk",
                    Description = "Milk cow's love",
                    NumberlnStock = 100
                },
                new ProductInfo
                { 
                    Name = "Pure Silk Tofu",
                    Description = "Bland as Possible",
                    NumberlnStock = 120
                },
                new ProductInfo
                { 
                    Name = "Crunchy Pops",
                    Description = "Cheezy, peppery goodness",
                    NumberlnStock = 2
                },
                new ProductInfo
                { 
                    Name = "RipOff Water",
                    Description = "From the tap to your wallet",
                    NumberlnStock = 100
                },
                new ProductInfo
                {
                    Name = "Classic Valpo Pizza",
                    Description = "Everyone loves pizza'",
                    NumberlnStock = 73
                }
            };

            //Build anonym here
            var nameDesc = (from p in itemsInStock select new { p.Name, p.Description }).ToArray();

            var item = nameDesc.FirstOrDefault(i => !string.IsNullOrEmpty(i.Description));

        }
    }
}
