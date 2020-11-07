using Moq;
using NSubstitute.ExceptionExtensions;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using SlothEnterprise.ProductApplication.Products.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {
        private ApplicationResult GetSuccessfulApplicationResult()
        {
            return new ApplicationResult()
            {
                ApplicationId = 1,
                Success = true,
                Errors = new List<string>(),
            };
        }

        private SellerCompanyData GetCompanyData() {
            return new SellerCompanyData
            {
                Name = "Company 1",
                Number = 1,
                DirectorName = "Director 1",
                Founded = new DateTime(2020, 1, 1),
            };
        }

        [Fact]
        public void Can_Submit_Application_For_SelectiveInvoiceDiscount() {

            // Arrange

            var selectInvoiceDiscount = new SelectiveInvoiceDiscount
            {
                Id = 1,
                InvoiceAmount = 200,
                AdvancePercentage = 80,
            };

            var sellerApplication = new SellerApplication {
                Product = selectInvoiceDiscount,
                CompanyData = GetCompanyData(),
            };

            var mockSelectInvoiceService = new Mock<ISelectInvoiceService>();
            mockSelectInvoiceService.Setup(sis => sis.SubmitApplicationFor(It.IsAny<string>(), 200, 80)).Returns(1);

            var sut = new ProductApplicationService(
                mockSelectInvoiceService.Object,
                new Mock<IConfidentialInvoiceService>().Object,
                new Mock<IBusinessLoansService>().Object
            );

            // Act

            var applicationResult = sut.SubmitApplicationFor(sellerApplication);

            // Assert
            Assert.Equal(1, applicationResult);
            mockSelectInvoiceService.VerifyAll();
        }

        [Fact]
        public void Can_Submit_Application_For_Product_ConfidentialInvoiceDiscount()
        {
            // Arrange

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
                CompanyData = GetCompanyData(),
            };

            var mockConfidentialInvoiceService = new Mock<IConfidentialInvoiceService>();
            mockConfidentialInvoiceService.Setup(cis => cis.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), 200, 80, 0.20M)).Returns(GetSuccessfulApplicationResult());

            var sut = new ProductApplicationService(
                new Mock<ISelectInvoiceService>().Object,
                mockConfidentialInvoiceService.Object,
                new Mock<IBusinessLoansService>().Object
            );

            // Act
            var applicationResult = sut.SubmitApplicationFor(sellerApplication);

            // Assert
            Assert.Equal(1, applicationResult);
            mockConfidentialInvoiceService.VerifyAll();
        }

        [Fact]
        public void Can_Submit_Application_For_Product_BusinessLoans()
        {
            // Arrange

            var businessLoans = new BusinessLoans
            {
                Id = 1,
                InterestRatePerAnnum = 0.05M,
                LoanAmount = 100,
            };

            var sellerApplication = new SellerApplication
            {
                Product = businessLoans,
                CompanyData = GetCompanyData(),
            };

            var mockBusinessLoansService = new Mock<IBusinessLoansService>();
            mockBusinessLoansService.Setup(bls => bls.SubmitApplicationFor(It.IsAny<CompanyDataRequest>(), It.IsAny<LoansRequest>())).Returns(GetSuccessfulApplicationResult());

            var sut = new ProductApplicationService(
                new Mock<ISelectInvoiceService>().Object,
                new Mock<IConfidentialInvoiceService>().Object,
                mockBusinessLoansService.Object
            );

            // Act
            var applicationResult = sut.SubmitApplicationFor(sellerApplication);

            // Assert
            Assert.Equal(1, applicationResult);
            mockBusinessLoansService.VerifyAll();
        }

        [Fact]
        public void Can_Not_Submit_Application_For_Unsupported_Product()
        {
            // Arrange

            var unsupportedProduct = new UnsupportedProduct
            {
                Id = 1,
                LoanAmount = 200,
            };

            var sellerApplication = new SellerApplication
            {
                Product = unsupportedProduct,
                CompanyData = GetCompanyData(),
            };

            var sut = new ProductApplicationService(
                new Mock<ISelectInvoiceService>().Object,
                new Mock<IConfidentialInvoiceService>().Object,
                new Mock<IBusinessLoansService>().Object
            );

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => sut.SubmitApplicationFor(sellerApplication));
        }



        internal class ApplicationResult : IApplicationResult {
            public int? ApplicationId { get; set; }
            public bool Success { get; set; }
            public IList<string> Errors { get; set; }
        }

        internal class UnsupportedProduct : IProduct {
            public int Id { get; set; }
            public decimal LoanAmount { get; set; }
        }
    }
}
