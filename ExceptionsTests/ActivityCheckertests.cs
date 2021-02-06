using System;
using System.Collections.Generic;
using Exceptions;
using Xunit;

namespace ExceptionsTests
{
    public class ActivityCheckerTests
    {
        internal ActivityChecker _testClass = new ActivityChecker();
    }

    public class CheckActiveState : ActivityCheckerTests
    {
        [Fact]
        public void ReturnsTrue_WhenGuidEndsInEven()
        {
            // arrange
            var activeGuid = "961c2b7a-059f-40e3-b660-1244ad7f21a2";

            // act
            var result = _testClass.CheckActiveState(Guid.Parse(activeGuid));

            // assert
            Assert.True(result);
        }

        [Fact]
        public void RetrunsFalse_WhenGuidEndsInOdd()
        {
            // arrange
            var inactiveGuid = "f9046d1c-9193-46ff-84b3-a0959a17e6e1";

            // act
            var result = _testClass.CheckActiveState(Guid.Parse(inactiveGuid));

            // assert
            Assert.False(result);
        }

        [Fact]
        public void ThrowsException_WhenGuidInvalid()
        {
            // arrange
            var invalidGuid = "b8009e94-346f-4717-8e93-d3c0dd90d2fa";

            // act + assert
            var result = Assert.Throws<ActivityException>(() => _testClass.CheckActiveState(Guid.Parse(invalidGuid)));
            Assert.Equal("Guids must not end on letters", result.Message);

            //try
            //{
            //    _testClass.CheckActiveState(Guid.Parse(invalidGuid));
            //} catch (Exception e)
            //{
            //}
        }
    }
    
    public class HowManyActive : ActivityCheckerTests
    {
        string guid1 = "961c2b7a-059f-40e3-b660-1244ad7f21a2";
        string guid2 = "0ca4445f-e359-4525-8dd4-347f73709720";
        string guid3 = "b8009e94-346f-4717-8e93-d3c0dd90d2fa";
        string guid4 = "dfd71260-30dc-4094-b6f7-0ec29adc3384";
        string guid5 = "f9046d1c-9193-46ff-84b3-a0959a17e6e1";
        string guid6 = "db49637d-93f4-4ac4-9b10-e1c6c8681a6e";
        string guid7 = "46cf3d15-0fb3-43dc-9b8a-8dea33a8c078";
        string guid8 = "41e8157c-a218-485b-b0ed-727d6ebb847e";
        string guid9 = "7c569ef1-9444-43d1-9730-dfd53412580b";
        string guid10 = "975e4116-896c-4551-9aab-b2a2180a8266";
        string guid11 = "banana";
       

        [Fact]
        public void GivenNoActiveGuids_ReturnZero()
        {
            var ids = new List<string> { guid5 };

            var results = _testClass.HowManyActive(ids);

            Assert.Equal(0, results);
        }

        [Fact]
        public void GivenActiveAndInactive_ReturnCorrectNumberOfActive()
        {
            var ids = new List<string> { guid1, guid2, guid3, guid4, guid5, guid6, guid7, guid8, guid9, guid10 };

            var results = _testClass.HowManyActive(ids);

            Assert.Equal(5, results);
        }

        [Fact]
        public void ThrowError_WhenGuidNotValid()
        {
            var ids = new List<string> { guid1, guid2, guid3, guid4, guid5, guid11 };

            var results = Assert.Throws<ActivityException>(() => _testClass.HowManyActive(ids));

            Assert.Equal("banana is not a proper Guid.", results.Message);
        }
    }

}
