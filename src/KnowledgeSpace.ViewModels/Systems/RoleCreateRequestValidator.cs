using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.Systems
{
    public class RoleCreateRequestValidator : AbstractValidator<RoleCreateRequest>
    {
        public RoleCreateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Mã quyền là bắt buộc")
                .MaximumLength(50).WithMessage("Mã quyền không vượt quá 50 kí tự");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên quyền là bắt buộc");
        }
    }
}