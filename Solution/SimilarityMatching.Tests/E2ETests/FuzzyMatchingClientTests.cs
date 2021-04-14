using FuzzyMatching.Algorithms;
using FuzzyMatching.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xunit;


namespace FuzzyMatching.Tests.E2ETests
{
    public class FuzzyMatchingClientTests
    {
        [Fact]
        public void TestAlgorithm()
        {
            var storageOptions = new StorageOptions();
            // local storage
            //storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Local;
            //storageOptions.BaseDirectory = @"C:\Users\karim\Documents\GitHub\FuzzyMatching";

            storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Blob;
            storageOptions.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fuzzytest12;AccountKey=p3h+kwNL/2V5Hx7yn73NxX6b0Nkx9elcu6CoR65Hojf3qYO6Iq23Vd9GjTkWLLNieYMKJ7alWYKpLL+6o28Z6Q==;EndpointSuffix=core.windows.net";
            storageOptions.ContainerName = "container";
            var fuzzyMatcher = new FuzzyMatchingClient(storageOptions);

            int[] sizes = new int[6] { 10, 100, 1000, 10000, 25000, 50000 };


            List<string> rawData = new List<string>();


            foreach (var size in sizes)
            {
                var readerUtterance = new StreamReader(File.OpenRead(@"C:\Users\karim\Documents\GitHub\FuzzyMatching\largeDataset.csv"));
                readerUtterance.ReadLine();
                for (int i = 0; i < size; i++)
                {
                    var line = readerUtterance.ReadLine();
                    var values = line.Split(',');
                    rawData.Add(values[1]);
                }


                Console.WriteLine("Dataset Loaded !!");
                string name = "mydataset";
                fuzzyMatcher.PreprocessAsync(name, "", rawData);
                DateTime start = DateTime.Now;
                var result = fuzzyMatcher.MatchSentenceAsync("take record", "", name).GetAwaiter().GetResult();
                DateTime end = DateTime.Now;
                TimeSpan ts = (end - start);
                Console.WriteLine("Elapsed Time for the program with size {0} is {1} s", size, ts.TotalSeconds);
                Console.WriteLine(result.ClosestSentence);
                rawData.Clear();
            }
    }

