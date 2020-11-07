using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Mappers;
using System.Collections.Generic;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ApplicationResultMapperTests
    {
        [Fact]
        public void Mapping_Of_Sucessful_Application_Result_Returns_1()
        {
            var successfulApplicationResult = new ApplicationResult()
            {
                ApplicationId = 1,
                Success = true,
                Errors = new List<string>(),
            };

            var sut = new ApplicationResultMapper();

            var result = sut.MapToResultCode(successfulApplicationResult);

            Assert.Equal(1, result);
        }

        [Fact]
        public void Mapping_Of_SUnsucessful_Application_Result_Returns_minus_1()
        {
            var successfulApplicationResult = new ApplicationResult()
            {
                ApplicationId = 2,
                Success = false,
                Errors = new List<string>(),
            };

            var sut = new ApplicationResultMapper();

            var result = sut.MapToResultCode(successfulApplicationResult);

            Assert.Equal(-1, result);
        }
    }

    internal class ApplicationResult : IApplicationResult
    {
        public int? ApplicationId { get; set; }
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }
}
