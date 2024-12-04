using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerPayrollRepository : IWorkerPayrollRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerPayrollRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkerPayroll> getPayrollData(int? workerId = null, string? payrollMonth = null, string? payrollYear = null)
        {
            var workerPayroll = await _dbContext.WorkerPayroll.AsQueryable().Where(w => w.IsActive == true && w.WorkerId == workerId && w.PayrollMonth == payrollMonth && w.PayrollYear == payrollYear)
                        .FirstOrDefaultAsync();

            return workerPayroll;
        }

        public async Task addWorkerPayrollDetails(WorkerPayroll workerPayroll)
        {
            await _dbContext.WorkerPayroll.AddAsync(workerPayroll);
        }

        public async Task updateWorkerPayrollDetails(WorkerPayroll workerPayroll)
        {
            var workerPayrollDetails = _dbContext.WorkerPayroll.AsQueryable().Where(w => w.IsActive == true && w.Id == workerPayroll.Id).FirstOrDefault();
            if (workerPayrollDetails != null)
            {
                workerPayrollDetails.Id = workerPayroll.Id;
                workerPayrollDetails.WorkerId = workerPayroll.WorkerId;
                workerPayrollDetails.PayrollMonth = workerPayroll.PayrollMonth;
                workerPayrollDetails.PayrollYear = workerPayroll.PayrollYear;
                workerPayrollDetails.Amount = workerPayroll.Amount;
                workerPayrollDetails.Comment = workerPayroll.Comment;
                workerPayrollDetails.UpdatedAt = DateTime.UtcNow;
                workerPayrollDetails.UpdatedBy = workerPayroll.UpdatedBy;

                _dbContext.Entry(workerPayrollDetails).State = EntityState.Modified;
            }
        }
    }
}
