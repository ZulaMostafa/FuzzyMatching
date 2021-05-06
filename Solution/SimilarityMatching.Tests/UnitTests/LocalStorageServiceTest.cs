using FuzzyMatching.Core.Factories;
using FuzzyMatching.Definitions.Models;
using FuzzyMatching.Definitions.Models.Enums;
using FuzzyMatching.Definitions.Services;
using Xunit;

namespace FuzzyMatching.Tests.UnitTests
{
    public class LocalStorageServiceTest
    {
        public static TheoryData TestLocalStorageServiceArrays()
        {
            int[] array1D = new int[5] { 99, 98, 92, 97, 95 };
            var storageOptions = new StorageOptions();
            // local storage
            storageOptions.StorageType = StorageType.Local;
            storageOptions.BaseDirectory = @".";
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
        [MemberData(nameof(TestLocalStorageServiceArrays))]
        public void TestLocalStorageService(int[] array1D, IStorageService StorageService, int length)
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
