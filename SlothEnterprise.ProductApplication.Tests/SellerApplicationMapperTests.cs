using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Products;
using System;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class SellerApplicationMapperTests
    {

        [Fact]
        public void Can_Map_To_CompanyDataRequest() {
            var confidentialInvoiceDiscount = new ConfidentialInvoiceDiscount
            {
                Id = 1,
                TotalLedgerNetworth = 200,
                AdvancePercentage = 80,
                VatRate = 0.20M
            };

            var sellerApplication = new SellerApplication
            {
                Product = confidentialInvoiceDiscount,
                CompanyData = new SellerCompanyData
                {
                    Name = "Company 1",
                    Number = 1,
                    DirectorName = "Director 1",
                    Founded = new DateTime(2020, 1, 1),
                }
            };

            var expected = new CompanyDataRequest
            {
                CompanyFounded = new DateTime(2020, 1, 1),
                CompanyNumber = 1,
                CompanyName = "Company 1",
                DirectorName = "Director 1"
            };

            var sut = new SellerApplicationMapper();

            var result = sut.MapToCompanyDataRequest(sellerApplication);

            Assert.Equal(sellerApplication.CompanyData.Founded, expected.CompanyFounded);
            Assert.Equal(sellerApplication.CompanyData.Number, expected.CompanyNumber);
            Assert.Equal(sellerApplication.CompanyData.Name, expected.CompanyName);
            Assert.Equal(sellerApplication.CompanyData.DirectorName, expected.DirectorName);
        }
    }
}
