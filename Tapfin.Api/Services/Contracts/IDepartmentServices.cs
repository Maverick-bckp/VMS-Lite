using Tapfin.Api.Models.DTO;

namespace Tapfin.Api.Services.Contracts
{
    public interface IDepartmentServices
    {
        Task<dynamic> getAllDepartment();
        Task<dynamic> getDepartmentById(int Id);
        Task<dynamic> createDepartment(CreateDepartmentDtoRequest request);
        Task<dynamic> updateDepartment(UpdateDepartmentDtoRequest request);
        Task<dynamic> deleteDepartment(int Id);
    }
}
