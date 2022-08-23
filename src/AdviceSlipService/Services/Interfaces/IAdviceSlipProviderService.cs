using AdviceSlipService.Models;

namespace AdviceSlipService.Services.Interfaces
{
    public interface IAdviceSlipProviderService
    {
        Task<AdviceResponse> GetAdviceSlip(AdviceRequest request);
    }
}
