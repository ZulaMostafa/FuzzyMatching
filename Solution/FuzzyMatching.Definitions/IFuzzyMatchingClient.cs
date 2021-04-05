using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuzzyMatching.Definitions
{
    public interface IFuzzyMatchingClient
    {
        public Task<bool> PreprocessAsync(string datasetName, List<string> dataset);
        public FuzzyMatchingResult MatchSentence(string sentence, string datasetName);
        public List<string> ListPreProcessedDatasets();
    }
}
