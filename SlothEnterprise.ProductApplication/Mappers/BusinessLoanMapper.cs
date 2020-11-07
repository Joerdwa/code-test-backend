using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Mappers.Interfaces;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public class BusinessLoanMapper : IBusinessLoanMapper
    {
        public LoansRequest MapToLoanRequest(BusinessLoans loans)
        {
            return new LoansRequest
            {
                InterestRatePerAnnum = loans.InterestRatePerAnnum,
                LoanAmount = loans.LoanAmount
            };
        }
    }
}
