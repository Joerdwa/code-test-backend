using SlothEnterprise.External;
using SlothEnterprise.ProductApplication.Mappers.Interfaces;

namespace SlothEnterprise.ProductApplication.Mappers
{
    public class ApplicationResultMapper : IApplicationResultMapper
    {
        public int MapToResultCode(IApplicationResult applicationResult)
        {
            return (applicationResult.Success) ? applicationResult.ApplicationId ?? -1 : -1;
        }
    }
}
