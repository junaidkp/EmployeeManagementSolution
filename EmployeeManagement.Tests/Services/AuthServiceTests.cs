using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Tests.Services
{
    public class AuthServiceTests
    {
        [Fact]
        public void Password_Verification_ShouldReturnTrue()
        {
            // Arrange
            var password = "123456";
            var hash = BCrypt.Net.BCrypt.HashPassword(password);

            // Act
            var result = BCrypt.Net.BCrypt.Verify(password, hash);

            // Assert
            Assert.True(result);
        }
    }
}
