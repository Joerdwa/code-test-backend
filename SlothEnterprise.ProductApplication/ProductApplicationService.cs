using System;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications.Interfaces;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;
        }

        public int SubmitApplicationFor(ISellerApplication application)
        {
            switch (application.Product) {
                case SelectiveInvoiceDiscount sid:
                    return SubmitApplicationForSelectiveInvoiceDiscount(application, sid);
                case ConfidentialInvoiceDiscount cid:
                    return SubmitApplicationForConfidentialInvoiceDiscount(application, cid);
                case BusinessLoans loans:
                    return SubmitApplicationForBusinessLoans(application, loans);
                default:
                    throw new InvalidOperationException();
            }
        }

        private int SubmitApplicationForSelectiveInvoiceDiscount(ISellerApplication application, SelectiveInvoiceDiscount sid) {
            return _selectInvoiceService.SubmitApplicationFor(application.CompanyData.Number.ToString(), sid.InvoiceAmount, sid.AdvancePercentage);
        }

        private int SubmitApplicationForConfidentialInvoiceDiscount(ISellerApplication application, ConfidentialInvoiceDiscount cid) {
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                    GetCompanyDataRequestFromSellerApplication(application),
                    cid.TotalLedgerNetworth,
                    cid.AdvancePercentage,
                    cid.VatRate
            );

            return GetSuccessCodeFromApplicationResult(result);
        }

        private int SubmitApplicationForBusinessLoans(ISellerApplication application, BusinessLoans loans) {
            var result = _businessLoansService.SubmitApplicationFor(
                GetCompanyDataRequestFromSellerApplication(application), 
                new LoansRequest
                {
                    InterestRatePerAnnum = loans.InterestRatePerAnnum,
                    LoanAmount = loans.LoanAmount
                }
            );

            return GetSuccessCodeFromApplicationResult(result);
        }

        private CompanyDataRequest GetCompanyDataRequestFromSellerApplication(ISellerApplication application) {
            return new CompanyDataRequest
            {
                CompanyFounded = application.CompanyData.Founded,
                CompanyNumber = application.CompanyData.Number,
                CompanyName = application.CompanyData.Name,
                DirectorName = application.CompanyData.DirectorName
            };
        }

        private int GetSuccessCodeFromApplicationResult(IApplicationResult applicationResult) {
            return (applicationResult.Success) ? applicationResult.ApplicationId ?? -1 : -1;
        }
    }
}
