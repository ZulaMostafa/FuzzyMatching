using FuzzyMatching.Definitions.Models;
using System.Collections.Generic;

namespace FuzzyMatching.Definitions.Services
{
    public interface IPreprocessorClient
    {
        public PreprocessedDataset CreateFeatureMatrix(List<string> dataset);
    }
}
