using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Tickets;

namespace Ticket.Validators.Validation.Fluent.Validators
{
    public class TicketValidator:AbstractValidator<TicketDto>
    {
        public TicketValidator()
        {
            string NotNullMessage = "Zorunlu alan";
            RuleFor(c => c.Descripton).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.SubjectHeading).NotNull().WithMessage(NotNullMessage);
        }
    }
  
}
