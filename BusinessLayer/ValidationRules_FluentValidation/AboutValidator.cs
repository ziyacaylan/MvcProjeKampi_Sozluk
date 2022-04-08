using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class AboutValidator : AbstractValidator<About>
    {
        public AboutValidator()
        {
            RuleFor(x => x.AboutTitle).NotEmpty().WithMessage("Başlık alanını boş geçemezsiniz...");
            RuleFor(x => x.AboutDetails).NotEmpty().WithMessage("Açıklamayı boş geçemezsiniz...");
            RuleFor(x => x.AboutTitle).MinimumLength(3).WithMessage("Lütfen en az 3 (üç) karakter girşi yapınız...");
            RuleFor(x => x.AboutDetails).MinimumLength(3).WithMessage("Lütfen en az 3 (üç) karakter girşi yapınız...");
            RuleFor(x => x.AboutTitle).MaximumLength(20).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayınız...");
        }
    }
}
