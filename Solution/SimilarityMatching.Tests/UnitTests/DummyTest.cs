using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FuzzyMatching.Tests.UnitTests
{
    public class DummyTest
    {
        public static TheoryData TestLocalStorageServiceArrays()
        {
           


            return new TheoryData<string,string>
            {
                {
                   "2",
                   "2"
                }
            };
        }

        [Theory]
        [MemberData(nameof(TestLocalStorageServiceArrays))]
        public void TestLocalStorageService(string x , string y)
        {
            Assert.Equal(x, y);
        }
    }
}

