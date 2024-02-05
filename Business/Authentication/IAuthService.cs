using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Authentication
{
    public interface IAuthService
    {
        IResult Register(RegisterAuthDto registerDto);
        string Login(LoginAuthDto loginDto);
    }
}
