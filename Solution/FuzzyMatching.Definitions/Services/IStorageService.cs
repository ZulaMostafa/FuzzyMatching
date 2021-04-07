// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FuzzyMatching.Definitions.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FuzzyMatching.Definitions.Services
{
    public interface IStorageService
    {
    
        public Task<Object> LoadObjectAsync(string name, string Location);
        public  Task StoreObjectAsync(Object data, string fileName, string location);
        public Task<string[]> ListPreprocessedDatasetsAsync(string Location);
    }
}
