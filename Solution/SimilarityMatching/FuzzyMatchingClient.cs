using FuzzyMatching.Core.Factories;
using FuzzyMatching.Core.Services;
using FuzzyMatching.Core.Utilities.ModelConverters;
using FuzzyMatching.Definitions;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System.Collections.Generic;
using System.IO;

namespace FuzzyMatching.Core
{
    public class FuzzyMatchingClient : IFuzzyMatchingClient
    {
        private PreprocessorClient PreprocessorClient;
        private RuntimeClient RuntimeClient;
        private IStorageService StorageService;
        private float[] temp;

        public FuzzyMatchingClient(StorageOptions storageOptions)
        {
            StorageService = StorageFactory.create(storageOptions);
            PreprocessorClient = new PreprocessorClient();
            RuntimeClient = new RuntimeClient();
        }

        public void PreprocessDataset(List<string> dataset, string datasetName, string relativeDirectory = "")
        {
            // create feature matrix
            var processedDataset = PreprocessorClient.PreprocessDataset(dataset);
            temp = processedDataset.IDFVector;
            var storedDataset = ProcessedDatasetModelConverter.ProcessedToStored(processedDataset);
            // store preprocessed data
            StorageService.StoreBinaryObject(storedDataset, datasetName + "_PreProcessed", relativeDirectory);
            StorageService.StoreBinaryObject(dataset, datasetName + "_Dataset", relativeDirectory);
        }



        public MatchingResult MatchSentence(string sentence, string datasetName, string relativeDirectory = "")
        {
            try
            {
                // try to get the preprocessed dataset
                var storedDataset = StorageService.LoadBinaryObject<StoredProcessedDataset>(datasetName + "_PreProcessed", relativeDirectory);
                var dataset = StorageService.LoadBinaryObject<List<string>>(datasetName + "_Dataset", relativeDirectory);
                var processedDataset = ProcessedDatasetModelConverter.StoredToProcessed(storedDataset);
                // run matching algorithm
                return RuntimeClient.MatchSentence(sentence, processedDataset, dataset);
            }
            catch (FileNotFoundException)
            {
                try
                {
                    // load original dataset
                    var dataset = StorageService.LoadBinaryObject<List<string>>(datasetName + "_Dataset", relativeDirectory);
                    // run preprocessing
                    PreprocessDataset(dataset, datasetName, relativeDirectory);
                    // load preprocessed
                    var preprocessedDataset = StorageService.LoadBinaryObject<ProcessedDataset>(datasetName + "_PreProcessed", relativeDirectory);
                    // run matching algorithm
                    return RuntimeClient.MatchSentence(sentence, preprocessedDataset, dataset);
                }
                catch (FileNotFoundException)
                {
                    // this means original dataset wasn't found!
                    throw new FileNotFoundException();
                }
            }
        }

        public string[] ListProcessedDatasets(string directory)
        {
            return StorageService.ListPreprocessedDatasets(directory);
        }
    }
}
