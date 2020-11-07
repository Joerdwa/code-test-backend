using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications.Interfaces;
using SlothEnterprise.ProductApplication.Mappers.Interfaces;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public class SellerApplicationMapper : ISellerApplicationMapper
    {
        public CompanyDataRequest MapToCompanyDataRequest(ISellerApplication sellerApplication)
        {
            return new CompanyDataRequest
            {
                CompanyFounded = sellerApplication.CompanyData.Founded,
                CompanyNumber = sellerApplication.CompanyData.Number,
                CompanyName = sellerApplication.CompanyData.Name,
                DirectorName = sellerApplication.CompanyData.DirectorName
            };
        }
    }
}
