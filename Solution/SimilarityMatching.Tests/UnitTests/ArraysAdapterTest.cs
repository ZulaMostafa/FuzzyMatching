using FuzzyMatching.Core.Adapters;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FuzzyMatching.Tests.UnitTests
{
    public class ArraysAdapterTest
    {
        public static TheoryData TestLocalStorageServiceArrays()
        {

            float[][] array2D =
{
new float[] { 0, 1, 2,3 },
new float[] { 0, 4, 5 ,0},
new float[] { 0, 0,6,0 }
};

          


            return new TheoryData<float[][],int,int>
            {
                {
                    array2D,
                    3,
                    4
                }
            };
        }

        [Theory]
        [MemberData(nameof(TestLocalStorageServiceArrays))]
        public void TestLocalStorageService(float[][] array2D,int height,int width)
        {
            var spreadArray = ArraysAdapter.Make1DArray<float>(array2D);
            var unrolledMatrix = ArraysAdapter.Make2DArray<float>(spreadArray,height,width);
            for (int i =0; i < height;i++)
            {
                for (int j = 0; j<width;j++)
                {
                    Assert.Equal(array2D[i][j], unrolledMatrix[i][j]);
                }
            }
           
        }
    }
}
