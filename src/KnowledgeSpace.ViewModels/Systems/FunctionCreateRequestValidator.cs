using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.ViewModels.Systems
{
    public class FunctionCreateRequestValidator : AbstractValidator<FunctionCreateRequest>
    {
        public FunctionCreateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id là bắt buộc")
               .MaximumLength(50).WithMessage("Id không vượt quá 50 kí tự");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Tên phương thức là bắt buộc")
              .MaximumLength(200).WithMessage("Tên phương thức không vượt quá 200 kí tự");

            RuleFor(x => x.Url).NotEmpty().WithMessage("URL là bắt buộc")
             .MaximumLength(200).WithMessage("URL không vượt quá 200 kí tự");

            RuleFor(x => x.ParentId).MaximumLength(50)
                .When(x => !string.IsNullOrEmpty(x.ParentId))
                .WithMessage("URL cannot over limit 50 characters");
        }
    }
}