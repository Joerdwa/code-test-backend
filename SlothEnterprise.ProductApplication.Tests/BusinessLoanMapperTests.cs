using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class BusinessLoanMapperTests
    {
        [Fact]
        public void Can_Map_To_LoanRequest() {
            var businessLoans = new BusinessLoans
            {
                Id = 1,
                InterestRatePerAnnum = 0.05M,
                LoanAmount = 100,
            };

            var expected = new LoansRequest
            {
                InterestRatePerAnnum = 0.05M,
                LoanAmount = 100,
            };

            var sut = new BusinessLoanMapper();

            var result = sut.MapToLoanRequest(businessLoans);

            Assert.Equal(expected.InterestRatePerAnnum, result.InterestRatePerAnnum);
            Assert.Equal(expected.LoanAmount, result.LoanAmount);
        }
    }
}
