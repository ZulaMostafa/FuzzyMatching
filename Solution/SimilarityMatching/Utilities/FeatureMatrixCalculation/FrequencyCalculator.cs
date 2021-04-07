using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyMatching.Core.FeatureMatrixCalculation
{
    public static class FrequencyCalculator
    {


        public static Dictionary<string, int> GetNGramFrequencyAsync(string[] sentenceNGrams)
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

        }

        public static Dictionary<string, int>[] GetNGramFrequencyBatchAsync(string[][] sentenceDatasetNGrams)
        {
            return sentenceDatasetNGrams.AsParallel().Select(sentenceNGrams => GetNGramFrequencyAsync(sentenceNGrams)).ToArray();


        }


        public static async Task<Dictionary<string, int>> GetOverallNGramFrequencyAsync(string[][] sentenceListNGrams)
        {

            var result = new Dictionary<string, int>();
            /* sentenceListNGrams.AsParallel().ForAll( sentenceNGrams =>
             {
                 foreach(var ngram in sentenceNGrams)
                  {
                      if (result.ContainsKey(ngram))
                      {
                         lock (result)
                         {
                             result[ngram] += 1;
                         }
                      }
                      else
                      {
                         lock (result)
                         {
                             result[ngram] = 1;
                         }
                      }
                  }
             });

             return result;*/
            var tasks = sentenceListNGrams.Select(async sentenceNGrams =>
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

            /* foreach (var sentenceNGrams in sentenceListNGrams)
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
