using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;

namespace FuzzyMatching.Definitions.Services
{
    public interface IRuntimeClient
    {
        public FuzzyMatchingResult MatchSentence(string sentence, ProcessedDataset processedDataset, List<string> Dataset, int ngramsLength);
    }
}
