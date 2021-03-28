using System;
namespace FuzzyMatching.RunTime
{
    class RunTime
    {
        static void Main(string[] args)
        {
            int[] sizes = new int[7] { 10, 100, 1000, 10000, 25000, 50000, 100000 };
            foreach (var size in sizes)
            {
                Console.WriteLine("Hello World!");
                var matcher = new Algorithms.FuzzyMatching(size, @"C:\Users\v-kelhammady\OneDrive - Microsoft\Documents\GitHub\FuzzyMatching\LargeDataset.csv");
                DateTime start = DateTime.Now;
                //var result = GetClosestSentence("I want a transport for london");
                var result = matcher.MatchSentence("I want a transport for london");
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                Console.WriteLine("Elapsed Time for the program with size {0} is {1} s",size, ts.TotalSeconds);
                var closestMatch = result.Item1;
                Console.WriteLine(closestMatch);
                var index = result.Item2;
                Console.WriteLine(index);
                var score = result.Item3;
                Console.WriteLine(score);
            }
            
        }
        public static (string, float, int) GetClosestSentence(string Sentence)

        {
            var matcher = new Algorithms.FuzzyMatching(100, @"C:\Users\v-kelhammady\OneDrive - Microsoft\Documents\GitHub\FuzzyMatching\LargeDataset.csv");
            return matcher.MatchSentence(Sentence);
        }
    }
}
