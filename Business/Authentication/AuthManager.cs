using Business.Authentication.Validation.FluentValidation;
using Business.Repositories.UserRepository;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (result)
            {
                return "Kullanıcı girişi başarılı";
            }
            return "Kullanıcı bilgileri hatalı";
        }

        [ValidationAspect(typeof(AuthValidator))]
        public IResult Register(RegisterAuthDto registerDto)
        {
            int imgSize = 1;

            IResult result = BusinessRules.Run(
                CheckIfEmailExists(registerDto.Email),
                CheckIfImageExtensionAllow(registerDto.Image.FileName),
                CheckIfImageSizeIsLessThanOneMb(registerDto.Image.Length)
                );

            if (result != null)
            {
                return result;
            }

            _userService.Add(registerDto);
            return new SuccessResult("Kullanıcı kaydı başarıyla tamamlandı");
        }

        private IResult CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("Bu mail adresi daha önce kulanılmış");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageSizeIsLessThanOneMb(long imgSize)
        {
            decimal imageMbSize = Convert.ToDecimal(imgSize * 0.000001);
            if (imageMbSize > 1)
                return new ErrorResult("Yüklediğiniz resim boyutu 1 Mb'dan düşük olmalıdır");
            return new SuccessResult();
        }

        private IResult CheckIfImageExtensionAllow(string fileName)
        {
            var ext = fileName.Substring(fileName.LastIndexOf('.'));
            var extension = ext.ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Eklediğiniz resim .jpg, .jpeg, .gif, .png türlerinden biri olmalıdır!");
            }
            return new SuccessResult();
        }
    }
}
