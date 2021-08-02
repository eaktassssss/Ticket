using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Dtos.Customers;
namespace Ticket.Validators.Validation.Fluent.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            string NotNullMessage = "Zorunlu alan";
            RuleFor(c => c.Title).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.Email).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.PrimaryContactUserNameSurname).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.PrimaryContactUserEmail).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.Gsm).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.Email).EmailAddress().WithMessage("Format dışı email");
            RuleFor(c => c.PrimaryContactUserEmail).EmailAddress().WithMessage("Format dışı email");
        }
    }
}
