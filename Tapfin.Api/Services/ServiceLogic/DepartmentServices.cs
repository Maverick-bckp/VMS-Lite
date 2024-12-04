using AutoMapper;
using System.Net;
using Tapfin.Api.Helpers.Result;
using Tapfin.Api.Models.DTO;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Persistence.Contracts;
using Tapfin.Api.Services.Contracts;

namespace Tapfin.Api.Services.ServiceLogic
{
    public class DepartmentServices : IDepartmentServices
    {
        Result _result = new Result();
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IAccountsServices _accountsServices;
        User _currentUser;

        public DepartmentServices(IUnitOfWork uow, IMapper mapper, IAccountsServices accountsServices)
        {
            _uow = uow;
            _mapper = mapper;
            _accountsServices = accountsServices;

            _currentUser = _accountsServices.GetLoggedInUser();
        }

        public async Task<dynamic> getAllDepartment()
        {
            var departmentList = await _uow.departmentRepository.getAllAsync();
            dynamic departmentListMapped = _mapper.Map<List<GetDepartmentDtoResponse>>(departmentList);

            if (departmentList.Count == 0)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, departmentListMapped, "All departments has been fetched successfully.");
        }

        public async Task<dynamic> getDepartmentById(int Id)
        {
            var department = await _uow.departmentRepository.getDepartmentById(Id);
            dynamic departmentMapped = _mapper.Map<GetDepartmentDtoResponse>(department);

            if (department == null)
            {
                return _result.AddReturnData(HttpStatusCode.NoContent, "No Data Found.");
            }

            return _result.AddReturnData(HttpStatusCode.OK, departmentMapped, "Department data has been fetched successfully.");
        }

        public async Task<dynamic> createDepartment(CreateDepartmentDtoRequest request)
        {
            var department = _mapper.Map<Department>(request);
            department.CreatedBy = _currentUser.UserCustomId;

            await _uow.departmentRepository.addDepartment(department);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Department is not created. Please try again.");
            }
            return _result.AddReturnData(HttpStatusCode.Created, message: "Department is created successfully.");
        }

        public async Task<dynamic> updateDepartment(UpdateDepartmentDtoRequest request)
        {
            var department = _mapper.Map<Department>(request);
            department.UpdatedBy = _currentUser.UserCustomId;

            await _uow.departmentRepository.updateDepartment(department);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Department details is not updated. Please try again.");
            }
            return _result.AddReturnData(HttpStatusCode.OK, message: "Department data is updated successfully.");
        }

        public async Task<dynamic> deleteDepartment(int Id)
        {
            var userCustomId = _currentUser.UserCustomId;

            await _uow.departmentRepository.deleteDepartment(Id, userCustomId);
            int status = (int)await _uow.SaveChangesAsync();

            if (status < 1)
            {
                return _result.AddReturnData(HttpStatusCode.BadRequest, message: "Department is not deleted. Please try again.");
            }
            return _result.AddReturnData(HttpStatusCode.OK, message: "Department is deleted successfully.");
        }
    }
}
