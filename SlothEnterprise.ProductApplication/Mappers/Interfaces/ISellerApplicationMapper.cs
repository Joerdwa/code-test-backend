using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications.Interfaces;

namespace SlothEnterprise.ProductApplication.Mappers.Interfaces
{
    public interface ISellerApplicationMapper
    {
        CompanyDataRequest MapToCompanyDataRequest(ISellerApplication sellerApplication);
    }
}
