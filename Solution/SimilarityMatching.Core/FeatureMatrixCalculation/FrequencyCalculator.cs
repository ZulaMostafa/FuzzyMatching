using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzztMatching.Core.FeatureMatrixCalculation
{
    public static class FrequencyCalculator
    {
        public static  Dictionary<string, int> GetNGramFrequencyAsync(string[] sentenceNGrams)
        {
            var result = new Dictionary<string, int>();
            foreach (var ngram in sentenceNGrams)
            {
                if (result.ContainsKey(ngram))
                {
                    result[ngram] += 1;
                }
                else
                {
                    result[ngram] = 1;
                }
            }
            return result;
            //return await Task.FromResult(result);
        }

        public static  Dictionary<string, int>[] GetNGramFrequencyBatchAsync(string[][] sentenceDatasetNGrams)
        {
            var tasks = sentenceDatasetNGrams.AsParallel().Select(  sentenceNGrams =>  GetNGramFrequencyAsync(sentenceNGrams)).ToArray();
            return tasks;
            //return await Task.WhenAll(tasks);
        }

        public static async Task<Dictionary<string, float>> GetOverallNGramFrequencyAsync(string[][] sentenceListNGrams)
        {
            var result = new Dictionary<string, float>();

            var tasks = sentenceListNGrams.Select( async sentenceNGrams =>
           {
               var task = sentenceNGrams.Select(async ngram =>
              {
                  if (result.ContainsKey(ngram))
                  {
                      result[ngram] += 1;
                  }
                  else
                  {
                      result[ngram] = 1;
                  }
              });
               await Task.WhenAll(task);
           });
            await Task.WhenAll(tasks);

            return await Task.FromResult(result);
            
            /*foreach (var sentenceNGrams in sentenceListNGrams)
            {
                foreach (var ngram in sentenceNGrams)
                {
                    if (result.ContainsKey(ngram))
                    {
                        result[ngram] += 1;
                    }
                    else
                    {
                        result[ngram] = 1;
                    }
                }
            }
            return result;*/
        }
    }
}
