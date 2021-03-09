using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace FuzztMatching.Core.FeatureMatrixCalculation
{
    public static class NGramsCalculator
    {
        /// <summary>
        /// Given a sentence, returns ngrams of that sentence
        /// example:
        ///     sentence = "department", ngramsLength = 3
        ///     result = ['dep', 'par', 'art', 'rtm', 'tme', 'men', 'ent']
        /// </summary>
        /// <param name="sentence"></param>
        /// <param name="ngramsLength"></param>
        /// <returns></returns>
        public static async Task<string[]> GetSentenceNGramsAsync(string sentence, int ngramsLength = 3)
        {
            var result = new List<string>();
            for (var i = 0; i < sentence.Length - ngramsLength + 1; i++)
            {
                result.Add(sentence.Substring(i, ngramsLength));
            }
            return await Task.FromResult(result.ToArray());
        }

        public static async Task<string[][]> GetSentenceNGramsBatchAsync(List<string> sentenceList, int ngramsLength = 3)
        {
            /*var task = from sentence in sentenceList.AsParallel()
                       select sentence;
             task.ForAll( sentence =>  GetSentenceNGrams(sentence, ngramsLength));*/
        
            var tasks =sentenceList.Select( async sentence =>  await GetSentenceNGramsAsync(sentence, ngramsLength)).ToArray();
            
           
            return await Task.WhenAll(tasks);
        }

        public static HashSet<string> GetAllUniqueNGrams(List<List<string>> senteceListNgrams)
        {
            return null;
        }
    }
}
