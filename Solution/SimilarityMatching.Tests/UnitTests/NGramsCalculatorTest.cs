﻿using SimilarityMatching.Core.FeatureMatrixCalculation;
using System;
using Xunit;

namespace SimilarityMatching.Tests.UnitTests
{
    public class NGramsCalculatorTest
    {
        public static TheoryData TestNGramsAlgorithmData()
        {
            return new TheoryData<string>
            {
                "department",
                "mohamed"
            };
        }

        [Theory]
        [MemberData(nameof(TestNGramsAlgorithmData))]
        public void TestNGramsAlgorithm(string sentence)
        {
            var result = NGramsCalculator.GetSentenceNGrams(sentence);
            Console.WriteLine(result);
        }
    }
}
