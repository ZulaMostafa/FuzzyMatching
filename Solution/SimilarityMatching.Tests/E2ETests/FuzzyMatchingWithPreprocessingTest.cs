using Xunit;
using System;


namespace FuzzyMatching.Tests.E2ETests
{
    public class FuzzyMatchingWithPreprocessingTest
    {
        [Fact]
        public void TestAlgorithm()
        {
            // get test data
            string sentence = "take record";
           /* var sentenceDataset = new List<string>
            {
                "go out",
                "drink water",
                "do laundry",
                "play games",
                "blah blah blah"
            };*/

            // calculate similarity (no preprocessing)
            var result = RunTime.RunTime.GetClosestSentence(sentence, 25000);
            var closestMatch = result.Item1;
            Console.WriteLine(closestMatch);
            var index = result.Item2;
            Console.WriteLine(index);
            var score = result.Item3;
            Console.WriteLine(score);
        }
    }
}
