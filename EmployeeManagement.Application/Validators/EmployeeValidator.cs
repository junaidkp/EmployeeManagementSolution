using EmployeeManagement.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.EmployeeName)
                .NotEmpty().WithMessage("Employee name is required")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required")
                .MaximumLength(100);

            RuleFor(x => x.DateOfJoining)
                .NotEmpty().WithMessage("Date of Joining is required")
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Date of Joining cannot be in the future");
        }
    }
}
