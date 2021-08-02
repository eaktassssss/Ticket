using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Roles;

namespace Ticket.Validators.Validation.Fluent.Validators
{
    public class RoleValidator:AbstractValidator<RoleDto>
    {
        public RoleValidator()
        {
            string NotNullMessage = "Zorunlu alan";
            RuleFor(c => c.Name).NotNull().WithMessage(NotNullMessage);
        }
    }
}
