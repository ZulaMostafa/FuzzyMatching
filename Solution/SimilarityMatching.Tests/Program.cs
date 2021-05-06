using System;
using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;
using System.IO;
using FuzzyMatching.Core.Factories;
using FuzzyMatching.Core;

namespace SimilarityMatching
{
    class Program
    {

        public static void Main(string[] args)
        {
            var storageOptions = new StorageOptions();
            // local storage
            storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Local;
            storageOptions.BaseDirectory = @"C:\Users\karim\Documents\GitHub\FuzzyMatching";

            //storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Blob;
            //storageOptions.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fuzzytest12;AccountKey=p3h+kwNL/2V5Hx7yn73NxX6b0Nkx9elcu6CoR65Hojf3qYO6Iq23Vd9GjTkWLLNieYMKJ7alWYKpLL+6o28Z6Q==;EndpointSuffix=core.windows.net";
            //storageOptions.ContainerName = "container";
            var fuzzyMatcher = new FuzzyMatchingClient(storageOptions);

            int[] sizes = new int[6] { 10, 100, 1000, 10000, 25000, 50000 };


            List<string> rawData = new List<string>();


            foreach (var size in sizes)
            {
                var readerUtterance = new StreamReader(File.OpenRead(@"C:\Users\karim\Documents\GitHub\FuzzyMatching\Solution\SimilarityMatching.Tests\TestData\largeDataset.csv"));
                readerUtterance.ReadLine();
                for (int i = 0; i < size; i++)
                {
                    var line = readerUtterance.ReadLine();
                    var values = line.Split(',');
                    rawData.Add(values[1]);
                }
              
                string name = "mydataset";
                fuzzyMatcher.PreprocessDataset(rawData, name);
                DateTime start = DateTime.Now;
                var result = fuzzyMatcher.MatchSentence("take record",name);
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                Console.WriteLine("Elapsed Time for the program with size {0} is {1} s", size, ts.TotalSeconds);
                Console.WriteLine(result.ClosestSentence);
                rawData.Clear();
            }
        }
        public static T[] Make1DArray<T>(T[][] input)
        {
            //
            int rows = input.Length;
            int columns = input[0].Length;
            T[] result = new T[rows * columns];

            // Step 2: copy 2D array elements into a 1D array.
            int write = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int z = 0; z < columns; z++)
                {
                    result[write++] = input[i][z];
                }
            }
            // Step 3: return the new array.
            return result;
        }
    }
}




