using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using Microsoft.CogSLanguageUtilities.Core.Services.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuzzyMatching.Core.Factories
{
    public static class StorageFactory
    {
        public static IStorageService  create ( StorageOptions storageOptions)
        {

            if (storageOptions.StorageType == 0)
            {
                return new LocalStorageService(storageOptions.BaseDirectory);
            }
            else
            {
                return new BlobStorageService(storageOptions.ConnectionString,storageOptions.ContainerName);
            }
        }
    }
}
