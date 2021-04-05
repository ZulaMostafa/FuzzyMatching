// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FuzzyMatching.Definitions.Models;
using System.Threading.Tasks;

namespace FuzzyMatching.Definitions.Services
{
    public interface IStorageService
    {
        public Task StorePreprocessedDatasetAsync(PreprocessedDataset preprocessedDataset, string datasetName);
        public Task<PreprocessedDataset> LoadPreprocessedDatasetAsync(string datasetName);
        public Task<string[]> ListPreprocessedDatasetsAsync();
    }
}
