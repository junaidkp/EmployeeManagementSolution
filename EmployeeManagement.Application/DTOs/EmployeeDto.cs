using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public DateTime DateOfJoining { get; set; }
    }
}
