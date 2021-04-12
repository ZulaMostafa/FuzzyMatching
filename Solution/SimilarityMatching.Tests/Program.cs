using System;
using FuzzyMatching.Algorithms;
using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;
using System.IO;

namespace SimilarityMatching
{
    class Program
    {

        public static void Main(string[] args)
        {
            var storageOptions = new StorageOptions();
            storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Local;
            storageOptions.BaseDirectory = @"C:\Users\karim\Documents\GitHub\FuzzyMatching";
            var fuzzyMatcher = new FuzzyMatchingClient(storageOptions);

            var readerUtterance = new StreamReader(File.OpenRead(@"C:\Users\karim\Documents\GitHub\FuzzyMatching\largeDataset.csv"));
            readerUtterance.ReadLine();

            List<string> Dataset = new List<string>();

            for (int i = 0; i < 25000; i++)
            {
                var line = readerUtterance.ReadLine();
                var values = line.Split(',');
                Dataset.Add(values[1]);
            }

            Console.WriteLine("Dataset Loaded !!");
            string name = "mydataset";
           // fuzzyMatcher.PreprocessAsync(name, "", Dataset);
            var result = fuzzyMatcher.MatchSentenceAsync("take record", "", name).GetAwaiter().GetResult();
            Console.WriteLine(result.ClosestSentence);

        }
    }
}

