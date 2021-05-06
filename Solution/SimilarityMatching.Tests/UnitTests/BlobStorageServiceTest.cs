using FuzzyMatching.Core.Factories;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FuzzyMatching.Tests.UnitTests
{
    public class BlobStorageServiceTest
    {
        public static TheoryData TestBlobStorageServiceArrays()
        {
            int[] array1D = new int[5] { 99, 98, 92, 97, 95 };
            var storageOptions = new StorageOptions();
            // Blob storage

            storageOptions.StorageType = FuzzyMatching.Definitions.Models.Enums.StorageType.Blob;
            storageOptions.ConnectionString = "DefaultEndpointsProtocol=https;AccountName=fuzzytest12;AccountKey=p3h+kwNL/2V5Hx7yn73NxX6b0Nkx9elcu6CoR65Hojf3qYO6Iq23Vd9GjTkWLLNieYMKJ7alWYKpLL+6o28Z6Q==;EndpointSuffix=core.windows.net";
            storageOptions.ContainerName = "container";
            IStorageService StorageService = StorageFactory.create(storageOptions);


            return new TheoryData<int[], IStorageService, int>
            {
                {
                    array1D,
                    StorageService,
                    array1D.Length
                }
            };
        }

        [Theory]
        [MemberData(nameof(TestBlobStorageServiceArrays))]
        public void TestBlobStorageService(int[] array1D, IStorageService StorageService, int length)
        {
            StorageService.StoreBinaryObjectAsync(array1D, "1D_array", "");
            var LoadedArray = StorageService.LoadBinaryObjectAsync<int[]>("1D_array", "").GetAwaiter().GetResult();
            for (int i = 0; i < length; i++)
            {
                Assert.Equal(array1D[i], LoadedArray[i]);
            }
        }
    }
}
