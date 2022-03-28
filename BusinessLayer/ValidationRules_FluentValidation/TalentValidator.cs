using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class TalentValidator : AbstractValidator<Talent>
    {
        public TalentValidator()
        {
            RuleFor(x => x.SkillName).NotEmpty().WithMessage("Yetenek Adını boş geçemezsiniz...!");
            RuleFor(x => x.SkillLevel).NotEmpty().WithMessage("Yetenek Seviyesini boş geçemezsiniz...!");
            RuleFor(x => x.SkillDetails).NotEmpty().WithMessage("Detaylar alanını boş geçemezsiniz...!");
            RuleFor(x => x.SkillArea).NotEmpty().WithMessage("Yetenek Alanı bölümünü boş geçemezsiniz...!");
            
            RuleFor(x => x.SkillName).MinimumLength(2).WithMessage("Lütfen en az 2 (iki) karakter girşi yapınız...");
            RuleFor(x => x.SkillName).MaximumLength(50).WithMessage("Lütfen 50 karakterden fazla değer girişi yapmayınız...");
            RuleFor(x => x.SkillDetails).MaximumLength(250).WithMessage("Lütfen 250 karakterden fazla değer girişi yapmayınız...");
            RuleFor(x => x.SkillLevel).NotEmpty().InclusiveBetween(0, 100).WithMessage("Yetenek Seviyesini boş geçemezsiniz veya 0-100 arasında bir sayı girmelisiniz...!");
            RuleFor(x => x.SkillLevel).NotEmpty().WithMessage("Yetenek Bilgisi bölümünü boş geçemezsiniz...!");
        }
    }
}
