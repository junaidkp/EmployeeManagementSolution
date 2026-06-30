using EmployeeManagement.Application.Common;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    /// <summary>
    /// Employee Management APIs for CRUD operations.
    /// Requires JWT authentication for all endpoints.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeesController(IEmployeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all employees from the system.
        /// </summary>
        /// <returns>List of employees</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<EmployeeDto>>.SuccessResponse(result));
        }

        /// <summary>
        /// Get employee details by unique ID.
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns>Employee details if found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
                return NotFound(ApiResponse<string>.ErrorResponse("Employee not found"));

            return Ok(ApiResponse<EmployeeDto>.SuccessResponse(result));
        }

        /// <summary>
        /// Create a new employee record.
        /// </summary>
        /// <param name="dto">Employee creation data</param>
        /// <returns>Newly created employee ID</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            var id = await _service.CreateAsync(dto);

            return Ok(ApiResponse<int>.SuccessResponse(id, "Employee created successfully"));
        }

        /// <summary>
        /// Update existing employee details.
        /// </summary>
        /// <param name="id">Employee ID to update</param>
        /// <param name="dto">Updated employee data</param>
        /// <returns>Success message</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateEmployeeDto dto)
        {
            await _service.UpdateAsync(id, dto);

            return Ok(ApiResponse<string>.SuccessResponse("Updated successfully"));
        }

        /// <summary>
        /// Delete an employee from the system.
        /// </summary>
        /// <param name="id">Employee ID to delete</param>
        /// <returns>Success message</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(ApiResponse<string>.SuccessResponse("Deleted successfully"));
        }
    }
}