﻿using EntityLayer.Concreate;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules_FluentValidation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Alıcı adresini boş geçemezsiniz...");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konuyu boş geçemezsiniz...");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesajı boş geçemezsiniz...");
            RuleFor(x => x.ReceiverMail).EmailAddress().WithMessage("Geçerli bir e-posta değeri giriniz!")
                                        .When(i => !string.IsNullOrEmpty(i.ReceiverMail));
            RuleFor(x => x.Subject).MinimumLength(3).WithMessage("Lütfen en az 3 (üç) karakter girşi yapınız...");
        }
    }
}