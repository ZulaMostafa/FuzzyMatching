using FuzztMatching.Core.FeatureMatrixCalculation;
using FuzztMatching.Core.MatrixOperations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FuzzyMatching.Algorithms
{
    public class FuzzyMatching
    {
        private double[][] PreprocessedMatrix;
        public FuzzyMatching()
        {
            // do the preprocessing phase
            PreprocessedMatrix = CreateFeatureMatrix();
        }

        private void CreateFeatureMatrix()
        {
            // load data set
            // clean
            // create feature matrix
            return Preprocessor.Preprocessor.CreateFeatureMatrix();
        }

        public (string, float, int) MatchSentence(string sentence)
        {
            // match string, score, index

        }
    }
}
