using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IWorkerPayrollServices
    {
        Task<dynamic> getWorkerListByClientIDToDownloadFormat(GetPayrollWorkerListRequest request);
        Task<dynamic> UploadWorkerPayrollDetails(UploadPayrollWorkerDetailsRequest request);
    }
}
