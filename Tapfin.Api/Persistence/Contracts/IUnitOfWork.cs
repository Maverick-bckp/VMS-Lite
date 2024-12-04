namespace Tapfin.Api.Persistence.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRegionRepository regionRepository { get; }
        ICountryRepository countryRepository { get; }
        IClientRepository clientRepository  { get; }
        IClientAddressRepository ClientAddressRepository  { get; }
        IClientRevenueRepository clientRevenueRepository { get; }
        IVendorRepository vendorRepository  { get; }
        IJobRepository jobRepository  { get; }
        IAccountsRepository accountsRepository  { get; }
        IJobOrderRepository jobOrderRepository  { get; }
        IDepartmentRepository departmentRepository  { get; }
        IServiceTypeRepository serviceTypeRepository  { get; }
        IAllocatedAtClientRepository allocatedAtClientRepository  { get; }
        IVendorBillingRepository VendorBillingRepository  { get; }
        IVendorAddressRepository VendorAddressRepository  { get; }
        IWorkerRepository workerRepository  { get; }
        IWorkerEquipmentCostRepository WorkerEquipmentCostRepository { get; }
        IWorkerEvaluationRepository WorkerEvaluationRepository { get; }
        IWorkerBackgroundEvaluationRepository workerBackgroundEvaluationRepository { get; }
        IWorkerExtensionRepository workerExtensionRepository { get; }
        IWorkerPayrollRepository workerPayrollRepository { get; }
        IWorkerBIRepository workerBIRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
