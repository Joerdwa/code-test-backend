using SlothEnterprise.ProductApplication.Applications.Interfaces;
using SlothEnterprise.ProductApplication.Products.Interfaces;

namespace SlothEnterprise.ProductApplication.Applications
{
    public class SellerApplication : ISellerApplication
    {
        public IProduct Product { get; set; }
        public ISellerCompanyData CompanyData { get; set; }
    }
}