﻿using FuzzyMatching.Core;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Models.Enums;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using Xunit;


namespace FuzzyMatching.Tests.E2ETests
{
    public class FuzzyMatchingClientTests
    {
        public static TheoryData FuzzyMatchingClientTestData()
        {
            // prepare input
            var datasetLocation = @"C:\Users\karim\Documents\GitHub\FuzzyMatching\Solution\SimilarityMatching.Tests\TestData\largeDataset.csv";
            var dataset = ReadDatasetFromCSV(datasetLocation);
            var randomSentenceIndex = 5;
            var sentenceToMatch = dataset[randomSentenceIndex];
            var storageOptions = new StorageOptions
            {
                StorageType = StorageType.Blob,
                BaseDirectory = @".",
                ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fuzzytest12;AccountKey=p3h+kwNL/2V5Hx7yn73NxX6b0Nkx9elcu6CoR65Hojf3qYO6Iq23Vd9GjTkWLLNieYMKJ7alWYKpLL+6o28Z6Q==;EndpointSuffix=core.windows.net",
                ContainerName = "container"
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

            // print result

            Console.WriteLine("sentence to match : {0}", sentenceToMatch);
            Console.WriteLine("Matched Sentence : {0}", result.ClosestSentence);
            Console.WriteLine("Matched Sentence Score : {0}", result.MatchingScore);
            Console.WriteLine("Matched Sentence Index : {0}", result.MatchingIndex);
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