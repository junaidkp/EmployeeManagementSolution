using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.Services
{

    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _repoMock;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _repoMock = new Mock<IEmployeeRepository>();
            _service = new EmployeeService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEmployees()
        {
            // Arrange
            _repoMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(new List<Employee>
                {
                new Employee { EmployeeId = 1, EmployeeName = "John" }
                });

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenExists()
        {
            // Arrange
            var employee = new Employee { EmployeeId = 1, EmployeeName = "John" };

            _repoMock.Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(employee);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.EmployeeName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenEmployeeNotFound()
        {
            // Arrange
            _repoMock.Setup(x => x.GetByIdAsync(99))
                .ReturnsAsync((Employee)null);

            var dto = new CreateEmployeeDto
            {
                EmployeeName = "Test",
                Email = "test@test.com",
                Department = "IT",
                DateOfJoining = DateTime.Today
            };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                _service.UpdateAsync(99, dto));
        }
    }
}
