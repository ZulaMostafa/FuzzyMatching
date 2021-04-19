using FuzzyMatching.Algorithms;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Models.Enums;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using Xunit;


namespace FuzzyMatching.Tests.E2ETests
{
    public class FuzzyMatchingClientTests
    {
        public static TheoryData FuzzyMatchingClientTestData()
        {
            // prepare input
            var datasetLocation = @"C:\Users\v-moshaban\Desktop\FuzzyMatchingDotNet\Solution\SimilarityMatching.Tests\TestData\largeDataset.csv";
            var dataset = ReadDatasetFromCSV(datasetLocation);
            var randomSentenceIndex = 5;
            var sentenceToMatch = dataset[randomSentenceIndex];
            var storageOptions = new StorageOptions
            {
                StorageType = StorageType.Local,
                BaseDirectory = @"C:\Users\v-moshaban\Desktop\FuzzBuzz\word_dir",
                ConnectionString = "",
                ContainerName = ""
            };

            // expected
            var expected = new MatchingResult
            {
                ClosestSentence = dataset[randomSentenceIndex],
                MatchingIndex = randomSentenceIndex
            };


            return new TheoryData<List<string>, string, StorageOptions, MatchingResult>
            {
                {
                    dataset,
                    sentenceToMatch,
                    storageOptions,
                    expected
                }
            };
        }

        [Theory]
        [MemberData(nameof(FuzzyMatchingClientTestData))]
        public void FuzzyMatchingClientTestAsync(List<string> dataset, string sentenceToMatch, StorageOptions storageOptions, MatchingResult expected)
        {
            // create client
            var fuzzyMatchingClient = new FuzzyMatchingClient(storageOptions);

            // process dataset
            var datasetName = "someDataset";
            fuzzyMatchingClient.PreprocessDataset(dataset, datasetName);

            // runtime
            var result = fuzzyMatchingClient.MatchSentence(sentenceToMatch, datasetName);

            // assert
            Assert.Equal(result.ClosestSentence, expected.ClosestSentence);
            Assert.Equal(result.MatchingIndex, expected.MatchingIndex);
        }

        private static List<string> ReadDatasetFromCSV(string filePath, int limit = 30000)
        {
            // init result
            var result = new List<string>();

            // init parser
            var parser = new TextFieldParser(filePath);
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(new string[] { "," });
            parser.ReadFields(); // skip first line -> headers

            // read data
            var counter = 1;
            while (!parser.EndOfData && counter <= limit)
            {
                var row = parser.ReadFields(); // string[]
                result.Add(row[1]);
                counter++;
            }

            return result;
        }

    }
}