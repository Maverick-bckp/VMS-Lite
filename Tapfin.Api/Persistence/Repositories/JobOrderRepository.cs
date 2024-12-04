using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class JobOrderRepository : IJobOrderRepository
    {
        private readonly TapfinDbContext _dbContext;
        private readonly IAccountsServices _accountsServices;
        public JobOrderRepository(TapfinDbContext dbContext, IAccountsServices accountsServices)
        {
            _dbContext = dbContext;
            _accountsServices = accountsServices;          
        }

        public async Task<List<JobOrder>> getAllAsync(int jobId)
        {
            var jobOrdersList = await _dbContext.JobOrders.AsQueryable().Where(w => w.IsActive == true && w.JobId == jobId)
                .Include(o => o.Vendor)
                .Include(o => o.JobOrderStatusType)
                .Include(o => o.WorkerDetail.Where(w => w.IsActive == true))
                .ToListAsync();
            return jobOrdersList;
        }

        public async Task<List<JobOrderStatusType>> getJobOrderStatusTypeAsync()
        {
            var jobOrderStatusTypeList = await _dbContext.JobOrderStatusType.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return jobOrderStatusTypeList;
        }

        public async Task<JobOrder> getJobOrderDetailsById(int jobOrderId)
        {
            var jobOrderDetails = await _dbContext.JobOrders.AsQueryable().Where(w => w.IsActive == true && w.Id == jobOrderId)
                .Include(o => o.Vendor)
                .Include(o => o.JobOrderStatusType)
                .Include(o => o.WorkerDetail.Where(w => w.IsActive == true))
                .FirstOrDefaultAsync();
            return jobOrderDetails;
        }

        public async Task addJobOrder(JobOrder jobOrder)
        {
            await _dbContext.JobOrders.AddAsync(jobOrder);
        }

        public async Task updateJobOrderDetails(JobOrder jobOrder)
        {

            var jobOrderData = _dbContext.JobOrders.AsQueryable().Where(w => w.IsActive == true && w.Id == jobOrder.Id).FirstOrDefault();
            if (jobOrderData != null)
            {
                jobOrderData.JobId = jobOrder.JobId;
                jobOrderData.VendorId = jobOrder.VendorId;
                jobOrderData.JobOrderStatus = jobOrder.JobOrderStatus;
                jobOrderData.UpdatedAt = DateTime.UtcNow;
                jobOrderData.UpdatedBy = jobOrder.UpdatedBy;

                _dbContext.Entry(jobOrderData).State = EntityState.Modified;
            }
        }

        public async Task deleteJobOrder(int Id, string userCustomId)
        {
            var jobOrderData = _dbContext.JobOrders.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();
            if (jobOrderData != null)
            {
                jobOrderData.IsActive = false;
                jobOrderData.DeletedAt = DateTime.Now;
                jobOrderData.DeletedBy = userCustomId;
            }
        }
    }
}
