using FuzzyMatching.Core.Factories;
using FuzzyMatching.Definitions;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System.Collections.Generic;
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
            await StorageService.StoreObjectAsync(matrices, datasetName+"_PreProcessed", location);
            await StorageService.StoreObjectAsync(dataset, datasetName + "_Dataset", location);


            return await  Task.FromResult(true);

        }

     

        public   FuzzyMatchingResult MatchSentence(string sentence, string location, string datasetName)
        {
            var runTime = new Core.RunTime.RuntimeClient();

            var matrices = (PreprocessedDataset)StorageService.LoadObjectAsync(datasetName + "_PreProcessed", location).GetAwaiter().GetResult();
            var dataset = (List<string>)StorageService.LoadObjectAsync(datasetName + "_Dataset", location).GetAwaiter().GetResult();
            // return
            return runTime.MatchSentence(sentence, matrices, dataset);
        }

        public List<string> ListPreProcessedDatasets(string directory)
        {
            return  StorageService.ListPreprocessedDatasetsAsync(directory).GetAwaiter().GetResult().ToList();
        }
    }
}
