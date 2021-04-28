using FuzzyMatching.Core.Factories;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FuzzyMatching.Tests.UnitTests
{
    public class LocalStorageServiceTest
    {
        public static TheoryData TestLocalStorageServiceArrays()
        {
            int[] array1D = new int[5] { 99, 98, 92, 97, 95 };
            int[,] array2D = new int[,] { { 1, 2 }, { 3, 4 }, { 5, 6 }, { 7, 8 } };
            var storageOptions = new StorageOptions();
            // local storage
            storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Local;
            storageOptions.BaseDirectory = @"C:\Users\karim\Documents\GitHub\FuzzyMatching";
            IStorageService StorageService = StorageFactory.create(storageOptions);


            return new TheoryData<int []  , IStorageService,int>
            {
                {
                    array1D,
                    StorageService,
                    array1D.Length
                }
            };
        }

        [Theory]
        [MemberData(nameof(TestLocalStorageServiceArrays))]
        public void TestLocalStorageService (int [] array1D, IStorageService StorageService,int length)
        {
            StorageService.StoreBinaryObject(array1D, "1D_array", "");
            var LoadedArray = StorageService.LoadBinaryObject<int []>( "1D_array", "");
            for (int i = 0; i < length; i++)
            {
                Assert.Equal(array1D[i], LoadedArray[i]);
            }
        }
    }
}
