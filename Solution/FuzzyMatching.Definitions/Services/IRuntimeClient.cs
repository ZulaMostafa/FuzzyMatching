using FuzzyMatching.Definitions.Models;

namespace FuzzyMatching.Definitions.Services
{
    public interface IRuntimeClient
    {
        public FuzzyMatchingResult MatchSentence(string sentence, PreprocessedDataset preprocessedDataset);
    }
}
