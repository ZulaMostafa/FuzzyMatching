// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FuzzyMatching.Definitions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuzzyMatching.Definitions.Services
{
    public interface IStorageService
    {
        public Task<Object> LoadPreprocessedDatasetAsync(string datasetName, string Location);
        public Task<Object> LoadDatasetAsync(string datasetName, string Location);
        public Task StorePreprocessedDatasetAsync(PreprocessedDataset preprocessedDataset, string datasetName , string Location);
        public Task StoreDatasetAsync(List<string> Dataset, string datasetName, string Location);
        public Task<string[]> ListPreprocessedDatasetsAsync(string Location);
    }
}
