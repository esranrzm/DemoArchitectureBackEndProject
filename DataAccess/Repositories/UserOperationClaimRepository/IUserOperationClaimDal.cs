using Core.DataAccess;
using DataAccess.Context.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.UserOperationClaimRepository
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {

    }
}
