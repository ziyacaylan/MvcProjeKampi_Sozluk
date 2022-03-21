using EntityLayer.Concreate;
using FluentValidation;
//using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class WriterValidator : AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adını boş geçemezsiniz...");
            RuleFor(x => x.WriterSurname).NotEmpty().WithMessage("Yazar soyadını boş geçemezsiniz...");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmını boş geçemezsiniz...");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan kısmını boş geçemezsiniz...");
            RuleFor(x => x.WriterAbout).Must(IsAboutValid).WithMessage("Hakkında kısmında mutlaka 'a' harfi geçmesi gerekmektedir.");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 (iki) karakter girşi yapınız...");
            RuleFor(x => x.WriterName).MaximumLength(20).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayınız...");

            // Yazarın hakkında kısmında mutlaka "a" harfinin geçmesi gerekiyor ödevinin sonucu
            bool IsAboutValid(string arg)
            {
                try
                {
                    Regex regex = new Regex(@"^(?=.*[a,A])");
                    return regex.IsMatch(arg);
                }
                catch (Exception)
                {

                    return false;
                }
            }
        }
    }
}