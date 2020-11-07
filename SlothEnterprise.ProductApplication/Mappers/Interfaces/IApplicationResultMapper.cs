using SlothEnterprise.External;

namespace SlothEnterprise.ProductApplication.Mappers.Interfaces
{
    public interface IApplicationResultMapper
    {
        int MapToResultCode(IApplicationResult applicationResult);
    }
}
