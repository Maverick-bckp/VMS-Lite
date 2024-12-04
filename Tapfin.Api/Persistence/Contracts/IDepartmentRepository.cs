using Tapfin.Api.Models.Entities;

namespace Tapfin.Api.Persistence.Contracts
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> getAllAsync();
        Task<Department> getDepartmentById(int departmentId);
        Task addDepartment(Department department);
        Task deleteDepartment(int Id, string userCustomId);
        Task updateDepartment(Department department);
    }
}
