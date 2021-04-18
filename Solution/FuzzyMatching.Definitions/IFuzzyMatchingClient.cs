using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;

namespace FuzzyMatching.Definitions
{
    public interface IFuzzyMatchingClient
    {
        public void PreprocessDataset(List<string> dataset, string datasetName, string relativeDirectory);
        public FuzzyMatchingResult MatchSentence(string sentence, string datasetName, string relativeDirectory);
        public string[] ListPreProcessedDatasets(string relativeDirectory);
    }
}
