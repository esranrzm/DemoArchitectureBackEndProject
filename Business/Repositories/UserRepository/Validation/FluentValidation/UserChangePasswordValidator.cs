using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator() 
        {
            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("Password boş olamaz");
            RuleFor(p => p.NewPassword).MinimumLength(6).WithMessage("Password uzunluğu minimum 6 olmalı");
            RuleFor(p => p.NewPassword).Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir");
            RuleFor(p => p.NewPassword).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir");
            RuleFor(p => p.NewPassword).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir");
            RuleFor(p => p.NewPassword).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir");
        }
    }
}
