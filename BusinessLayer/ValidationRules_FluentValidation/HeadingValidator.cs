using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class HeadingValidator : AbstractValidator<Heading>
    {
        public HeadingValidator()
        {
            RuleFor(x => x.HeadingName).NotEmpty().WithMessage("Başlık adını boş geçemezsiniz...");
            RuleFor(x => x.HeadingName).Null().WithMessage("Başlık adını boş geçemezsiniz...");
            RuleFor(x => x.HeadingName).MinimumLength(3).WithMessage("Lütfen en az 3 (üç) karakter girşi yapınız...");
            RuleFor(x => x.HeadingName).MaximumLength(20).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayınız...");
        }
    }
}
