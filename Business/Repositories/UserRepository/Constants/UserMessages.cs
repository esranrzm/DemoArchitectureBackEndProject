using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Constants
{
    public class UserMessages
    {
        public static string UpdatedUser = "Kullanıcı kaydı başarıyla güncellendi";
        public static string DeletedUser = "Kullanıcı kaydı başarıyla silindi"; 
        public static string WrongCurrentPassword = "Mevcut şifreniz hatalı girilmiştir"; 
        public static string PasswordChanged = "Şifre başarıyla değiştirildi"; 
    }
}
