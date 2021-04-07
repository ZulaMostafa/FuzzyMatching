using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;

namespace FuzzyMatching.Definitions.Services
{
    public interface IRuntimeClient
    {
        public FuzzyMatchingResult MatchSentence(string sentence, PreprocessedDataset preprocessedDataset, List<string> Dataset);
    }
}
