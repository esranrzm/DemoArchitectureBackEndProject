using Business.Repositories.OperationClaimRepository.Constants;
using Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Business.Repositories.OperationClaimRepository
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            // class çağırıldığında çalışır
            // operationclaim dal'ı new'leme işlemi gibi yaoar memeroy yemez new kullanılmadığı için
            _operationClaimDal = operationClaimDal;
        }
        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            // kontroller yapılır
            // KONTROLLER OKEYSE, Data access'e add operation'ını yap denilir

            IResult result = BusinessRules.Run(IsNameExistForAdd(operationClaim.Name));
            if (result != null)
            {
                return result;
            }

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(OperationClaimMessages.Added);
        }


        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            IResult result = BusinessRules.Run(IsNameExistForUpdate(operationClaim));
            if (result != null)
            {
                return result;
            }
            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }


        public IResult Delete(OperationClaim operationClaim)
        {
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }


        public IDataResult<List<OperationClaim>> GetList()
        {
            return new SuccessDataResult<List<OperationClaim>>(_operationClaimDal.GetAll());
        }


        public IDataResult<OperationClaim> GetById(int id)
        {
            return new SuccessDataResult<OperationClaim>(_operationClaimDal.Get(p=> p.Id == id));
        }

        private IResult IsNameExistForAdd(string name)
        {
            var result = _operationClaimDal.Get(p=> p.Name == name);
            if (result != null)
            {
                return new ErrorResult(OperationClaimMessages.NameIsNotAvailable);
            }
            return new SuccessResult();
        }

        private IResult IsNameExistForUpdate(OperationClaim operationClaim)
        {
            var currentOperationClaim = _operationClaimDal.Get(p => p.Id == operationClaim.Id);
            if (currentOperationClaim.Name != operationClaim.Name)
            {
                var result = _operationClaimDal.Get(p => p.Name == operationClaim.Name);
                if (result != null)
                {
                    return new ErrorResult(OperationClaimMessages.NameIsNotAvailable);
                }               
            }
            return new SuccessResult();

        }
    }
}
