using FuzzyMatching.Core.Factories;
using FuzzyMatching.Definitions;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Models.Enums;
using FuzzyMatching.Definitions.Services;
using FuzzyMatching.FeatureMatrixCalculation;
using FuzzyMatching.MatrixOperations;
using FuzzyMatching.ReadWriteOperations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyMatching.Algorithms
{
    public class FuzzyMatchingClient : IFuzzyMatchingClient
    {
        private IStorageService StorageService;
      
        public FuzzyMatchingClient( StorageOptions storageOptions)
        {
            StorageService = StorageFactory.create(storageOptions);
        }

       
       

        private void loadDataset(int size, string path)
        {

        
        }
        



        
        public  async Task<bool> PreprocessAsync(string DatasetName, string Location, List<string> dataset)
        {
            var preprocessor = new Preprocessor.PreprocessorClient();
           
            // create feature matrix
            var matrices=   preprocessor.CreateFeatureMatrix(dataset);
            // store feature matrix
            await StorageService.StorePreprocessedDatasetAsync(matrices, DatasetName+"_PreProcessed", Location);
            await StorageService.StoreDatasetAsync(dataset, DatasetName + "_Dataset", Location);


            return await  Task.FromResult(true);

        }

     

        public   FuzzyMatchingResult MatchSentence(string Sentence, string Location, string DatasetName)
        {
            var runTime = new RunTime.RuntimeClient();

            var matrices = (PreprocessedDataset)StorageService.LoadPreprocessedDatasetAsync(DatasetName + "_PreProcessed", Location).GetAwaiter().GetResult();
            
            var dataset = (List<string>)StorageService.LoadDatasetAsync(DatasetName + "_Dataset", Location).GetAwaiter().GetResult();
            // return
            return runTime.MatchSentence(Sentence, matrices, dataset);
        }

        public List<string> ListPreProcessedDatasets(string directory)
        {
            return  StorageService.ListPreprocessedDatasetsAsync(directory).GetAwaiter().GetResult().ToList();
        }
    }
}
