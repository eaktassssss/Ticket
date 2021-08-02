using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Users;

namespace Ticket.Validators.Validation.Fluent.Validators
{
    public class UserCreatedValidator : AbstractValidator<UserCreatedDto>
    {
        public UserCreatedValidator()
        {
            string NotNullMessage = "Zorunlu alan";
            RuleFor(c => c.Name).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.Surname).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.IdentityNumber).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.IdentityNumber).Must(CheckIdentityNumberCharacter).WithMessage("Tc No 11 karakter olmalıdır");
            RuleFor(c => c.IdentityNumber).Must(CheckIdentityNumberIsValid).WithMessage("Geçersiz Tc No girişi yaptınız");
            RuleFor(c => c.Gsm).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.Email).NotNull().WithMessage(NotNullMessage);
            RuleFor(c => c.Email).EmailAddress().WithMessage("Format dışı email");
            RuleFor(c => c.Password).NotNull().WithMessage(NotNullMessage);
        }
        private bool CheckIdentityNumberCharacter(string identityNumber)
        {
            if (identityNumber != null)
            {
                if (identityNumber.Length > 11 || identityNumber.Length < 11)
                    return false;
                else
                    return true;
            }
            return false;
        }
        private bool CheckIdentityNumberIsValid(string identityNumber)
        {
            try
            {
                bool result = false;
                if (identityNumber != null && identityNumber.Length == 11)
                {
                    Int64 ATCNO, BTCNO, TcNo;
                    long C1, C2, C3, C4, C5, C6, C7, C8, C9, Q1, Q2;
                    TcNo = Int64.Parse(identityNumber);
                    ATCNO = TcNo / 100;
                    BTCNO = TcNo / 100;
                    C1 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C2 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C3 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C4 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C5 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C6 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C7 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C8 = ATCNO % 10; ATCNO = ATCNO / 10;
                    C9 = ATCNO % 10; ATCNO = ATCNO / 10;
                    Q1 = ((10 - ((((C1 + C3 + C5 + C7 + C9) * 3) + (C2 + C4 + C6 + C8)) % 10)) % 10);
                    Q2 = ((10 - (((((C2 + C4 + C6 + C8) + Q1) * 3) + (C1 + C3 + C5 + C7 + C9)) % 10)) % 10);
                    result = ((BTCNO * 100) + (Q1 * 10) + Q2 == TcNo);
                    return result;
                }
                else
                    return false;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
