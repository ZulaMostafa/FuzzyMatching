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
            // get test data
            //string sentence = "take record";
            var storageOptions = new StorageOptions();
            storageOptions.StorageType = Definitions.Models.Enums.StorageType.Local;
            storageOptions.BaseDirectory = @"C:\Users\karim\Documents\GitHub\FuzzyMatching";
            var fuzzyMatcher = new FuzzyMatchingClient(storageOptions);

            var readerUtterance = new StreamReader(File.OpenRead(@"C:\Users\karim\Documents\GitHub\FuzzyMatching\largeDataset.csv"));
            readerUtterance.ReadLine();

            Console.WriteLine("Dataset Loaded !!");

            List<string> Dataset = new List<string>();

            for (int i = 0; i < 25000; i++)
            {
                var line = readerUtterance.ReadLine();
                var values = line.Split(',');
                Dataset.Add(values[1]);
            }
            string name = "mydataset";
            fuzzyMatcher.PreprocessAsync(name, "", Dataset);
            var result =fuzzyMatcher.MatchSentenceAsync("take record", "", name).GetAwaiter().GetResult();
            Console.WriteLine(result.ClosestSentence);
            Debug.Assert(result.ClosestSentence == "kkfj", "dsjfaaf");
            /* var sentenceDataset = new List<string>
             {
                 "go out",
                 "drink water",
                 "do laundry",
                 "play games",
                 "blah blah blah"
             };*/

            // calculate similarity (no preprocessing)
            //var result = RunTime.RunTime.GetClosestSentence(sentence, 25000);
            //var closestMatch = result.Item1;
            //// expected output: barca take record as robson celebrates birthday in
            //Console.WriteLine(closestMatch);
            //var index = result.Item2;
            //Console.WriteLine(index);
            //var score = result.Item3;
            //Console.WriteLine(score);
        }
    }
}
