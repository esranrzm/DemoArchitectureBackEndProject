﻿using Business.Repositories.UserRepository.Constants;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Business.Utilities.File;
using Core.Aspects.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;
        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async void Add(RegisterAuthDto registerDto)
        {
            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/img/");
            //string fileName = _fileService.FileSaveToFtp(registerDto.Image);
            byte[] fileByteArray = _fileService.FileConvertByteArrayToDatabase(registerDto.Image);

            var user = CreateUser(registerDto, fileName);
            _userDal.Add(user);
        }

        private User CreateUser(RegisterAuthDto registerDto, string fileName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out passwordHash, out passwordSalt);

            User user = new User();
            user.Id = 0;
            user.Email = registerDto.Email;
            user.Name = registerDto.Name;
            user.ImageUrl = fileName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return user;
        }

        public User GetByEmail(string email)
        {
            return _userDal.Get(p => p.Email == email);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(UserMessages.UpdatedUser);
        }

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
            return new SuccessResult(UserMessages.DeletedUser);
        }

        public IDataResult<List<User>> GetList()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll());
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(_userDal.Get(p=> p.Id == id));
        }
        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(p => p.Id == userChangePasswordDto.UserId);
            bool result = HashingHelper.VerifyPasswordHash(userChangePasswordDto.CurrentPassword, user.PasswordHash, user.PasswordSalt);
            if (!result)
            {
                return new ErrorResult(UserMessages.WrongCurrentPassword);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(userChangePasswordDto.NewPassword, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _userDal.Update(user);
            return new SuccessResult(UserMessages.PasswordChanged);

        }
    }
}
