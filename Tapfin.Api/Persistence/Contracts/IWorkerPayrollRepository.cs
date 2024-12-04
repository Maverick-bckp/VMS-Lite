using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IWorkerPayrollRepository
    {
        Task<WorkerPayroll> getPayrollData(int? workerId = null, string? payrollMonth = null, string? payrollYear = null);
        Task addWorkerPayrollDetails(WorkerPayroll workerPayroll);
        Task updateWorkerPayrollDetails(WorkerPayroll workerPayroll);
    }
}
