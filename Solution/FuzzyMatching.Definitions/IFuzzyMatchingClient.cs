using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuzzyMatching.Definitions
{
    public interface IFuzzyMatchingClient
    {
        public Task<bool> PreprocessAsync(string datasetName,string Location, List<string> dataset);
        public Task<FuzzyMatchingResult> MatchSentenceAsync(string sentence,string Location, string datasetName);
        public List<string> ListPreProcessedDatasets(string directory);
    }
}
