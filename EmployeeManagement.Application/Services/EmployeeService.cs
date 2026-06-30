using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeService(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var data = await _repo.GetAllAsync();
            return data.Select(MapToDto);
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            return emp == null ? null : MapToDto(emp);
        }

        public async Task<int> CreateAsync(CreateEmployeeDto dto)
        {
            var emp = new Employee
            {
                EmployeeName = dto.EmployeeName,
                Email = dto.Email,
                Department = dto.Department,
                DateOfJoining = dto.DateOfJoining,
                CreatedAt = DateTime.Now
            };

            await _repo.AddAsync(emp);

            return emp.EmployeeId;

        }

        public async Task UpdateAsync(int id, CreateEmployeeDto dto)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found");

            emp.EmployeeName = dto.EmployeeName;
            emp.Email = dto.Email;
            emp.Department = dto.Department;
            emp.DateOfJoining = dto.DateOfJoining;
            emp.UpdatedAt = DateTime.Now;

            await _repo.UpdateAsync(emp);
        }

        public async Task DeleteAsync(int id)
        {
            var emp = await _repo.GetByIdAsync(id);
            if (emp == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found");

            await _repo.DeleteAsync(emp);
        }

        private EmployeeDto MapToDto(Employee e) => new()
        {
            EmployeeId = e.EmployeeId,
            EmployeeName = e.EmployeeName,
            Email = e.Email,
            Department = e.Department,
            DateOfJoining = e.DateOfJoining
        };
    }
}
