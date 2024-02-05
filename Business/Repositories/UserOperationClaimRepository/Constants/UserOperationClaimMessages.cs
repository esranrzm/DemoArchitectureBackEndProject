using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.Constants
{
    public class UserOperationClaimMessages
    {
        public static string Added = "Yetki ataması başarılı";
        public static string Updated = "Yetki ataması başarıyla güncellendi";
        public static string Deleted = "Yetki ataması başarıyla silindi";
        public static string OperationClaimSetExist = "Bu kullanıcıya bu yetki daha önce atanmış";
        public static string OperationClaimNotExist = "Seçilen yetki bilgisi yetkilerde bulunmamaktadır";
        public static string UserNotExist = "Seçilen kullanıcı bulunamadı";
    }
}
