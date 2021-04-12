using FuzzyMatching.Core.Factories;
using FuzzyMatching.Definitions;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
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
        



        
        public  async Task<bool> PreprocessAsync(string datasetName, string location, List<string> dataset)
        {
            var preprocessor = new Core.Preprocessor.PreprocessorClient();
           
            // create feature matrix
            var matrices=   preprocessor.CreateFeatureMatrix(dataset);
            // store feature matrix
           
            await StorageService.StoreObjectAsync(matrices, datasetName + "_PreProcessed", location);
            await StorageService.StoreObjectAsync(dataset, datasetName + "_Dataset", location);
 
            return await  Task.FromResult(true);

        }

     

        public async Task<FuzzyMatchingResult> MatchSentenceAsync(string sentence, string location, string datasetName)
        {
            var runTime = new Core.RunTime.RuntimeClient();
            PreprocessedDataset matrices;
            try
            {
                 matrices = (PreprocessedDataset)StorageService.LoadObjectAsync(datasetName + "_PreProcessed", location).GetAwaiter().GetResult();
            }
            catch (FileNotFoundException)
            {
                try
                {
                    var data = (List<string>)StorageService.LoadObjectAsync(datasetName + "_Dataset", location).GetAwaiter().GetResult();
                    await PreprocessAsync(datasetName, location, data);
                    matrices = (PreprocessedDataset)StorageService.LoadObjectAsync(datasetName + "_PreProcessed", location).GetAwaiter().GetResult();
                    return runTime.MatchSentence(sentence, matrices, data);
                }
                catch(FileNotFoundException)
                {
                    throw new FileNotFoundException();
                }
            }
            var dataset = (List<string>)StorageService.LoadObjectAsync(datasetName + "_Dataset", location).GetAwaiter().GetResult();
            // return
            return runTime.MatchSentence(sentence,matrices , dataset);
        }

        public List<string> ListPreProcessedDatasets(string directory)
        {
            return  StorageService.ListPreprocessedDatasetsAsync(directory).GetAwaiter().GetResult().ToList();
        }

      
    }
}
