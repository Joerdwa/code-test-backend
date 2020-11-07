using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication.Mappers.Interfaces
{
    public interface IBusinessLoanMapper
    {
        LoansRequest MapToLoanRequest(BusinessLoans loans);
    }
}
