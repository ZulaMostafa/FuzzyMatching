﻿using FuzzyMatching.FeatureMatrixCalculation;
using System;
using Xunit;

namespace FuzzyMatching.Tests.UnitTests
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
            var result = NGramsCalculator.GetSentenceNGramsAsync(sentence);
            Console.WriteLine(result);
        }
    }
}
