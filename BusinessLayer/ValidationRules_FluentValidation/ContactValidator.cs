using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.UserMail).NotEmpty().WithMessage("Mail adresini boş geçemezsiniz.");
            RuleFor(x => x.UserSubject).NotEmpty().WithMessage("Konu adını boş geçemezsiniz...");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adını boş geçemezsiniz...");
            RuleFor(x => x.UserSubject).MinimumLength(3).WithMessage("Lütfen en az 3 (üç) karakter girşi yapınız...");
            RuleFor(x => x.UserName).MinimumLength(3).WithMessage("Lütfen en az 3 (üç) karakter girşi yapınız...");
            RuleFor(x => x.UserSubject).MaximumLength(50).WithMessage("Lütfen 20 karakterden fazla değer girişi yapmayınız...");
        }
    }
}