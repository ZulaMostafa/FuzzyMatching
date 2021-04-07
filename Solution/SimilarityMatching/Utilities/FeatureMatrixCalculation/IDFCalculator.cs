using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyMatching.Core.FeatureMatrixCalculation
{
    public static class IDFCalculator
    {
        /// <summary>
        /// calculates inverse frequence of word within all  documents 
        /// example:
        ///     for a total of 10,000,000 sentences with the ngram "dog" found 100 times
        ///     IDF = log( 10,000,000 / 100 ) = 4
        /// </summary>
        /// <param name="sentenceNgrams"></param>
        /// <param name="datasetNgramsFrequencies"></param>
        /// <returns></returns>
        public static float[] CalculateIDFVector(string[] allDataUniqueNGramsVector, Dictionary<string, int> overallDataNgramsFrequencies, int overallDataLength)
        {
            var result = new float[allDataUniqueNGramsVector.Length];


            result = allDataUniqueNGramsVector.AsParallel().Select(ngram => (float)Math.Log((float)overallDataLength / (float)overallDataNgramsFrequencies[ngram])).ToArray();

            // normal approach
            /*for (var i = 0; i < result.Length; i++)
            {
                var ngram = allDataUniqueNGramsVector[i];
               if (overallDataNgramsFrequencies.ContainsKey(ngram))
               {
                   var ngramOverallFrequency = overallDataNgramsFrequencies[ngram];
                   result[i] = (float)Math.Log((float)overallDataLength / (float)ngramOverallFrequency);
               }

            }*/
            return result;
        }
    }
}
