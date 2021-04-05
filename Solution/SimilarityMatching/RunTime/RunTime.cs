using System;
using System.Collections.Generic;
using System.IO;

namespace FuzzyMatching.RunTime
{
    public static class RunTime
    {
        static void Main(string[] args)
        {

            int[] sizes = new int[7] { 10, 100, 1000, 10000, 25000, 50000, 100000 };
            var readerUtterance = new StreamReader(File.OpenRead(@"C:\Users\v-kelhammady\OneDrive - Microsoft\Documents\GitHub\FuzzyMatching\largeDataset.csv"));
            readerUtterance.ReadLine();

            Console.WriteLine("Dataset Loaded !!");

            List<string> utteranceList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                var line = readerUtterance.ReadLine();
                var values = line.Split(',');
                utteranceList.Add(values[1]);
            }

            //foreach (var utterance in utteranceList)
            //{
            var utterance = "take record";
            var size = 25000;
            Console.WriteLine("results for sentence: {0}", utterance);
            // foreach (var size in sizes)
            {
                Console.WriteLine("Hello World!");
                //var matcher = new Algorithms.FuzzyMatching(size, @"C:\Users\v-kelhammady\OneDrive - Microsoft\Documents\GitHub\FuzzyMatching\LargeDataset.csv");
                DateTime start = DateTime.Now;
                var result = GetClosestSentence(utterance, 25000);
                // var result = matcher.MatchSentence(utterance);
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                Console.WriteLine("Elapsed Time for the program with size {0} is {1} s", size, ts.TotalSeconds);
                var closestMatch = result.Item1;
                Console.WriteLine(closestMatch);
                var index = result.Item2;
                Console.WriteLine(index);
                var score = result.Item3;
                Console.WriteLine(score);
                //}
                Console.WriteLine("---------------------------");

            }
        }
        public static (string, float, int) GetClosestSentence(string Sentence, int size)

        {
            var matcher = new Algorithms.FuzzyMatching(size, @"C:\Users\v-kelhammady\OneDrive - Microsoft\Documents\GitHub\FuzzyMatching\LargeDataset.csv");
            return matcher.MatchSentence(Sentence);
        }
    }
}
