using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly TapfinDbContext _dbContext;
        public WorkerRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<WorkerDetail>> getAllAsync(int? jobId = null, int? jobOrderId = null, int? countryId = null)
        {
            var workerList = await _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.CountryId == countryId && (jobId == null || w.JobId == jobId) && (jobOrderId == null || w.JobOrderId == jobOrderId))
                        .Include(o => o.JobDetail)
                        .Include(o => o.JobOrder)
                        .Include(o => o.ClientDetail)
                        .Include(o => o.VendorDetail)
                        .Include(o => o.WorkerStatusType)
                        .Include(o => o.WorkerContractStatusType)
                        .Include(o => o.WorkerEquipmentCost)
                        .ToListAsync();

            return workerList;
        }

        public async Task<List<WorkerDetail>> getAllByClientId(int? clientId = null, int? countryId = null)
        {
            var workerList = await _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.ClientId == clientId && w.CountryId == countryId)
                .Include (o => o.ServiceType)
                .ToListAsync();

            return workerList;
        }

        public async Task<WorkerDetail> getWorkerDetailsById(int workerId, int? countryId)
        {
            var workerDetails = await _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.CountryId == countryId && w.Id == workerId)
                        .Include(o => o.ClientDetail)
                        .Include(o => o.VendorDetail)
                        .Include(o => o.WorkerStatusType)
                        .Include(o => o.WorkerContractStatusType)
                        .FirstOrDefaultAsync();
            return workerDetails;
        }

        public async Task<WorkerDetail> getWorkerDetails(int workerId, string workerCode, string workerName)
        {
            var workerDetails = await _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.Id == workerId && w.WorkerCode == workerCode && w.WorkerName == workerName).FirstOrDefaultAsync();

            return workerDetails;
        }

        public async Task<WorkerDetail> getWorkerFullDetailsById(int workerId, int? countryId)
        {
            var workerDetails = await _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.CountryId == countryId && w.Id == workerId)
                        .Include(o => o.JobDetail)
                        .Include(o => o.JobOrder)
                        .Include(o => o.AllocatedAtClient)
                        .Include(o => o.Department)
                        .Include(o => o.ClientDetail)
                        .Include(o => o.VendorDetail)
                        .Include(o => o.WorkerStatusType)
                        .Include(o => o.WorkerContractStatusType)
                        .Include(o => o.WorkerEquipmentCost.Where(w => w.IsActive == true))
                        .FirstOrDefaultAsync();
            return workerDetails;
        }

        public async Task<WorkerDetail> getWorkerDetailsByJobOrderId(int jobOrderId)
        {
            var workerDetails = await _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.JobOrderId == jobOrderId)
                        .Include(o => o.JobDetail)
                        .Include(o => o.JobOrder)
                        .Include(o => o.ClientDetail)
                        .Include(o => o.VendorDetail)
                        .Include(o => o.WorkerStatusType)
                        .Include(o => o.WorkerContractStatusType)
                        .Include(o => o.WorkerEquipmentCost.Where(w => w.IsActive == true))
                        .FirstOrDefaultAsync();
            return workerDetails;
        }

        public async Task<List<WorkerStatusTypes>> getWorkerStatusTypeAsync()
        {
            var workerStatusTypeList = await _dbContext.WorkerStatusType.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return workerStatusTypeList;
        }

        public async Task<List<WorkerContractStatusTypes>> getWorkerContractStatusTypeAsync()
        {
            var workerContractTypeList = await _dbContext.WorkerContractStatusType.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return workerContractTypeList;
        }

        public async Task addWorkerDetails(WorkerDetail workerDetail)
        {
            await _dbContext.WorkerDetail.AddAsync(workerDetail);
        }

        public async Task updateWorkerDetails(WorkerDetail workerDetail)
        {
            var workerDetails = _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.Id == workerDetail.Id).FirstOrDefault();
            if (workerDetails != null)
            {
                workerDetails.JobOrderId = workerDetail.JobOrderId;
                workerDetails.ServiceTypeId = workerDetail.ServiceTypeId;
                workerDetails.WorkerName = workerDetail.WorkerName;
                workerDetails.WorkerCode = workerDetail.WorkerCode;
                workerDetails.CPF = workerDetail.CPF;
                workerDetails.Salary = workerDetail.Salary;
                workerDetails.RecruiterName = workerDetail.RecruiterName;
                workerDetails.IdentificationNo = workerDetail.IdentificationNo;
                workerDetails.HiringDate = workerDetail.HiringDate;
                workerDetails.WorkerLocation = workerDetail.WorkerLocation;
                workerDetails.DateOfBirth = workerDetail.DateOfBirth;
                workerDetails.ReasonForHiring = workerDetail.ReasonForHiring;
                workerDetails.DepartmentId = workerDetail.DepartmentId;
                workerDetails.PersonalPhone = workerDetail.PersonalPhone;
                workerDetails.WorkPhone = workerDetail.WorkPhone;
                workerDetails.TerminationDate = workerDetail.TerminationDate;
                workerDetails.ReasonForTermination = workerDetail.ReasonForTermination;
                workerDetails.PersonalEmail = workerDetail.PersonalEmail;
                workerDetails.WorkEmail = workerDetail.WorkEmail;
                workerDetails.ContractStartDate = workerDetail.ContractStartDate;
                workerDetails.ContractExpiryDate = workerDetail.ContractExpiryDate;
                workerDetails.MonthlyWorkload = workerDetail.MonthlyWorkload;
                workerDetails.ContractType = workerDetail.ContractType;
                workerDetails.AllocatedAtClientId = workerDetail.AllocatedAtClientId;
                workerDetails.ZipCode = workerDetail.ZipCode;
                workerDetails.Street = workerDetail.Street;
                workerDetails.Number = workerDetail.Number;
                workerDetails.Neighbourhood = workerDetail.Neighbourhood;
                workerDetails.State = workerDetail.State;
                workerDetails.City = workerDetail.City;
                workerDetails.WorkerStatus = workerDetail.WorkerStatus;
                workerDetails.UpdatedAt = DateTime.UtcNow;
                workerDetails.UpdatedBy = workerDetail.UpdatedBy;

                _dbContext.Entry(workerDetails).State = EntityState.Modified;
            }
        }

        public async Task deleteWorker(int workerId, string userCustomId)
        {
            var workerData = _dbContext.WorkerDetail.AsQueryable().Where(w => w.IsActive == true && w.Id == workerId).FirstOrDefault();
            if (workerData != null)
            {
                workerData.WorkerStatus = 2;
                workerData.DeletedAt = DateTime.Now;
                workerData.DeletedBy = userCustomId;
            }
        }
    }
}
