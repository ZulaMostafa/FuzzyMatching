using FuzzyMatching.Algorithms;
using System.Collections.Generic;
using Xunit;

namespace FuzztMatching.Tests.E2ETests
{
    public class FuzzyMatchingNoPreprocessingTest
    {
        [Fact]
        public void TestAlgorithm()
        {
            // get test data
            string sentence = "lets play some games";
            var sentenceDataset = new List<string>
            {
                "go out",
                "drink water",
                "do laundry",
                "play games",
                "blah blah blah"
            };

            // calculate similarity (no preprocessing)
            var result = FuzzyMatching.Algorithms.FuzzyMatching.GetClosestSentence(sentence, sentenceDataset);
            var closestMatch = result.Item1;
            var index = result.Item2;
            var score = result.Item3;
        }
    }
}
