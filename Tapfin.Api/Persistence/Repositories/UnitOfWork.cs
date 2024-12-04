using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tapfin.Api.Models;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TapfinDbContext _dbContext;
        private readonly IAccountsServices _accounts;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IRegionRepository regionRepository => new RegionRepository(_dbContext);
        public ICountryRepository countryRepository => new CountryRepository(_dbContext);
        public IClientRepository clientRepository => new ClientRepository(_dbContext);
        public IClientAddressRepository ClientAddressRepository => new ClientAddressRepository(_dbContext);
        public IClientRevenueRepository clientRevenueRepository => new ClientRevenueRepository(_dbContext);
        public IVendorRepository vendorRepository => new VendorRepository(_dbContext);
        public IJobRepository jobRepository => new JobRepository(_dbContext);
        public IAccountsRepository accountsRepository => new AccountsRepository(_dbContext);
        public IJobOrderRepository jobOrderRepository => new JobOrderRepository(_dbContext, _accounts);
        public IDepartmentRepository departmentRepository => new DepartmentRepository(_dbContext);
        public IServiceTypeRepository serviceTypeRepository => new ServiceTypeRepository(_dbContext);
        public IAllocatedAtClientRepository allocatedAtClientRepository => new AllocatedAtClientRepository(_dbContext);
        public IVendorBillingRepository VendorBillingRepository => new VendorBillingRepository(_dbContext);
        public IVendorAddressRepository VendorAddressRepository => new VendorAddressRepository(_dbContext);
        public IWorkerRepository workerRepository => new WorkerRepository(_dbContext);
        public IWorkerEquipmentCostRepository WorkerEquipmentCostRepository => new WorkerEquipmentCostRepository(_dbContext);
        public IWorkerEvaluationRepository WorkerEvaluationRepository => new WorkerEvaluationRepository(_dbContext);
        public IWorkerBackgroundEvaluationRepository workerBackgroundEvaluationRepository => new WorkerBackgroundEvaluationRepository(_dbContext);
        public IWorkerExtensionRepository workerExtensionRepository => new WorkerExtensionRepository(_dbContext);
        public IWorkerPayrollRepository workerPayrollRepository => new WorkerPayrollRepository(_dbContext);
        public IWorkerBIRepository workerBIRepository => new WorkerBIRepository(_dbContext);

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _dbContext.Dispose();
        }
    }
}
