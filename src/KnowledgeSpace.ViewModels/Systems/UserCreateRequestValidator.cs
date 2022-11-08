using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.Systems
{
    public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
    {
        public UserCreateRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name là bắt buộc");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password phải có ít nhất 8 ký tự")
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .WithMessage("Password cần có chữ hoa, chữ thường, số, ký tự đặc biệt.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email là bắt buộc")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").WithMessage("Email không đúng định dạng");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number là bắt buộc");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name là bắt buộc")
                .MaximumLength(50).WithMessage("First name không vượt quá 50 kí tự");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name là bắt buộc")
                .MaximumLength(50).WithMessage("Last name không vượt quá 50 kí tự");
        }
    }
}