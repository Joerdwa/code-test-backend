using System;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications.Interfaces;
using SlothEnterprise.ProductApplication.Mappers;
using SlothEnterprise.ProductApplication.Mappers.Interfaces;
using SlothEnterprise.ProductApplication.Products;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;
        private readonly IBusinessLoansService _businessLoansService;

        // Mappers
        private readonly IApplicationResultMapper _applicationResultMapper;
        private readonly ISellerApplicationMapper _sellerApplicationMapper;
        private readonly IBusinessLoanMapper _businessLoanMapper;

        public ProductApplicationService(ISelectInvoiceService selectInvoiceService, IConfidentialInvoiceService confidentialInvoiceWebService, IBusinessLoansService businessLoansService)
        {
            _selectInvoiceService = selectInvoiceService;
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
            _businessLoansService = businessLoansService;

            _applicationResultMapper = new ApplicationResultMapper();
            _sellerApplicationMapper = new SellerApplicationMapper();
            _businessLoanMapper = new BusinessLoanMapper();
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
                    _sellerApplicationMapper.MapToCompanyDataRequest(application),
                    cid.TotalLedgerNetworth,
                    cid.AdvancePercentage,
                    cid.VatRate
            );

            return _applicationResultMapper.MapToResultCode(result);
        }

        private int SubmitApplicationForBusinessLoans(ISellerApplication application, BusinessLoans loans) {
            var result = _businessLoansService.SubmitApplicationFor(
                _sellerApplicationMapper.MapToCompanyDataRequest(application),
                _businessLoanMapper.MapToLoanRequest(loans)
            );

            return _applicationResultMapper.MapToResultCode(result);
        }
    }
}
