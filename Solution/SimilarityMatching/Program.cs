using SimilarityMatching.SimilarityMatchingAlgorithms;
using System.Collections.Generic;

namespace SimilarityMatching
{
    class Program
    {
        static void Main(string[] args)
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
            var result = SimilarityMatchingNoPreprocessing.GetClosestSentence(sentence, sentenceDataset);
            var closestMatch = result.Item1;
            var index = result.Item2;
            var score = result.Item3;
        }
    }
}
