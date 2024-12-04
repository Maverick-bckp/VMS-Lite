using Microsoft.EntityFrameworkCore;
using Tapfin.Api.Models.Entities;
using Tapfin.Api.Models;
using Tapfin.Api.Persistence.Contracts;

namespace Tapfin.Api.Persistence.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly TapfinDbContext _dbContext;
        public DepartmentRepository(TapfinDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Department>> getAllAsync()
        {
            var departments = await _dbContext.Department.AsQueryable().Where(w => w.IsActive == true).ToListAsync();
            return departments;
        }

        public async Task<Department> getDepartmentById(int departmentId)
        {
            var department = await _dbContext.Department.AsQueryable().Where(w => w.IsActive == true && w.Id == departmentId).FirstOrDefaultAsync();
            return department;
        }
        public async Task addDepartment(Department department)
        {
            await _dbContext.Department.AddAsync(department);
        }

        public async Task deleteDepartment(int Id, string userCustomId)
        {
            var department = _dbContext.Department.AsQueryable().Where(w => w.IsActive == true && w.Id == Id).FirstOrDefault();
            if (department != null)
            {
                department.IsActive = false;
                department.DeletedAt = DateTime.Now;
                department.DeletedBy = userCustomId;
            }
        }

        public async Task updateDepartment(Department department)
        {
            var departmentData = _dbContext.Department.AsQueryable().Where(w => w.IsActive == true && w.Id == department.Id).FirstOrDefault();
            if (departmentData != null)
            {
                departmentData.DepartmentName = department.DepartmentName;
                departmentData.DepartmentCode = department.DepartmentCode;
                departmentData.UpdatedAt = DateTime.UtcNow;
                departmentData.UpdatedBy = department.UpdatedBy;

                _dbContext.Entry(departmentData).State = EntityState.Modified;
            }
        }
    }
}
