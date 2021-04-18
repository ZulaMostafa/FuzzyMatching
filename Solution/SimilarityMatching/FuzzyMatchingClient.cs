using FuzzyMatching.Core.Factories;
using FuzzyMatching.Core.Preprocessor;
using FuzzyMatching.Core.RunTime;
using FuzzyMatching.Definitions;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System.Collections.Generic;
using System.IO;

namespace FuzzyMatching.Algorithms
{
    public class FuzzyMatchingClient : IFuzzyMatchingClient
    {
        private PreprocessorClient PreprocessorClient;
        private RuntimeClient RuntimeClient;
        private IStorageService StorageService;

        public FuzzyMatchingClient(StorageOptions storageOptions)
        {
            StorageService = StorageFactory.create(storageOptions);
            PreprocessorClient = new PreprocessorClient();
            RuntimeClient = new RuntimeClient();
        }

        public void PreprocessDataset(List<string> dataset, string datasetName, string relativeDirectory = "")
        {
            // create feature matrix
            var preprocessedDataset = PreprocessorClient.PreprocessDataset(dataset);

            // store preprocessed data
            StorageService.StoreBinaryObject(preprocessedDataset, datasetName + "_PreProcessed", relativeDirectory);
            StorageService.StoreBinaryObject(dataset, datasetName + "_Dataset", relativeDirectory);
        }

        public FuzzyMatchingResult MatchSentence(string sentence, string datasetName, string relativeDirectory = "")
        {
            try
            {
                // try to get the preprocessed dataset
                var preprocessedDataset = StorageService.LoadBinaryObject<ProcessedDataset>(datasetName + "_PreProcessed", relativeDirectory);
                var dataset = StorageService.LoadBinaryObject<List<string>>(datasetName + "_Dataset", relativeDirectory);
                // run matching algorithm
                return RuntimeClient.MatchSentence(sentence, preprocessedDataset, dataset);
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

        public string[] ListPreProcessedDatasets(string directory)
        {
            return StorageService.ListPreprocessedDatasets(directory);
        }
    }
}
